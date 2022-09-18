using System.Threading.Tasks;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.Loading.States;
using UI.Canvases.Menu;
using UnityEngine.SceneManagement;

namespace SceneBehavior.Menu.States
{
	public class MainMenuActiveMachineState : MachineState
	{
		private readonly MainMenuCanvas _mainCanvas;
		
		public MainMenuActiveMachineState(MainMenuCanvas mainCanvas)
		{
			_mainCanvas = mainCanvas;
		}

		public override Task OnEnter()
		{
			_mainCanvas.gameObject.SetActive(true);
			_mainCanvas.PlayButton.onClick.AddListener(GoToGame);
			_mainCanvas.DevelopersButton.onClick.AddListener(GoToDevs);
			_mainCanvas.SettingsButton.onClick.AddListener(GoToSettings);
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			if(_mainCanvas != null) _mainCanvas.gameObject.SetActive(false);
			return Task.CompletedTask;
		}

		private void GoToGame()
		{
			SceneManager.LoadSceneAsync("UFO Game");
			CurrentStateMachine.SwitchStateTo<LoadingGameMachineState>();
		}
		private void GoToDevs()
		{
			//todo он в потоке, он в моменте
			CurrentStateMachine.SwitchStateTo<MachineState>();
		}
		private void GoToSettings()
		{
			CurrentStateMachine.SwitchStateTo<SettingsMenuMachineState>();
		}
	}
}