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
	public class BuildingsGenerator : LevelGenerator<ModularBuilding>
	{
		private readonly NastyUFOLevelGeneration_Settings _settings;
		private readonly BuildingsFactory _buildingsFactory;
		private readonly Camera _mainCamera;
		private readonly UFO _player;
		private readonly MonoPool<ModularBuilding> _buildingsPool;

		public BuildingsGenerator(
			ref MonoPool<ModularBuilding> buildingsPool,
			NastyUFOLevelGeneration_Settings settings, 
			BuildingsFactory buildingsFactory,
			Camera mainCamera,
			UFO player) : base(ref buildingsPool)
		{
			_settings = settings;
			_buildingsPool = buildingsPool;
			_buildingsFactory = buildingsFactory;
			_mainCamera = mainCamera;
			_player = player;
		}
		
		public override void Create()
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
				

				_buildingsPool.AddObject(modularBuilding);
			}
		}

		public override void Update()
		{
			var modularBuilding = _buildingsFactory.Create(
				new Vector2(
					_settings._generationStartPosition.x + _settings._buildingDistanceGap,
					_settings._generationStartPosition.y),
				Quaternion.Euler(new Vector3(0, 180, 0)));//TODO Убарть магичиские числа
			
			modularBuilding.AssembleBuilding(
				(ushort)Random.Range(_settings._buildingsFloorsRandomRange.x, _settings._buildingsFloorsRandomRange.y));
					
			_buildingsPool.AddObject(modularBuilding);//TODO Магические числа
		}
	}
}