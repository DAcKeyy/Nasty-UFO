using System.Threading.Tasks;
using Miscellaneous.StateMachines.Base;
using UnityEngine;

namespace SceneBehavior.UFOGame.States
{
	public class GameOverState : State
	{
		public GameOverState()
		{
			//TODO UI
		}
		
		
		public override Task OnEnter()
		{
			Debug.Log("Игра закончилась");
			
			//TODO UI
			
			return Task.CompletedTask;
		}
	}
}