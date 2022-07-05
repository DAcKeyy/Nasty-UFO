using System;
using Data.Generators;
using Data.Pools;
using Scenes.Actors.NastyUFO;
using Scenes.Generation.Base;
using Scenes.Generation.Factories.NastyUFO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.Generation.Contexts.NastyUFO
{
	public class CloudsGenerator : ILevelGenerator
	{
		private readonly NastyUFOLevelGenerationSettings _settings;
		private readonly MonoPool<Cloud> _cloudsPool;
		private readonly CloudsFactory _cloudsFactory;
		private readonly Camera _mainCamera;
		private readonly UFO _player;

		public CloudsGenerator(
			NastyUFOLevelGenerationSettings settings, 
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
			//TODO Убрать магическое число
			var прогонГенератораОблаковВметрах = _settings._clearingRange * 2; 
			
			for (var x = 0; x < прогонГенератораОблаковВметрах; x += (int)_settings._cloudsGapRange)
			{
				var cloud = RollDaCloud(_settings._cloudsSpawnChance);
				
				//если облачко не заролялось то некст
				if(cloud == null) continue;

				var cloudHeight = _settings._cloudsHeight + _settings._generationStartPosition.y;
				
				cloud.transform.position = new Vector3(
					Mathf.Clamp(-_settings._clearingRange + x, -_settings._clearingRange, _settings._clearingRange),
					cloudHeight,
					Random.Range(0, _settings._cloudsRandomShift.z));//рандомно его поддвигаем 
				
				//ставим в пул для контроля
				_cloudsPool.AddObject(cloud);
			}
		}

		
		//этот апдейт лучше делать с хорошей задержкой
		public void Update()
		{
			GeneratorTools<Cloud>.ClearFarObjects(_cloudsPool, _settings._clearingRange, _mainCamera.transform);

			//берём последнее облако
			var lastCreatedCloud = _cloudsPool.GetLast();
			
			if (lastCreatedCloud == null) throw new Exception("А где облака?");
			
			//растояние облака и камеры
			var cameraAndLastCloudDistance = Vector3.Distance(_mainCamera.transform.position, lastCreatedCloud.transform.position);
			
			//если ласт облачко не достаточно далеко до радиуса чистки..
			if (Mathf.Abs(cameraAndLastCloudDistance - _settings._clearingRange) < _settings._cloudsGapRange) 
				return;
			
			var cloud = RollDaCloud(_settings._cloudsSpawnChance);
				
			//если облачко не заролялось то пох
			if(cloud == null) 
				return;
			
			var cloudHeight = _settings._cloudsHeight + _settings._generationStartPosition.y;
			
			//Теорема пифагора где вычисляем предел спауна облака в линии игры
			var camToPlayerDist = Vector3.Distance(_mainCamera.transform.position, _player.transform.position);
			var maxSpawnDist = Math.Sqrt(Math.Pow(_settings._clearingRange, 2) - Math.Pow(camToPlayerDist, 2));
			
			cloud.transform.position = new Vector3(
				_player.transform.position.x + (float)maxSpawnDist, 
				cloudHeight,//рандомно его поддвигаем 
				Random.Range(0, _settings._cloudsRandomShift.z));
				
			//ставим в пул
			_cloudsPool.AddObject(cloud);
		}

		public void SetMode(int mode)
		{
			throw new System.NotImplementedException();
		}

		private Cloud RollDaCloud(float chance)
		{
			if (Random.Range(0f, 0.999f) > chance) 
				return null;

			return _cloudsFactory.Create(Vector3.zero);
		}
	}
}
