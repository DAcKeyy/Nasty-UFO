using System;
using System.Collections.Generic;
using Miscellaneous.Interfaces;
using Scenes.Actors.NastyUFO.Buildings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.Generation.Factories.NastyUFO
{
	public class BuildingsFactory : IFactory<Vector2, Quaternion, ModularBuilding>
	{
		private BuildingFactorySettings _settings;
		//родитель для удобства поиска в иерахрхии
		private readonly GameObject _buildingParent;
		
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
			return UnityEngine.Object.Instantiate(
					_settings._buildPrefabPool[Random.Range(0,_settings._buildPrefabPool.Count)], //рандом дом из списка
					position, 
					rotation, 
					_buildingParent.transform)
				.GetComponent<ModularBuilding>();
		}
		
		[Serializable]
		public struct BuildingFactorySettings
		{
			public List<GameObject> _buildPrefabPool;
		}
	}
}
