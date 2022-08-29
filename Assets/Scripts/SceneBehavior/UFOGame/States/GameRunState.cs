using System.Threading.Tasks;
using Actors.NastyUFO;
using Generation.Generators.NastyUFO.States;
using Input;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.StateMachines.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame.States
{
	public class GameRunState : State
	{
		private ObjectGenerator<MonoBehaviour> _generator;
			
		public GameRunState(UFO player, ObjectGenerator<MonoBehaviour> generator)
		{
			_generator = generator;
			player.Died += Die;
		}
		
		public override Task OnEnter()
		{
			_generator.SwitchState(typeof(RunState));
			//InputManager.CurrentInputManager.JumpAction.performed += context => _gameSystem.Jump();
			InputManager.CurrentInputManager.PauseAction.performed += ActionSubscription;
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			//InputManager.CurrentInputManager.JumpAction.performed -= context => _gameSystem.Jump();
			InputManager.CurrentInputManager.PauseAction.performed -= ActionSubscription;
			
			return Task.CompletedTask;
		}

		public override Task Update()
		{
			_generator.CurrentState.Update();
			
			return Task.CompletedTask;
		}

		private void ActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.PauseAction)
			{
				CurrentStateMachine.SwitchState(typeof(PauseState));
			}
			if (context.action == InputManager.CurrentInputManager.JumpAction)
			{
				
			}
		}

		private void Die(UFO palyer)
		{
			CurrentStateMachine.SwitchState(typeof(GameOverState));
		}
	}
}