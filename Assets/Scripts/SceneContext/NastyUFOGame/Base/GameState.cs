using System.Collections;
using System.Threading.Tasks;

namespace SceneContext.NastyUFOGame.Base
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