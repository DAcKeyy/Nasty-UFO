using System.Collections.Generic;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Generators.NastyUFO;
using Miscellaneous.Pools;
using SceneBehavior.NastyUFOGame.Base;
using SceneBehavior.NastyUFOGame.GameStates;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame
{
	public class UFOGameSystem : StateMachine
	{
		//TODO Можно сделать счётки созданных обектов, хз, по проекту это не надо (но можно)

		//TODO Прокидывать сюда UI
		public UFOGameSystem(
			NastyUFOLevelGeneration_Settings settings, 
			UFO player
			/*, UISetting uiSettings*/)
		{
			settings._generationCenter = player.transform;
			var objectPool = new MonoPool<MonoBehaviour>();
			ObjectGenerator<MonoBehaviour> ufoObjectGenerator = new NastyUfoObjectGenerator(ref objectPool, settings);
			
			MachineSatesList = new List<GameState>()
			{
				new GameStartup_State(ref ufoObjectGenerator, this, settings, ref objectPool, player),
				new GameLunched_State(ref ufoObjectGenerator, settings, player, ref objectPool),
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