using System;
using System.Threading.Tasks;
using Input;
using Miscellaneous.StateMachines.Base;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame.States
{
	public class PauseState : State
	{
		public Action Exit;
		
		public override Task OnEnter()
		{
			InputManager.CurrentInputManager.PauseAction.performed += ActionSubscription;
			
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			InputManager.CurrentInputManager.PauseAction.performed -= ActionSubscription;
			
			return Task.CompletedTask;
		}

		private void ActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.PauseAction)
			{
				CurrentStateMachine.SwitchState(typeof(GameRunState));
			}
			
			if (context.action == InputManager.CurrentInputManager.GameExitAction)
			{
				Exit();
			}
		}
	}
}