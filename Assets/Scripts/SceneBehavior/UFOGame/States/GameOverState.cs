using System.Threading.Tasks;
using Miscellaneous.StateMachines.Base;
using UI.Canvases;
using UnityEngine;

namespace SceneBehavior.UFOGame.States
{
	public class GameOverState : State
	{
		private UFOGameOverCanvas _ufoGameOverCanvas;
		public GameOverState(UFOGameOverCanvas ufoGameOverCanvas)
		{
			_ufoGameOverCanvas = ufoGameOverCanvas;
		}
		
		
		public override Task OnEnter()
		{
			Debug.Log("Игра закончилась");
			_ufoGameOverCanvas.gameObject.SetActive(true);
			//TODO UI
			
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			_ufoGameOverCanvas.gameObject.SetActive(false);
			
			return Task.CompletedTask;
		}
	}
}