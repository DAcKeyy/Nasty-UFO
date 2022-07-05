using System.Threading.Tasks;
using SceneBehavior.NastyUFOGame.Base;

namespace SceneBehavior.NastyUFOGame.GameStates
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