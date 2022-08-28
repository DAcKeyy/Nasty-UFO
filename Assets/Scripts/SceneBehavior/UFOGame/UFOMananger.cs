using System.Collections.Generic;
using Actors.Movement;
using Actors.NastyUFO;
using Data.Generators;
using Input;
using Miscellaneous.GameController;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.NastyUFOGame;
using SceneBehavior.UFOGame.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame
{
	public class UFOMananger : GameManager
	{
		[SerializeField] private LevelGenerationSettings_ScriptableObject _generalSettings;
		[SerializeField] private InputActionAsset _UFOActionAsset;
		[SerializeField] private UFO _player;
		[SerializeField] private UFOMovement _movementComponent;
		private UFOGameSystem _theGame;
		
		private void Awake()
		{
			InputManager.CurrentInputManager ??= new InputManager(_UFOActionAsset);

			_theGame = new UFOGameSystem(_generalSettings._settings, _player, _movementComponent, Camera.main);
			
			StateMachine = new StateMachine(new List<State>()
			{
				new AwaitInputState(),
				new GameOverState(),
				new GameRunState(_theGame)
			});
		}

		private void Start()
		{
			StateMachine.SwitchState(typeof(AwaitInputState));
			
			_theGame.Start();
		}
	}
}