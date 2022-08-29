using System.Collections.Generic;
using Actors.Movement;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
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
		[SerializeField] private LevelGenerationSettings_ScriptableObject _generalSettings;
		[SerializeField] private InputActionAsset _UFOActionAsset;
		[SerializeField] private UFO _player;
		[SerializeField] private UFOMovement _movementComponent;
		private BusinessGarbageCollector _gc;
		
		private void Awake()
		{
			InputManager.CurrentInputManager ??= new InputManager(_UFOActionAsset);
			
			_generalSettings._settings._buildingsFactorySettings._modularBuildingPrefab = _modularBuildingPrefab;
			_generalSettings._settings._gameCamera = Camera.main;
			_generalSettings._settings._generationCenter = _player.transform;
			
			var objectPool = new MonoPool<MonoBehaviour>();
			ObjectGenerator<MonoBehaviour> UFOObjectGenerator = new UFOObjectGenerator(ref objectPool, _generalSettings._settings);
			_gc = new NastyUFOGC(new InRadiusStrategy(ref objectPool, _generalSettings._settings._clearingRange, _generalSettings._settings._generationCenter));
			
			StateMachine = new StateMachine(new List<State>()
			{
				new AwaitInputState(UFOObjectGenerator),
				new GameOverState(),
				new GameRunState(_player, UFOObjectGenerator),
				new PauseState()
			});
			
			var pauseState = StateMachine.GetState(typeof(PauseState)) as PauseState;
			pauseState.Exit += base.Exit;

			InvokeRepeating("UpdateGC", 0, _generalSettings._settings._levelUpdateRate);
			InvokeRepeating("UpdateGenerator", 0, _generalSettings._settings._levelUpdateRate);
		}

		private void UpdateGC()
		{
			_gc.DoJob().GetAwaiter().GetResult();
		}
		
		private void UpdateGenerator()
		{
			StateMachine.Update().GetAwaiter().GetResult();
		}

		protected override void Update()
		{
			//НЕ СТИРАТЬ, override батю
		}

		private void Start()
		{
			StateMachine.SwitchState(typeof(AwaitInputState));
		}

		private void OnDisable()
		{
			var pauseState = StateMachine.GetState(typeof(PauseState)) as PauseState;
			pauseState.Exit -= base.Exit;
		}
	}
}