using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Miscellaneous.StateMachines.Base
{
	[Serializable]
	public class State
	{
		protected StateMachine CurrentStateMachine;
		protected Task CurrentTask = Task.CompletedTask;
		public void SetStateMachine(StateMachine currentStateMachine)
		{
			CurrentStateMachine = currentStateMachine;
		}
		
		public virtual Task OnEnter()
		{
			while (CurrentTask.IsCompletedSuccessfully == false)
			{
				Debug.Log("Awaitng");
			}
			
			Debug.Log($"Enter {GetType().Name}");
			return Task.CompletedTask;
		}

		public virtual Task Update()
		{
			while (CurrentTask.IsCompletedSuccessfully == false)
			{
				Debug.Log("Awaitng");
			}
			
			Debug.Log($"Update {GetType().Name}");
			return Task.CompletedTask;
		}
		
		public virtual Task OnExit()
		{
			while (CurrentTask.IsCompletedSuccessfully == false)
			{
				Debug.Log("Awaitng");
			}
			
			Debug.Log($"Exit {GetType().Name}");
			return Task.CompletedTask;
		}
	}
}