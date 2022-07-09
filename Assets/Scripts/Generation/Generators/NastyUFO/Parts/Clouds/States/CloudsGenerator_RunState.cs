using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Contexts.NastyUFO.States
{
	public class CloudsGenerator_AwaitngState : GeneratorState
	{
		private readonly NastyUFOLevelGeneration_Settings _settings;
		private readonly MonoPool<Cloud> _cloudsPool;
		private readonly CloudsFactory _cloudsFactory;

		public CloudsGenerator_AwaitngState(
			ref MonoPool<Cloud> cloudsPool,
			NastyUFOLevelGeneration_Settings settings, 
			CloudsFactory cloudsFactory)
		{
			_cloudsPool = cloudsPool;
			_settings = settings;
			_cloudsFactory = cloudsFactory;
		}
		
		public override Task Create()
		{
			//TODO Убрать магическое число
			var прогонГенератораОблаковВметрах = _settings._clearingRange * 2; 
			
			for (var x = 0; x < прогонГенератораОблаковВметрах; x += (int)_settings._cloudsGapRange)
			{
				var spawnPosition = new Vector3(
					Mathf.Clamp(-_settings._clearingRange + x, -_settings._clearingRange , _settings._clearingRange),
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
			
			return Task.CompletedTask;
		}

		public override Task Update()	
		{
			//берём последнее облако
			var lastCreatedCloud = _cloudsPool.GetLast();
			
			if (lastCreatedCloud == null) throw new Exception("А где облака?");

			var cloudHeight = _settings._cloudsHeight + _settings._generationStartPosition.y;
			
			var cloudPosition = new Vector3(
				_settings._generationCenter.transform.position.x /*+ (float)maxSpawnDist*/, 
				cloudHeight,//рандомно его поддвигаем 
				Random.Range(0, _settings._cloudsRandomShift.z));
			
			var cloud = RollDaCloud(_settings._cloudsSpawnChance, cloudPosition);
				
			//если облачко не заролялось то пох
			if(cloud == null) 
				return Task.CompletedTask;
			
			//ставим в пул
			_cloudsPool.AddObject(cloud);

			foreach (var additionCloud in SpawnCloudsLine(cloud))
			{
				_cloudsPool.AddObject(additionCloud);
			}
			
			return Task.CompletedTask;
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