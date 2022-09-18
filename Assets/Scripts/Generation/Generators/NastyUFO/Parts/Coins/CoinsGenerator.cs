using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO.Coins;
using Data.Difficulty;
using Generation.Generators.NastyUFO.Parts.Coins.States;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Generators.NastyUFO.Parts.Coins
{
	public class CoinsGenerator : ObjectGenerator<Coin>
	{
		public CoinsGenerator(MonoPool<Coin> monoPool,
			UFO_DifficultyController difficultyController) : base(monoPool)
		{
			StatesList = new List<GeneratorState<Coin>>()
			{
				new RunState(monoPool, difficultyController),
				new WaitState(monoPool)
			};
			CurrentState = StatesList[0];
		}
		
		public override async Task Create() => await CurrentState.Create();
		
		public override async Task Update() => await CurrentState.Update();
	}
}