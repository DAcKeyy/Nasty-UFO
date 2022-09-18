using System.Threading.Tasks;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.Loading.States;
using UI.Canvases;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneBehavior.UFOGame.States
{
	public class GameOverMachineState : MachineState
	{
		private UFOGameOverCanvas _ufoGameOverCanvas;

		public GameOverMachineState(UFOGameOverCanvas ufoGameOverCanvas)
		{
			_ufoGameOverCanvas = ufoGameOverCanvas;
		}
		
		public override Task OnEnter()
		{
			_ufoGameOverCanvas.gameObject.SetActive(true);
			_ufoGameOverCanvas.OkButton.onClick.AddListener(GoToMenu);
			//TODO _ufoGameOverCanvas.RestartButton.onClick.AddListener(RestartScene);
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			_ufoGameOverCanvas.gameObject.SetActive(false);
			return Task.CompletedTask;
		}

		private void RestartScene()
		{
			SceneManager.LoadSceneAsync("UFO Game");
			CurrentStateMachine.SwitchStateTo<LoadingGameMachineState>();
		}

		private void GoToMenu()
		{
			SceneManager.LoadSceneAsync("MainMenu");
			CurrentStateMachine.SwitchStateTo<LoadingGameMachineState>();
		}
	}
}