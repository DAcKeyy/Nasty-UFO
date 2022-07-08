using System.Collections.Generic;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Contexts.NastyUFO;
using Miscellaneous.Pools;
using SceneBehavior.NastyUFOGame.Base;
using SceneBehavior.NastyUFOGame.GameStates;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame
{
	public class UFOGameSystem : StateMachine
	{
		private readonly LevelGenerator<MonoBehaviour> _ufoLevelGenerator;
		//TODO Можно сделать счётки созданных обектов, хз, по проекту это не надо (но можно)
		private readonly MonoPool<MonoBehaviour> _objectPool;
		
		//TODO Прокидывать сюда UI
		public UFOGameSystem(
			NastyUFOLevelGeneration_Settings settings, 
			UFO player,
			Camera camera 
			/*, UISetting uiSettings*/)
		{
			_objectPool = new MonoPool<MonoBehaviour>();
			_ufoLevelGenerator = new NastyUFOLevelGenerator(ref _objectPool, settings, player, camera);
			MachineSatesList = new List<GameState>()
			{
				new GameStartup_State(ref _ufoLevelGenerator, this, settings, ref _objectPool, player),
				new GameLunched_State(ref _ufoLevelGenerator, settings, player, ref _objectPool),
				new GameEnded_State(player)
			};
			
			CurrentState = MachineSatesList[0];
		}

		public async void Start() => await CurrentState.Enter();
		
		public async void Pause() => await CurrentState.Pause();
		
		public async void Stop() => await CurrentState.Exit();
		
		public async void Jump() => await CurrentState.Jump();
	}
}