using System.Threading.Tasks;
using Data.Generators;
using Generation.Base;
using SceneBehavior.NastyUFOGame.Base;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameLunched_State : GameState
	{
		private ILevelGenerator _levelGenerator;
		private NastyUFOLevelGeneration_Settings _settings;
		
		public GameLunched_State(
			ILevelGenerator levelGenerator, 
			NastyUFOLevelGeneration_Settings settings) : base()
		{
			_levelGenerator = levelGenerator;
			_settings = settings;
		}

		public async override Task Startup()
		{
			while (true)
			{
				_levelGenerator.Update();
				await Task.Delay((int)(_settings._levelUpdateRate * 1000));//ms
			}
		}
	}
}