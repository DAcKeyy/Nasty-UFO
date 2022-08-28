using System;
using System.Collections.Generic;
using Miscellaneous.GameController;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.Menu.States;

namespace SceneBehavior.Menu
{
	public class MenuManager : GameManager
	{
		private void Awake()
		{
			StateMachine = new StateMachine(new List<State>()
			{
				new IntroState(),
				new MainMenuActiveState(),
				new SettingsMenuState(),
				new LoadingGameState()
			});
		}

		private void Start()
		{
			StateMachine.SwitchState(typeof(IntroState));
		}
	}
}