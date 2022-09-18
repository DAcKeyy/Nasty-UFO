using Miscellaneous.GameController;
using Miscellaneous.Saving;
using SceneBehavior.Loading.States;
using SceneBehavior.Menu.States;
using UI.Canvases.Loading;
using UI.Canvases.Menu;
using UnityEngine;

namespace SceneBehavior.Menu
{
	public class MenuManager : GameManager
	{
		[SerializeField] private MainMenuCanvas _mainMenuCanvas;
		[SerializeField] private LoadingCanvas _loadingCanvas;
		
		private void Awake()
		{
			SceneStateMachine.AddState(new IntroMachineState());
			SceneStateMachine.AddState(new MainMenuActiveMachineState(_mainMenuCanvas));
			SceneStateMachine.AddState(new SettingsMenuMachineState());
			SceneStateMachine.AddState(new LoadingGameMachineState(_loadingCanvas));
		}

		private void OnEnable()
		{
			if (GlobalPlayerPrefs.IsItAllReadyLunched) SceneStateMachine.SwitchStateTo<MainMenuActiveMachineState>();
			else SceneStateMachine.SwitchStateTo<IntroMachineState>();
		}
	}
}