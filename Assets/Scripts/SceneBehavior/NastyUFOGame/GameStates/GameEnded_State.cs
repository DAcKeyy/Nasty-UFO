using Actors.NastyUFO;
using SceneBehavior.NastyUFOGame.Base;
using Task = System.Threading.Tasks.Task;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameEnded_State : GameState
	{
		private UFO _player;
		
		public GameEnded_State(UFO player) : base()
		{
			_player = player;
		}


		public override Task Enter()
		{
			_player.Die();
			
			//TODO Получить доступ к UI, вывести "gameover"
			
			return Task.CompletedTask;
		}

		public override Task Exit()
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