using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miscellaneous.StateMachines.Base
{
	public class StateMachine
	{
		protected List<State> StateList;
		protected State CurrentState;
		
		public StateMachine(List<State> stateList)
		{
			StateList = stateList;

			foreach (var state in StateList)
			{
				state.SetStateMachine(this);
			}
		}
		
		//Logic

		public virtual async Task Update()
		{
			if(CurrentState == null) throw new Exception($"{GetType().Name} Current State is null");
			await CurrentState.Update();
		}
		
		
		public void SwitchState(Type newState)
		{
			var state = StateList.Find(x => x.GetType() == newState);
			
			if (state != null)
			{
				if(CurrentState != null) CurrentState.OnExit();
				
				CurrentState = state;
				
				CurrentState.OnEnter();
			}
			else
			{
				throw new Exception($"{newState.Name} not initialized in states list");
			}
		}
	}
}