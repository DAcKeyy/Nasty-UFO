using System.Collections;
using System.Threading.Tasks;
using Generation.Base;
using SceneContext.NastyUFOGame.Base;

namespace SceneContext.NastyUFOGame.GameStates
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