using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miscellaneous.StateMachines.Base
{
	public sealed class StateMachine
	{
		public MachineState CurrentMachineState { get; private set; }
		public event Action<MachineState> StateChanged = delegate(MachineState state) {  };
		private readonly List<MachineState> _statesList;

		public StateMachine()
		{
			_statesList = new List<MachineState>();
		}

		public async Task Update()
		{
			if(CurrentMachineState == null) throw new Exception($"{GetType().Name} Current State is null");
			await CurrentMachineState.Update();
		}

		public void AddState<T>(T newState) where T : MachineState
		{
			CurrentMachineState ??= newState;
			_statesList.Add(newState);
			newState.SetStateMachine(this);
		}
		
		public T GetState<T>() where T : MachineState
		{
			var state = _statesList.Find(x => x.GetType() == typeof(T));
			if (state != null) return state as T;
			else throw new Exception($"{typeof(T).BaseType} not initialized in states list");
		}

		public async void SwitchStateTo<T>() where T : MachineState
		{
			var state = _statesList.Find(x => x.GetType() == typeof(T));
			
			if (state != null)
			{
				if(CurrentMachineState != null) await CurrentMachineState.OnExit();
				
				CurrentMachineState = state;
				StateChanged(CurrentMachineState);
				await CurrentMachineState.OnEnter();
			}
			else
			{
				throw new Exception($"{typeof(T).BaseType} not initialized in states list");
			}
		}

		public async void ShutDown()
		{
			await CurrentMachineState.OnExit();
			CurrentMachineState = null;
		}
	}
}