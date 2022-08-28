using System.Collections.Generic;
using Actors.Movement;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Generators.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using SceneBehavior.NastyUFOGame.Base;
using SceneBehavior.NastyUFOGame.GameStates;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame
{
	public class UFOGameSystem : StateMachine
	{
		public UFOGameSystem(
			NastyUFOLevelGeneration_Settings settings, 
			UFO player, UFOMovement ufoMovement, Camera gameCamera)
		{
			settings._gameCamera = gameCamera;
			settings._generationCenter = player.transform;
			var objectPool = new MonoPool<MonoBehaviour>();
			
			ObjectGenerator<MonoBehaviour> ufoObjectGenerator = new UFOObjectGenerator(ref objectPool, settings);
			
			MachineSatesList = new List<GameState>() {
				new GameStartup_State(ref ufoObjectGenerator, this, settings, ref objectPool, player),
				new GameLunched_State(ref ufoObjectGenerator, settings, player, ufoMovement, this, ref objectPool),
				new GameEnded_State(player)
			};

			CurrentState = MachineSatesList[0];
		}

		public void Start() =>  CurrentState.Enter();
		
		public void Pause() =>  CurrentState.Pause();
		
		public void Stop() =>  CurrentState.Exit();
		
		public void Jump() =>  CurrentState.Jump();
	}
}