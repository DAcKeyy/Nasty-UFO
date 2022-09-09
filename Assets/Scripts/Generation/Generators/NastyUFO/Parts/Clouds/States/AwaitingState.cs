using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Difficulty;
using Data.Generators;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random; 

namespace Generation.Generators.NastyUFO.Parts.Clouds.States
{
	public class AwaitingState : GeneratorState<Cloud>
	{
		private readonly NastyUFO_GenerationSettings _settings;
		private readonly MonoPool<Cloud> _cloudsPool;
		private readonly CloudsFactory _cloudsFactory;

		public AwaitingState(
			ref MonoPool<Cloud> cloudsPool,
			UFO_DifficultyController difficultyController, 
			CloudsFactory cloudsFactory) : base(cloudsPool)
		{
			_cloudsFactory = cloudsFactory;
		}
		
		public override Task Create()
		{
			return Task.CompletedTask;
		}

		public override Task Update()	
		{
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