using System.Threading.Tasks;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Miscellaneous.Pools;

namespace Generation.Generators.NastyUFO.Parts.Buildings.States
{
	public class BuildingsGenerator_RunState : GeneratorState<ModularBuilding>
	{
		private BuildingsFactory _factory;
		
		public BuildingsGenerator_RunState(
			ref MonoPool<ModularBuilding> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(ref monoPool, settings)
		{
			_factory = new BuildingsFactory(settings._buildingsFactorySettings);
		}
		
		public override Task Create()
		{
			
			
			return Task.CompletedTask;
		}

		public override Task Update()
		{
			return base.Update();
		}
	}
}