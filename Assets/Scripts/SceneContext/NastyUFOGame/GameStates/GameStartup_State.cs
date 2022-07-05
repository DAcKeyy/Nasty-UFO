using System.Collections;
using SceneContext.NastyUFOGame.Base;

namespace SceneContext.NastyUFOGame.GameStates
{
	public class StartupState : GameState 
	{
		public StartupState(UFOGameSystem statesGameSystem) : base(statesGameSystem)
		{
			
		}
		
		public override IEnumerator Startup()
		{
			return base.Startup();
		}
		
	}
}