using System.Threading.Tasks;
using Input;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.StateMachines.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame.States
{
	public class AwaitInputState : State
	{
		private ObjectGenerator<MonoBehaviour> _generator;
		
		public AwaitInputState(ObjectGenerator<MonoBehaviour> generator)
		{
			_generator = generator;
		}
		
		public override Task OnEnter()
		{
			InputManager.CurrentInputManager.JumpAction.performed += ActionSubscription;
			_generator.CurrentState.Create();
			return Task.CompletedTask;
		}

		public override Task Update()
		{
			_generator.CurrentState.Update();
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			InputManager.CurrentInputManager.JumpAction.performed -= ActionSubscription;
			
			return Task.CompletedTask;
		}

		private void ActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.JumpAction)
			{
				CurrentStateMachine.SwitchState(typeof(GameRunState));
			}
		}
	}
}