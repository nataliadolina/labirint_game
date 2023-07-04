using UnityEngine;
using Zenject;
using Props.Enemies;
using Maze;
using System.Linq;
using System;
using Extensions;
using System.Collections.Generic;
using Interfaces;

namespace States
{
    internal class FollowMovingSystemState : State
    {
        private Vector2[] _movingSystem;
        private Vector3[] _path;
        private int _currentCellCount = 0;
        private Vector3 _targetPosition;

        private PathGenerator _pathGenerator;
        private IMazeDataGenerator _dataGenerator;
        private PositionCellConverter _positionCellConverter;
        private Enemy _enemy;

        private int _pathLastIndex;
        private float _speed = 0;
        private int _step = 1;

        internal FollowMovingSystemState(Enemy enemy,
            PathGenerator pathGenerator,
            IMazeDataGenerator mazeDataGenerator,
            PositionCellConverter positionCellConverter,
            Settings settings)
        {

            _dataGenerator = mazeDataGenerator;
            
            _pathGenerator = pathGenerator;
            _positionCellConverter = positionCellConverter;

            _enemy = enemy;
            _movingSystem = GenerateEnemyMovingSystem();
            _path = new Vector3[] { };

            if (_movingSystem.Length == 1)
            {
                return;
            }

            SavePath();

            _enemy.Position = _path[0];
            _targetPosition = _path[1];
            _pathLastIndex = _path.Length - 1;
            _speed = UnityEngine.Random.Range(settings.MinSpeed, settings.MaxSpeed);
        }

        private void SavePath()
        {
            for (int i = 0; i < _movingSystem.Length-1; i++)
            {
                Vector2 fromCell = _movingSystem[i];
                Vector2 toCell = _movingSystem[i + 1];
                List<Vector2> generatedPath = _pathGenerator.GeneratePath(fromCell, toCell);

                _path = _path.Concat(
                    _pathGenerator.GeneratePath(fromCell, toCell)
                    .Select(x => _positionCellConverter.ReturnPositionInMaze(x))
                    )
                    .ToArray();
            }
        }

        internal Vector2[] GenerateEnemyMovingSystem()
        {
            Vector3 position = _enemy.Position;
            Vector2 positionInMaze = _positionCellConverter.ReturnObjectCell(position);
            List<Vector2> neighbourCells = positionInMaze.GetNeighbours(UnityEngine.Random.Range(2, 5), _dataGenerator.Maze);
            if (neighbourCells.Count == 0)
            {
                return null;
            }
             
            if (neighbourCells.Count == 1)
            {
                return new Vector2[2] { positionInMaze, neighbourCells[0] };
            }

            neighbourCells = neighbourCells.Shuffle();
            int movingSystemLength = UnityEngine.Random.Range(2, Mathf.Clamp(neighbourCells.Count, 2, 4));
            Vector2[] movingSystem = new Vector2[movingSystemLength];
            for (int i = 0; i < movingSystemLength; i++)
            {
                movingSystem[i] = neighbourCells[i];
            }

            return movingSystem;
        }

        public override void Update()
        {
            if (_path.Length == 0){
                return;
            }

            if (_enemy.Position == _targetPosition)
            {
                _targetPosition = _path[_currentCellCount];
                _currentCellCount += _step;
                if (_currentCellCount == _pathLastIndex || _currentCellCount == 0)
                {
                    _step *= -1;
                }
            }
            _enemy.Transform.LookAt(_targetPosition);
            _enemy.Position = Vector3.MoveTowards(_enemy.Position, _targetPosition, _speed * Time.deltaTime);
        }

        [Serializable]
        internal class Settings
        {
            public float MinSpeed;
            public float MaxSpeed;
        }

        internal class Factory : PlaceholderFactory<FollowMovingSystemState>
        {

        }
    }
}
