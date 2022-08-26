using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Contexts.NastyUFO.State;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Contexts.NastyUFO
{
	public class LandGenerator : ObjectGenerator<Land>
	{
		public LandGenerator(
			ref MonoPool<Land> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(monoPool)
		{
			StatesList = new List<GeneratorState<Land>>()
			{
				new LandGenerator_RunState(ref monoPool, settings)
			};

			CurrentState = StatesList[0];
		}
		
		public override void Create()
		{
			throw new System.NotImplementedException();
		}

		public override void Update()
		{
			throw new System.NotImplementedException();
		}
	}
}
