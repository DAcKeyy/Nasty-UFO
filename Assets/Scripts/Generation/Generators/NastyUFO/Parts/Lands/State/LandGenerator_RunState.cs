using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Contexts.NastyUFO.State
{
	public class LandGenerator_RunState : GeneratorState<Land>
	{
		public LandGenerator_RunState(
			ref MonoPool<Land> pool,
			NastyUFOLevelGeneration_Settings settings) : base(pool)
		{
			
		}
		
		public override void Create()
		{
			base.Create();
		}

		public override void Update()
		{
			base.Update();
		}
	}
}