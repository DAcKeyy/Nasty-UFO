using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Generators.NastyUFO.Parts.Lands.State;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Generators.NastyUFO.Parts.Lands
{
	public class LandGenerator : ObjectGenerator<Land>
	{
		public LandGenerator(
			ref MonoPool<Land> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(monoPool)
		{
			StatesList = new List<GeneratorState<Land>>()
			{
				new RunState(ref monoPool, settings)
			};

			CurrentState = StatesList[0];
		}
		
		public override Task Create()
		{
			throw new System.NotImplementedException();
		}

		public override Task Update()
		{
			throw new System.NotImplementedException();
		}
	}
}
