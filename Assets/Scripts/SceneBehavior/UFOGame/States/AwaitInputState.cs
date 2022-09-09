using Input;
using System.Threading.Tasks;
using Data.Difficulty;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.StateMachines.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame.States
{
	public class AwaitInputState : State
	{
		private readonly ObjectGenerator<MonoBehaviour> _generator;
		private UFO_DifficultyController _difficultyController;
		
		public AwaitInputState(
			ObjectGenerator<MonoBehaviour> generator,
			UFO_DifficultyController difficultyController)
		{
			_generator = generator;
			_difficultyController = difficultyController;
		}
		
		public override async Task OnEnter()
		{
			InputManager.CurrentInputManager.JumpAction.performed += ActionSubscription;
			
			await _generator.CurrentState.Create();
		}

		public override async Task Update()
		{
			await _generator.CurrentState.Update();
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