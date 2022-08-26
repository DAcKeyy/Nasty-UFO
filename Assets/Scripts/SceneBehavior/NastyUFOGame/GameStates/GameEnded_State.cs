using Actors.NastyUFO;
using SceneBehavior.NastyUFOGame.Base;
using Task = System.Threading.Tasks.Task;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameEnded_State : GameState
	{
		private readonly UFO _player;
		
		public GameEnded_State(UFO player)
		{
			_player = player;
		}


		public override void Enter()
		{
			_player.Die();
			
			//TODO Получить доступ к UI, вывести "gameover"
			
		}

		public override void Exit()
		{
			//TODO Выход из игры

		}

		public override void Reset()
		{
			//TODO начало новой игры..
			
		}
	}
}