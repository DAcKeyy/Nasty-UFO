using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace SceneContext.NastyUFOGame.Base
{
	public abstract class StateMachine: MonoBehaviour
	{
		protected GameState CurrentState;
		
		protected List<GameState> MachineSatesList;

		public async Task SwitchState<T>(T newState) where T : GameState
		{
			var state = MachineSatesList.FirstOrDefault(gameState => gameState is T);
			if (state is T == false) throw new NotFiniteNumberException($"{typeof(T)} not initialized in states list");
			await CurrentState.Stop();
			await state.Startup();
			CurrentState = state;
		}
	}
}