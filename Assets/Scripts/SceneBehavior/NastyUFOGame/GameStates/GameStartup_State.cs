using System.Threading.Tasks;
using Generation.Base;
using SceneBehavior.NastyUFOGame.Base;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class Startup_State : GameState
	{
		private ILevelGenerator UfoLevelGenerator;
		
		public Startup_State(ILevelGenerator levelGenerator) : base()
		{
			UfoLevelGenerator = levelGenerator;
		}
		
		public override Task Startup()
		{
			UfoLevelGenerator.Create();
			
			return Task.CompletedTask;
		}
	}
}