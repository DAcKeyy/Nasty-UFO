using System.Collections;
using System.Threading.Tasks;
using SceneContext.NastyUFOGame.Base;

namespace SceneContext.NastyUFOGame.GameStates
{
	public class WaitForInput_State : GameState
	{
		public WaitForInput_State() : base()
		{
			
		}

		public override Task Jump()
		{
			//SetSta
			return Task.CompletedTask;
		}
	}
}