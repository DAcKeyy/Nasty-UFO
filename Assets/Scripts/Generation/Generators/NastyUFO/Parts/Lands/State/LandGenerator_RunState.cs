using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Miscellaneous.Pools;

namespace Generation.Contexts.NastyUFO.State
{
	public class LandGenerator_RunState : GeneratorState<Land>
	{
		public LandGenerator_RunState(
			ref MonoPool<Land> pool,
			NastyUFOLevelGeneration_Settings settings) : base(ref pool, settings)
		{
			
		}
		
		public override Task Create()
		{
			return base.Create();
		}

		public override Task Update()
		{
			return base.Update();
		}
	}
}