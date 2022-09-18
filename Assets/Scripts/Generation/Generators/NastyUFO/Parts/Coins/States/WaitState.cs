using Actors.NastyUFO.Coins;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Generators.NastyUFO.Parts.Coins.States
{
	public class WaitState : GeneratorState<Coin>
	{
		public WaitState(MonoPool<Coin> coinPool) : base(coinPool)
		{
			
		}
	}
}