using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame.Base
{
	public abstract class StateMachine: MonoBehaviour
	{
		protected GameState CurrentState;
		
		protected List<GameState> MachineSatesList;

		public async Task SwitchState<T>() where T : GameState
		{
			var state = MachineSatesList.FirstOrDefault(gameState => gameState is T);
			Debug.Log($"Try to new game state: {state.GetType().BaseType}");
			if (state is T == false) throw new NotFiniteNumberException($"{typeof(T)} not initialized in states list");
			await CurrentState.Exit();
			await state.Enter();
			CurrentState = state;
		}
	}
}