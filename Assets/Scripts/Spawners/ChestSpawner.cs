using System.Collections.Generic;
using UnityEngine;
using Props.Chests;
using Maze;
using Enums;
using System.Linq;
using Utilities.Utils;
using Installers;
using UI;
using Interfaces;

namespace Spawners
{
    internal class ChestSpawner
    {
        private PickUpUIAnimationLauncher.Factory _factory;
        private Dictionary<PickUpType, int> _pickUpAmountDataMap = new Dictionary<PickUpType, int>();
        private List<PickUpType> _availableTypes = new List<PickUpType>();
        private float _chestHalfScaleY;
        private IResourceCounter[] _resourceCounters;
        private PickUpUIAnimation.Settings _uiAnimationSettings;

        internal ChestSpawner(
            PickUpUIAnimation.Settings uiAnimationSettings,
            MazeDataGenerator.Settings mazeDataSettings,
            PickUpUIAnimationLauncher.Factory factory,
            GameInstaller.Settings gameInstallerSettings,
            IResourceCounter[] resourceCounters)
        {
            _resourceCounters = resourceCounters;
            _uiAnimationSettings = uiAnimationSettings;
            _factory = factory;
            CreatePickUpAmountData(mazeDataSettings.PickUpAmount);
            _chestHalfScaleY = gameInstallerSettings.ChestPrefab.transform.localScale.y / 2;
        }

        private void CreatePickUpAmountData(List<PickUpTypeAmountData> PickUpAmount)
        {
            foreach (var pickUpAmountData in PickUpAmount)
            {
                if (pickUpAmountData.PickUpAmount <= 0)
                {
                    continue;
                }
                _pickUpAmountDataMap.Add(pickUpAmountData.PickUpType, pickUpAmountData.PickUpAmount);
                _availableTypes.Add(pickUpAmountData.PickUpType);
            }
        }

        private PickUpType GenerateRandomPickUpType()
        {
            if (_availableTypes.Count == 0)
            {
                return PickUpType.None;
            }

            int idx = UnityEngine.Random.Range(0, _availableTypes.Count);
            PickUpType type = _availableTypes[idx];
            _pickUpAmountDataMap[type]--;
            if (_pickUpAmountDataMap[type] == 0)
            {
                _availableTypes.Remove(type);
            }

            return type;
        }

        internal void Spawn(Vector3 position)
        {
            position.y = _chestHalfScaleY;
            PickUpUIAnimationLauncher launcher = _factory.Create();
            launcher.transform.position = position;

            PickUpType pickUpType = GenerateRandomPickUpType();
            IResourceCounter resourceCounter = _resourceCounters
                .Where(rc => rc.PickUpType == pickUpType)
                .ToArray()[0];

            Sprite sprite = _uiAnimationSettings.PickUpAnimationData.Where(_ => _.PickUpType == pickUpType).ToArray()[0].Sprite;
            Vector3 endPoint = resourceCounter.IconPosition;
            launcher.SetUp(endPoint, resourceCounter, sprite);
        }
    }
}
