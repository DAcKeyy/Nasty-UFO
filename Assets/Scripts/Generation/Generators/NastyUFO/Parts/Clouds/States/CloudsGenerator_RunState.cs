using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Contexts.NastyUFO.States
{
	public class CloudsGenerator_AwaitngState : GeneratorState<Cloud>
	{
		private readonly NastyUFOLevelGeneration_Settings _settings;
		private readonly MonoPool<Cloud> _cloudsPool;
		private readonly CloudsFactory _cloudsFactory;

		public CloudsGenerator_AwaitngState(
			ref MonoPool<Cloud> cloudsPool,
			NastyUFOLevelGeneration_Settings settings, 
			CloudsFactory cloudsFactory) : base(cloudsPool)
		{
			_cloudsFactory = cloudsFactory;
		}
		
		public override void Create()
		{
			
		}

		public override void Update()	
		{
			
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