using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Difficulty;
using Generation.Generators.NastyUFO.States;
using Input;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.StateMachines.Base;
using UI.Canvases;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame.States
{
	public class GameRunMachineState : MachineState
	{
		private readonly UFOGameCanvas? _ufoGameCanvas;
		private UFO_DifficultyController _difficultyController;
		private readonly ObjectGenerator<MonoBehaviour> _generator;
		private readonly UFO _player;
			
		public GameRunMachineState(
			UFO player, 
			ObjectGenerator<MonoBehaviour> generator,
			UFO_DifficultyController difficultyController,
			UFOGameCanvas ufoGameCanvas)
		{
			_difficultyController = difficultyController;
			_generator = generator;
			_player = player;
			_ufoGameCanvas = ufoGameCanvas;
			_player.Died += Die;
		}
		
		public override async Task OnEnter()
		{
			InputManager.CurrentInputManager.JumpAction.performed += PerformedActionSubscription;
			InputManager.CurrentInputManager.PauseAction.performed += PerformedActionSubscription;
			InputManager.CurrentInputManager.JumpAction.canceled += CanceledActionSubscription;
			if(_ufoGameCanvas != null) _ufoGameCanvas.gameObject.SetActive(true);
			await _generator.SwitchState(typeof(RunState));
		}

		public override async Task Update()
		{
			await _generator.CurrentState.Update();
		}
		
		public override Task OnExit()
		{
			InputManager.CurrentInputManager.JumpAction.performed -= PerformedActionSubscription;
			InputManager.CurrentInputManager.PauseAction.performed -= PerformedActionSubscription;
			InputManager.CurrentInputManager.JumpAction.canceled -= CanceledActionSubscription;
			if(_ufoGameCanvas != null) _ufoGameCanvas.gameObject.SetActive(false);
			return Task.CompletedTask;
		}

		private void PerformedActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.PauseAction)
				CurrentStateMachine.SwitchStateTo<PauseMachineState>();

			if (context.action == InputManager.CurrentInputManager.JumpAction)
			{
				_player.Accelerating(true);
			}
		}

		private void CanceledActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.JumpAction)
				_player.Accelerating(false);
		}
		
		private void Die(UFO player)
		{
			_player.Died -= Die;
			CurrentStateMachine.SwitchStateTo<GameOverMachineState>();
		}
	}
}