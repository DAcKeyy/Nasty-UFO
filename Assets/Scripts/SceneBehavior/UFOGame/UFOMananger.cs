using Actors.Movement;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Difficulty;
using Data.Generators;
using Generation.GarbageCollection.NastyUFO;
using Generation.GarbageCollection.NastyUFO.Strategies;
using Generation.Generators.NastyUFO;
using Input;
using Miscellaneous.GameController;
using Miscellaneous.GC;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.Loading.States;
using SceneBehavior.UFOGame.States;
using TMPro;
using UI.Canvases;
using UI.Canvases.Loading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame
{
	public class UFOMananger : GameManager
	{
		[SerializeField] private PlayerInput _playerInput;
		[SerializeField] private UFOGameCanvas _gameCanvas;
		[SerializeField] private UFOGameOverCanvas _gameOverCanvas;
		[SerializeField] private UFOPauseCanvas _gamePauseCanvas;
		[SerializeField] private AwaitInputCanvas _awaitCanvas;
		[SerializeField] private LoadingCanvas _loadingCanvas;
		[SerializeField] private ModularBuilding _modularBuildingPrefab;
		[SerializeField] private LevelGenerationSettings_ScriptableObject _generationSettings;
		[SerializeField] private DifficultySettings_ScriptableObject _difficultySettings;
		[SerializeField] private InputActionAsset _UFOActionAsset;
		[SerializeField] private UFO _player;
		[SerializeField] private UFOMovement _movementComponent;
		private BusinessGarbageCollector _gc;
		private UFO_DifficultyController _difficultyController;
		private float _awaitTime = 0f;

		private void Awake()
		{
			InputManager.CurrentInputManager ??= new InputManager(_UFOActionAsset, _playerInput);
			
			_generationSettings._settings._buildingsFactorySettings._modularBuildingPrefab = _modularBuildingPrefab;
			_generationSettings._settings._gameCamera = Camera.main;
			_generationSettings._settings._generationCenter = _player.transform;
			_generationSettings._settings._groundLevel = Vector3.zero;
			_difficultyController = new UFO_DifficultyController(_generationSettings, _difficultySettings, _player);
			
			var objectPool = new MonoPool<MonoBehaviour>();
			ObjectGenerator<MonoBehaviour> UFOObjectGenerator = new UFOObjectGenerator(ref objectPool, _difficultyController);
			_gc = new NastyUFOGC(new InRadiusStrategy(ref objectPool, _generationSettings._settings._clearingRange, _generationSettings._settings._generationCenter));
			
			SceneStateMachine.AddState(new AwaitInputMachineState(UFOObjectGenerator, _difficultyController, _awaitCanvas));
			SceneStateMachine.AddState(new GameOverMachineState(_gameOverCanvas));
			SceneStateMachine.AddState(new GameRunMachineState(_player, UFOObjectGenerator, _difficultyController, _gameCanvas));
			SceneStateMachine.AddState(new PauseMachineState(_gamePauseCanvas));
			SceneStateMachine.AddState(new LoadingGameMachineState(_loadingCanvas));
			
			SceneStateMachine.GetState<PauseMachineState>().GameExit += base.Exit;
			
			SceneStateMachine.StateChanged += OnSceneStateChanged;
			
			InvokeRepeating("UpdateGC", 1, _generationSettings._settings._levelUpdateRate);
			InvokeRepeating("UpdateGenerator", 0, _generationSettings._settings._levelUpdateRate);
		}

		private void Start()
		{
			SceneStateMachine.SwitchStateTo<AwaitInputMachineState>();
		}
		
		private void OnSceneStateChanged(MachineState currentMachineState)
		{
			if (currentMachineState.GetType() == typeof(GameOverMachineState))
			{
				SceneStateMachine.StateChanged -= OnSceneStateChanged;
			}
			
			if (currentMachineState.GetType() == typeof(GameRunMachineState))
			{
				if(_awaitTime == 0f) _awaitTime = Time.time;
				InvokeRepeating("UpdateDifficulty", _generationSettings._settings._levelDifficultyIncreaseRate, _generationSettings._settings._levelDifficultyIncreaseRate);
			}
		}

		private void UpdateGC()
		{
			_gc.DoJob().GetAwaiter().GetResult();
		}
		
		private void UpdateGenerator()
		{
			SceneStateMachine.Update().GetAwaiter().GetResult();
		}

		private void UpdateDifficulty()
		{
			_difficultyController.UpdateDifficulty(Time.time - _awaitTime);
		}

		protected override void Update()
		{
			//НЕ СТИРАТЬ, override батю
		}

		private void OnDisable()
		{
			var pauseState = SceneStateMachine.GetState<PauseMachineState>();
			pauseState.GameExit -= base.Exit;
		}
	}
}