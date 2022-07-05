using System.Threading.Tasks;

namespace SceneBehavior.NastyUFOGame.Base
{
	public abstract class GameState
	{
		public GameState()
		{

		}

		public async virtual Task Enter()
		{
			
		}

		public async virtual Task Pause()
		{
			
		}	

		public async virtual Task Exit()
		{
			
		}

		public async virtual Task Jump()
		{
			
		}
		
		public async virtual Task Reset()
		{
			
		}
	}
}