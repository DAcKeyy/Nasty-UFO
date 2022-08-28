using System.Threading.Tasks;
using Miscellaneous.StateMachines.Base;
using UnityEngine;

namespace SceneBehavior.Menu.States
{
	public class IntroState : State
	{
		public override async Task Update()
		{
			CurrentTask = Task.Delay(3000);
			
			while (CurrentTask.IsCompletedSuccessfully == false)
			{
				Debug.Log("Awaitng");
			}
			
			CurrentStateMachine.SwitchState(typeof(MainMenuActiveState));
		}
	}
}