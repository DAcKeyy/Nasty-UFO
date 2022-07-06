﻿using System;
using System.Collections.Generic;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Contexts.NastyUFO
{
	public class CloudsGenerator : ILevelGenerator
	{
		private readonly NastyUFOLevelGeneration_Settings _settings;
		private readonly MonoPool<Cloud> _cloudsPool;
		private readonly CloudsFactory _cloudsFactory;
		private readonly Camera _mainCamera;
		private readonly UFO _player;
		private float _specialClearingRangeForClouds;

		public CloudsGenerator(
			NastyUFOLevelGeneration_Settings settings, 
			CloudsFactory cloudsFactory,
			Camera mainCamera,
			UFO player)
		{
			_settings = settings;
			_cloudsFactory = cloudsFactory;
			_cloudsPool = new MonoPool<Cloud>();
			_mainCamera = mainCamera;
			_player = player;
		}
		
		//TODO Создавать облака не только в даль по игре но и в ширь, в горизонт
		
		public void Create()
		{	
			_specialClearingRangeForClouds = _settings._clearingRange * _settings._cloudsFactorySettings._cloudsScale;
			
			//TODO Убрать магическое число
			var прогонГенератораОблаковВметрах = _specialClearingRangeForClouds * 2; 
			
			for (var x = 0; x < прогонГенератораОблаковВметрах; x += (int)_settings._cloudsGapRange)
			{
				var spawnPosition = new Vector3(
					Mathf.Clamp(-_specialClearingRangeForClouds + x, -_specialClearingRangeForClouds , _specialClearingRangeForClouds),
					_settings._cloudsHeight + _settings._generationStartPosition.y,
					Random.Range(0, _settings._cloudsRandomShift.z));//рандомно его поддвигаем 
				
				var cloud = RollDaCloud(_settings._cloudsSpawnChance, spawnPosition);
				
				//если облачко не заролялось то некст
				if(cloud == null) continue;
				
				//ставим в пул для контроля
				_cloudsPool.AddObject(cloud);
				
				foreach (var additionCloud in SpawnCloudsLine(cloud))
				{
					_cloudsPool.AddObject(additionCloud);
				}
			}
		}

		
		//этот апдейт лучше делать с хорошей задержкой
		public void Update()
		{
			GeneratorTools<Cloud>.ClearFarObjects(_cloudsPool, 
				_specialClearingRangeForClouds + 
				_settings._cloudsRandomShift.z + 
				(_settings._aditionCloudsOnLine * _settings._cloudsGapRange), 
				_settings._generationCenter);

			//берём последнее облако
			var lastCreatedCloud = _cloudsPool.GetLast();
			
			if (lastCreatedCloud == null) throw new Exception("А где облака?");
			
			//растояние облака и камеры
			var cameraAndLastCloudDistance = Vector3.Distance(_mainCamera.transform.position, lastCreatedCloud.transform.position);
			
			//если ласт облачко не достаточно далеко до радиуса чистки..
			if (Mathf.Abs(cameraAndLastCloudDistance - _specialClearingRangeForClouds) < _settings._cloudsGapRange * _settings._cloudsFactorySettings._cloudsScale) 
				return;

			var cloudHeight = _settings._cloudsHeight + _settings._generationStartPosition.y;
			
			var cloudPosition = new Vector3(
				_player.transform.position.x /*+ (float)maxSpawnDist*/, 
				cloudHeight,//рандомно его поддвигаем 
				Random.Range(0, _settings._cloudsRandomShift.z));
			
			var cloud = RollDaCloud(_settings._cloudsSpawnChance, cloudPosition);
				
			//если облачко не заролялось то пох
			if(cloud == null) 
				return;
			
			//ставим в пул
			_cloudsPool.AddObject(cloud);

			foreach (var additionCloud in SpawnCloudsLine(cloud))
			{
				_cloudsPool.AddObject(additionCloud);
			}
		}

		public void SetMode(int mode)
		{
			throw new System.NotImplementedException();
		}

		private Cloud[] SpawnCloudsLine(Cloud originCloud)
		{
			List<Cloud> clouds = new List<Cloud>(); 
			for (var i = 1 ; i <= _settings._aditionCloudsOnLine ; i++)
			{
				clouds.Add(RollDaCloud(_settings._cloudsSpawnChance, 
					new Vector3(
					originCloud.transform.position.x + Random.Range(0, _settings._cloudsRandomShift.x),
					originCloud.transform.position.y + Random.Range(0, _settings._cloudsRandomShift.y),
					originCloud.transform.position.z + _settings._cloudsGapRange * i)));
			}

			return clouds.ToArray();
		}
		
		private Cloud RollDaCloud(float chance, Vector3 position)
		{
			if (Random.Range(0f, 0.999f) > chance) 
				return null;

			return _cloudsFactory.Create(position);
		}
	}
}
