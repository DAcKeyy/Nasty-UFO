using System;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Contexts.NastyUFO
{
	public class BuildingsGenerator : ILevelGenerator
	{
		private readonly NastyUFOLevelGeneration_Settings _settings;
		private readonly MonoPool<ModularBuilding> _buildingsPool;
		private readonly BuildingsFactory _buildingsFactory;
		private readonly Camera _mainCamera;
		private readonly UFO _player;

		public BuildingsGenerator(
			NastyUFOLevelGeneration_Settings settings, 
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
				//Теорема пифагора где вычисляем предел спауна облака в линии игры
				var camToPlayerDist = Vector3.Distance(_mainCamera.transform.position, _player.transform.position);
				var maxSpawnDist = (float)Math.Sqrt(Math.Pow(_settings._clearingRange, 2) - Math.Pow(camToPlayerDist, 2));
				var modularBuilding = _buildingsFactory.Create(
					new Vector3(
						_settings._generationStartPosition.x + maxSpawnDist,
						_settings._generationStartPosition.y,
						_player.transform.position.z),
					Quaternion.Euler(new Vector3(0, 180, 0))); //TODO Как узнать куда повернуть дом? - сделать плечи дома были парралельны вектору движения камеры
				
				modularBuilding.AssembleBuilding(
					(ushort) Random.Range(_settings._buildingsFloorsRandomRange.x, _settings._buildingsFloorsRandomRange.y));
				

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