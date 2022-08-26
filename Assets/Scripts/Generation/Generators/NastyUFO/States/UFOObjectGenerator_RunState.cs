using System.Threading.Tasks;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class UFOObjectGenerator_RunState : GeneratorState<MonoBehaviour>
	{
		private readonly ObjectGenerator<ModularBuilding> _buildingsGenerator;
		private readonly ObjectGenerator<Cloud> _cloudsGenerator;
		
		public UFOObjectGenerator_RunState(
			ref MonoPool<MonoBehaviour> pool,
			NastyUFOLevelGeneration_Settings settings) : base(pool)
		{
			
		}
		
		public UFOObjectGenerator_RunState(
			ref MonoPool<MonoBehaviour> pool, 
			NastyUFOLevelGeneration_Settings settings,
			ObjectGenerator<ModularBuilding> buildingsGenerator,
			ObjectGenerator<Cloud> cloudsGenerator) : base(pool)
		{
			_buildingsGenerator = buildingsGenerator;
			_cloudsGenerator = cloudsGenerator;
		}
		
		public override void Create()
		{
			_buildingsGenerator.Create();
			_cloudsGenerator.Create();
		}

		public override void Update()
		{
			_buildingsGenerator.Update();
			_cloudsGenerator.Update();
		}
	}
}