using System.Collections.Generic;
using UnityEngine;
using States;
using Maze;
using Props.Player;
using Props.Enemies;

internal class StateFollowingPath : State
{
    private PositionCellConverter _positionCellConverter;
    private PathGenerator _pathGenerator;

    private Transform _enemyTransform;
    private Player _player;

    private List<Vector2> _path;
    private Vector3 _targetPosition;
    private int _pathLastIndex;

    private int _currentCellCount;
    private float _speed;

    public StateFollowingPath(
        Enemy enemy,
        PositionCellConverter positionCellConverter,
        PathGenerator pathGenerator,
        Player player)
    {
        _enemyTransform = enemy.transform;
        _player = player;
        _player = player;

        _pathGenerator = pathGenerator;
        _positionCellConverter = positionCellConverter;
        
    }
    public override void Start()
    {
        Vector2 targetCell = _positionCellConverter.ReturnObjectCell(_player.Position);
        Vector2 startCell = _positionCellConverter.ReturnObjectCell(_enemyTransform.position);
        _path = _pathGenerator.GeneratePath(startCell, targetCell);
        _pathLastIndex = _path.Count - 1;
        _targetPosition = _positionCellConverter.ReturnPositionInMaze(_path[1]);
    }

    public override void Update()
    {
        if (_currentCellCount >= _pathLastIndex)
        {
            return;
        }

        if (_enemyTransform.position == _targetPosition)
        {
            _currentCellCount++;
            if (_currentCellCount >= _pathLastIndex)
            {
                _currentCellCount = 0;
                return;
            }

            _targetPosition = _positionCellConverter.ReturnPositionInMaze(_path[_currentCellCount]);
        }
        _enemyTransform.LookAt(_targetPosition);
        _enemyTransform.position = Vector3.MoveTowards(_enemyTransform.position, _targetPosition, _speed * Time.deltaTime);
    }
}
