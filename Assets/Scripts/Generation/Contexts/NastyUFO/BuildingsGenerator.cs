using Data.Generators;
using Data.Pools;
using Scenes.Actors.NastyUFO;
using Scenes.Actors.NastyUFO.Buildings;
using Scenes.Generation.Base;
using Scenes.Generation.Factories.NastyUFO;	
using UnityEngine;

namespace Scenes.Generation.Contexts.NastyUFO
{
	public class BuildingsGenerator : ILevelGenerator
	{
		private readonly NastyUFOLevelGenerationSettings _settings;
		private readonly MonoPool<ModularBuilding> _buildingsPool;
		private readonly BuildingsFactory _buildingsFactory;
		private readonly Camera _mainCamera;
		private readonly UFO _player;

		public BuildingsGenerator(
			NastyUFOLevelGenerationSettings settings, 
			BuildingsFactory buildingsFactory,
			Camera mainCamera,
			UFO player)
		{
			_settings = settings;
			_buildingsFactory = buildingsFactory;
			_mainCamera = mainCamera;
			_player = player;
			_buildingsPool = new MonoPool<ModularBuilding>();
		}
		
		public void Create()
		{
			//TODO Убрать магичиские числа
			var прогонДомовВМетрах = _settings._clearingRange * 2; 
			
			for (var x = 0; x < прогонДомовВМетрах; x += (int)_settings._buildingDistanceGap)
			{
				var modularBuilding = _buildingsFactory.Create(
					new Vector3(
						_settings._generationStartPosition.x,
						_settings._generationStartPosition.y,
						_player.transform.position.z),
					Quaternion.Euler(new Vector3(0, 180, 0))); //TODO Как узнать куда повернуть дом? - сделать плечи дома были парралельны вектору движения камеры
				
				modularBuilding.AssembleBuilding(
					(ushort) Random.Range(_settings._buildingsFloorsRandomRange.x, _settings._buildingsFloorsRandomRange.y));
				
				if(_buildingsPool.PrefabPool.Count == 0)
					modularBuilding.transform.position = new Vector3(
						Mathf.Clamp(-_settings._clearingRange + x, -_settings._clearingRange, _settings._clearingRange),
						_settings._generationStartPosition.y,
						_player.transform.position.z);

				_buildingsPool.AddObject(modularBuilding);
			}
		}

		public void Update()
		{
			GeneratorTools<ModularBuilding>.ClearFarObjects(_buildingsPool, _settings._clearingRange, _mainCamera.transform);

			var modularBuilding = _buildingsFactory.Create(
				new Vector2(
					_settings._generationStartPosition.x + _settings._buildingDistanceGap,
					_settings._generationStartPosition.y),
				Quaternion.Euler(new Vector3(0, 180, 0)));//TODO Убарть магичиские числа
			
			modularBuilding.AssembleBuilding(
				(ushort)Random.Range(_settings._buildingsFloorsRandomRange.x, _settings._buildingsFloorsRandomRange.y));
					
			_buildingsPool.AddObject(modularBuilding);//TODO Магические числа
		}

		public void SetMode(int mode)
		{
			throw new System.NotImplementedException();
		}
	}
}