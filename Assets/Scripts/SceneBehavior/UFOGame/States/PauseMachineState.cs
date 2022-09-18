using System;
using System.Threading.Tasks;
using Input;
using Miscellaneous.StateMachines.Base;
using UI.Canvases;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame.States
{
	public class PauseMachineState : MachineState
	{
		private UFOPauseCanvas _ufoPauseCanvas;
		public PauseMachineState(UFOPauseCanvas ufoPauseCanvas)
		{
			_ufoPauseCanvas = ufoPauseCanvas;
		}
		
		public Action GameExit;
		
		public override Task OnEnter()
		{
			InputManager.CurrentInputManager.PauseAction.performed += ActionSubscription;
			_ufoPauseCanvas.gameObject.SetActive(true);
			Time.timeScale = 0;
			return Task.CompletedTask;
		}
	
		public override Task OnExit()
		{
			InputManager.CurrentInputManager.PauseAction.performed -= ActionSubscription;
			if(_ufoPauseCanvas != null) _ufoPauseCanvas.gameObject.SetActive(false);
			Time.timeScale = 1;
			return Task.CompletedTask;
		}

		private void ActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.PauseAction)
			{
				CurrentStateMachine.SwitchStateTo<GameRunState>();
			}
			
			if (context.action == InputManager.CurrentInputManager.GameExitAction)
			{
				GameExit();
			}
		}
	}
}