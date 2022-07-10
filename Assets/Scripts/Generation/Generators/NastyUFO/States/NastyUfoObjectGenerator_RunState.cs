using System.Threading.Tasks;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Base;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class NastyUfoObjectGenerator_RunState : GeneratorState<MonoBehaviour>
	{
		private readonly ObjectGenerator<ModularBuilding> _buildingsGenerator;
		private readonly ObjectGenerator<Cloud> _cloudsGenerator;
		
		public NastyUfoObjectGenerator_RunState(
			ref MonoPool<MonoBehaviour> pool,
			NastyUFOLevelGeneration_Settings settings) : base(ref pool, settings)
		{
			
		}
		
		public NastyUfoObjectGenerator_RunState(
			ref MonoPool<MonoBehaviour> pool, 
			NastyUFOLevelGeneration_Settings settings,
			ObjectGenerator<ModularBuilding> buildingsGenerator,
			ObjectGenerator<Cloud> cloudsGenerator) : base(ref pool, settings)
		{
			_buildingsGenerator = buildingsGenerator;
			_cloudsGenerator = cloudsGenerator;
		}
		
		public override async Task Create()
		{
			await _buildingsGenerator.Create();
			await _cloudsGenerator.Create();
		}

		public override async Task Update()
		{
			await _buildingsGenerator.Update();
			await _cloudsGenerator.Update();
		}
	}
}