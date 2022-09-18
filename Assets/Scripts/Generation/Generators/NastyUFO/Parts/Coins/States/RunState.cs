using System.Threading.Tasks;
using Actors.NastyUFO.Coins;
using Data.Difficulty;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Generators.NastyUFO.Parts.Coins.States
{
	public class RunState : GeneratorState<Coin>
	{
		private UFO_DifficultyController _difficultyController;
		
		public RunState(MonoPool<Coin> coinPool,
			UFO_DifficultyController difficultyController) : base(coinPool)
		{
			_difficultyController = difficultyController;
		}

		public override async Task Create()
		{
			await Task.CompletedTask;
		}

		public override async Task Update()
		{
			await Task.CompletedTask;
		}
	}
}