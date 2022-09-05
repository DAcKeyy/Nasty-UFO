using System;
using System.Collections.Generic;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Miscellaneous.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Factories.NastyUFO
{
	[Serializable]
	public class BuildingsFactory : IFactory<Vector3, Quaternion, ModularBuilding>
	{
		public Bounds BiggestBuildingsBound => _biggestBuildingsBound;
		private Bounds _biggestBuildingsBound;
		private BuildingFactorySettings _settings;
		//родитель для удобства поиска в иерахрхии
		private readonly GameObject _buildingParent;
		private int _createCounter;
		
		public BuildingsFactory(BuildingFactorySettings settings)
		{
			_settings = settings;

			_buildingParent = new GameObject("Buildings") {
				transform = {
					position = Vector3.zero,
				}
			};

			if (settings._biggestBuildingBounds.size != Vector3.zero)
			{
				_biggestBuildingsBound = settings._biggestBuildingBounds;
			}
			else
			{
				foreach (var building in _settings._buildingDataList)
				{
					if (building._buildingData.GetSumOfBounds().size.x > _biggestBuildingsBound.size.x ||
					    building._buildingData.GetSumOfBounds().size.z > _biggestBuildingsBound.size.z)
					{
						_biggestBuildingsBound = building._buildingData.GetSumOfBounds();
					}
				}
			}
		}
		
		public ModularBuilding Create(Vector3 position, Quaternion rotation)
		{
			ModularBuilding modularBuildingComponent = UnityEngine.Object.Instantiate(_settings._modularBuildingPrefab,
																				position, rotation, _buildingParent.transform);
			
			BuildingData_ScriptableObject randomBuildingData = _settings._buildingDataList[(int)Random.Range(0, _settings._buildingDataList.Count - 1)];

			modularBuildingComponent.Init(randomBuildingData._buildingData);

			modularBuildingComponent.name = modularBuildingComponent.name + ++_createCounter;

			return modularBuildingComponent;
		}
		
		[Serializable]
		public struct BuildingFactorySettings
		{
			[HideInInspector] public ModularBuilding _modularBuildingPrefab;
			[Tooltip("Список данных о построенных домах")]
			public List<BuildingData_ScriptableObject> _buildingDataList;
			[HideInInspector] public Bounds _biggestBuildingBounds;
		}
	}
}
