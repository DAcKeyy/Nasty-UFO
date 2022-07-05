using System.Collections.Generic;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Contexts.NastyUFO;
using SceneBehavior.NastyUFOGame.Base;
using SceneBehavior.NastyUFOGame.GameStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.NastyUFOGame
{
	public class UFOGameSystem : StateMachine
	{
		[SerializeField] private InputActionAsset _inputActionAsset;
		[SerializeField] private UFO _player;
		private NastyUFOLevelGeneration_Settings _settings;
		protected ILevelGenerator _UFOLevelGenerator;
		
		//TODO InputAction вынести в отдельный для обработки класс и дёргать методы тут от туда
		private InputAction _jumpAction;
		private InputAction _pauseAction;

		private void Awake()
		{
			_settings = Resources.Load<LevelGenerationSettings_ScriptableObject>("Data/Settings/Level Generation Settings")._settings;
			_UFOLevelGenerator = new NastyUFOLevelGenerator(_settings, _player, Camera.main);
			
			//TODO InputAction вынести в отдельный для обработки класс и дёргать методы тут от туда
			_jumpAction = _inputActionAsset.FindActionMap("Game").FindAction("Jump");
			_pauseAction = _inputActionAsset.FindActionMap("Game").FindAction("Pause");
			_jumpAction.performed += context => OnJump();
			_pauseAction.performed += context => OnPause();

			MachineSatesList = new List<GameState>()
			{
				new Startup_State(_UFOLevelGenerator),
				new WaitForInput_State(),
				new GameLunched_State(_UFOLevelGenerator, _settings),
				new GameEnded_State(_player)
			};
		}

		private void Start()
		{
			SwitchState(MachineSatesList[0]);
		}

		public void OnStart() => CurrentState.Startup();

		public void OnPause() => CurrentState.Pause();
		
		public void OnStop() => CurrentState.Stop();
		
		public void OnJump() => CurrentState.Jump();
	}
}