using System.Collections;
using Actors.NastyUFO;
using SceneContext.NastyUFOGame.Base;
using UnityEditor.VersionControl;
using Task = System.Threading.Tasks.Task;

namespace SceneContext.NastyUFOGame.GameStates
{
	public class GameEnded_State : GameState
	{
		private UFO _player;
		
		public GameEnded_State(UFO player) : base()
		{
			_player = player;
		}


		public override Task Startup()
		{
			_player.Die();
			
			//TODO Получить доступ к UI, вывести "gameover"
			
			return Task.CompletedTask;
		}

		public override Task Stop()
		{
			//TODO Выход из игры
			return Task.CompletedTask;
		}

		public override Task Reset()
		{
			//TODO начало новой игры..
			
			return Task.CompletedTask;
		}
	}
}