using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Generation.Base;
using Generation.Contexts.NastyUFO.State;
using Miscellaneous.Pools;

namespace Generation.Contexts.NastyUFO
{
	public class LandGenerator : ObjectGenerator<Land>
	{
		private MonoPool<Land> _landPool;
		
		public LandGenerator(ref MonoPool<Land> pool) : base(ref pool)
		{
			_landPool = pool;

			StatesList = new List<GeneratorState>()
			{
				new LandGenerator_RunState()
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
