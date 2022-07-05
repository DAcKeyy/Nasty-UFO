using System.Collections;
using SceneContext.UFO.Base;

namespace SceneContext.UFO.GameStates
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