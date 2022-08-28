using System.Threading.Tasks;
using Input;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.NastyUFOGame;

namespace SceneBehavior.UFOGame.States
{
	public class AwaitInputState : State
	{
		public override Task OnEnter()
		{
			InputManager.CurrentInputManager.JumpAction.performed += context => GameStart();
			
			return Task.CompletedTask;
		}

		public override Task Update()
		{
			
			
			
			return Task.CompletedTask;
		}

		private void GameStart()
		{
			CurrentStateMachine.SwitchState(typeof(GameRunState));
		}
	}
}