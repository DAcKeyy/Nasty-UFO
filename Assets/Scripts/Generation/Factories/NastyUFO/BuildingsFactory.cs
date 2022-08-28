using System;
using System.Collections.Generic;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Miscellaneous.Extensions.Variables;
using Miscellaneous.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Factories.NastyUFO
{
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
			GameObject modularBuildingGameObj = new GameObject($"Modular Building {++_createCounter}")
			{
				transform =
				{
					position = position,
					rotation = rotation//возможно localRotation
				}
			};

			ModularBuilding modularBuildingComponent = modularBuildingGameObj.AddComponent<ModularBuilding>();

			BuildingData_ScriptableObject randomBuildingData = _settings._buildingDataList[Random.Range(0, _settings._buildingDataList.Count)];
			BuildingData buildingData = randomBuildingData._buildingData;
			
			modularBuildingComponent.Init(buildingData);

			return modularBuildingComponent;
		}
		
		[Serializable]
		public struct BuildingFactorySettings
		{
			public List<BuildingData_ScriptableObject> _buildingDataList;
			public MinMaxInt _randomFloorsGap;
		}
	}
}
