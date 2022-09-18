using System.Threading.Tasks;
using Miscellaneous.Saving;
using Miscellaneous.StateMachines.Base;
using UnityEngine;

namespace SceneBehavior.Menu.States
{
	public class IntroMachineState : MachineState
	{
		public override Task OnEnter()
		{
			CurrentTask = Task.Delay(1000); //TODO Запуск интро
			
			while (CurrentTask.IsCompletedSuccessfully == false)
			{

			}
			
			CurrentStateMachine.SwitchStateTo<MainMenuActiveMachineState>();
			
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			GlobalPlayerPrefs.IsItAllReadyLunched = true;
			return Task.CompletedTask;
		}
	}
}