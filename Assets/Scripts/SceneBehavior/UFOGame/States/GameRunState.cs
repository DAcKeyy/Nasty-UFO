using System.Threading.Tasks;
using Input;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.NastyUFOGame;

namespace SceneBehavior.UFOGame.States
{
	public class GameRunState : State
	{
		private UFOGameSystem _gameSystem;
		
		public GameRunState(UFOGameSystem gameSystem)
		{
			this._gameSystem = gameSystem;
		}
		
		public override Task OnEnter()
		{
			InputManager.CurrentInputManager.JumpAction.performed += context => _gameSystem.Jump();
			
			return Task.CompletedTask;
		}
	}
}