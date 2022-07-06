﻿using System;
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
		[SerializeField] private Camera _mainCamera;
		private NastyUFOLevelGeneration_Settings _settings;
		protected ILevelGenerator _UFOLevelGenerator;
		
		//TODO InputAction вынести в отдельный для обработки класс и дёргать методы тут от туда
		private InputAction _jumpAction;
		private InputAction _pauseAction;

		private void Awake()
		{
			_settings = Resources.Load<LevelGenerationSettings_ScriptableObject>("Data/Settings/Level Generation Settings")._settings;
			_UFOLevelGenerator = new NastyUFOLevelGenerator(_settings, _player, _mainCamera);
			
			//TODO InputAction вынести в отдельный для обработки класс и дёргать методы тут от туда
			_jumpAction = _inputActionAsset.FindActionMap("Game").FindAction("Jump");
			_pauseAction = _inputActionAsset.FindActionMap("Game").FindAction("Pause");
			_jumpAction.performed += context => OnJump();
			_pauseAction.performed += context => OnPause();

			MachineSatesList = new List<GameState>()
			{
				new GameStartup_State(_UFOLevelGenerator, this, _settings),
				new GameLunched_State(_UFOLevelGenerator, _settings, _player),
				new GameEnded_State(_player)
			};

			CurrentState = MachineSatesList[0];
		}

		private void Start() => OnStart();

		public async void OnStart() => await CurrentState.Enter();

		public async void OnPause() => await CurrentState.Pause();
		
		public async void OnStop() => await CurrentState.Exit();
		
		public async void OnJump() => await CurrentState.Jump();
	}
}