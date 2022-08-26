using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SceneBehavior.NastyUFOGame.Base
{
	public abstract class StateMachine
	{
		public GameState CurrentState { get; protected set; }
		protected List<GameState> MachineSatesList;

		public async Task SwitchState<T>() where T : GameState
		{
			var state = MachineSatesList.FirstOrDefault(gameState => gameState is T);
			
			if (state is T == false) throw new NotFiniteNumberException($"{typeof(T)} not initialized in states list");
			
			CurrentState.Exit();
			
			CurrentState = state;
			
			state.Enter();
		}
	}
}