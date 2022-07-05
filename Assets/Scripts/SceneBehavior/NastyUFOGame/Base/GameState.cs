using System.Threading.Tasks;

namespace SceneBehavior.NastyUFOGame.Base
{
	public abstract class GameState
	{
		public GameState()
		{

		}

		public async virtual Task Startup()
		{
			
		}

		public async virtual Task Pause()
		{
			
		}	

		public async virtual Task Stop()
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