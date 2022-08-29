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
	public class BuildingsFactory : IFactory<Vector2, Quaternion, ModularBuilding>
	{
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
		}
		
		public ModularBuilding Create(Vector2 position, Quaternion rotation)
		{
			ModularBuilding modularBuildingComponent = UnityEngine.Object.Instantiate(_settings._modularBuildingPrefab, position, rotation, _buildingParent.transform);
			
			BuildingData_ScriptableObject randomBuildingData = _settings._buildingDataList[Random.Range(0, _settings._buildingDataList.Count)];

			modularBuildingComponent.Init(randomBuildingData._buildingData);

			modularBuildingComponent.name = modularBuildingComponent.name + ++_createCounter;

			return modularBuildingComponent;
		}
		
		[Serializable]
		public struct BuildingFactorySettings
		{
			[HideInInspector] public ModularBuilding _modularBuildingPrefab;
			public List<BuildingData_ScriptableObject> _buildingDataList;
		}
	}
}
