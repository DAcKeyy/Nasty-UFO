using System.Collections.Generic;
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
using SceneBehavior.UFOGame.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame
{
	public class UFOMananger : GameManager
	{
		[SerializeField] private ModularBuilding _modularBuildingPrefab;
		[SerializeField] private LevelGenerationSettings_ScriptableObject _generationSettings;
		[SerializeField] private DifficultySettings_ScriptableObject _difficultySettings;
		[SerializeField] private InputActionAsset _UFOActionAsset;
		[SerializeField] private UFO _player;
		[SerializeField] private UFOMovement _movementComponent;
		private BusinessGarbageCollector _gc;
		private UFO_DifficultyController _difficultyController;
		
		private void Awake()
		{
			InputManager.CurrentInputManager ??= new InputManager(_UFOActionAsset);
			
			_generationSettings._settings._buildingsFactorySettings._modularBuildingPrefab = _modularBuildingPrefab;
			_generationSettings._settings._gameCamera = Camera.main;
			_generationSettings._settings._generationCenter = _player.transform;
			_generationSettings._settings._groundLevel = Vector3.zero;
			_difficultyController = new UFO_DifficultyController(_generationSettings, _difficultySettings, _player);
			
			var objectPool = new MonoPool<MonoBehaviour>();
			ObjectGenerator<MonoBehaviour> UFOObjectGenerator = new UFOObjectGenerator(ref objectPool, _difficultyController);
			_gc = new NastyUFOGC(new InRadiusStrategy(ref objectPool, _generationSettings._settings._clearingRange, _generationSettings._settings._generationCenter));
			
			
			StateMachine = new StateMachine(new List<State>()
			{
				new AwaitInputState(UFOObjectGenerator, _difficultyController),
				new GameOverState(),
				new GameRunState(_player, UFOObjectGenerator, _difficultyController),
				new PauseState()
			});
			
			var pauseState = StateMachine.GetState(typeof(PauseState)) as PauseState;
			pauseState.Exit += base.Exit;
			StateMachine.StateChanged += OnStateChanged;
			InvokeRepeating("UpdateGC", 1, _generationSettings._settings._levelUpdateRate);
			InvokeRepeating("UpdateGenerator", 0, _generationSettings._settings._levelUpdateRate);
			
			CancelInvoke("UpdateDifficulty");
		}

		private void Start()
		{
			StateMachine.SwitchState(typeof(AwaitInputState));
		}
		
		private void OnStateChanged(State currentState)
		{
			if (currentState.GetType() == typeof(GameOverState))
			{
				StateMachine.StateChanged -= OnStateChanged;
			}
			
			if (currentState.GetType() == typeof(GameRunState))
			{
				InvokeRepeating("UpdateDifficulty", _generationSettings._settings._levelDifficultyIncreaseRate, _generationSettings._settings._levelDifficultyIncreaseRate);
			}
		}

		private void UpdateGC()
		{
			_gc.DoJob().GetAwaiter().GetResult();
		}
		
		private void UpdateGenerator()
		{
			StateMachine.Update().GetAwaiter().GetResult();
		}

		private void UpdateDifficulty()
		{
			_difficultyController.UpdateDifficulty(Time.time);
		}

		protected override void Update()
		{
			//НЕ СТИРАТЬ, override батю
		}

		private void OnDisable()
		{
			var pauseState = StateMachine.GetState(typeof(PauseState)) as PauseState;
			pauseState.Exit -= base.Exit;
		}
	}
}