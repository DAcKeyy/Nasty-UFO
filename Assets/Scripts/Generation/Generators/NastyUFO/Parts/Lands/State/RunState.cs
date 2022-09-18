using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Generators.NastyUFO.Parts.Lands.State
{
	public class RunState : GeneratorState<Land>
	{
		public RunState(
			ref MonoPool<Land> coinPool,
			NastyUFO_GenerationSettings settings) : base(coinPool)
		{
			
		}
		
		public override Task Create()
		{
			base.Create();
			return Task.CompletedTask;
		}

		public override Task Update()
		{
			base.Update();
			return Task.CompletedTask;
		}
	}
}