using System.Threading.Tasks;

namespace SceneBehavior.NastyUFOGame.Base
{
	public abstract class GameState
	{
		protected bool IsActive;
		
		public virtual Task Enter()
		{
			IsActive = true;//!!!!!!!!
			return Task.CompletedTask;
		}

		public virtual Task Pause()
		{
			return Task.CompletedTask;
		}	

		public virtual Task Exit()
		{
			IsActive = false;//!!!!!!!!
			return Task.CompletedTask;
		}

		public virtual Task Jump()
		{
			return Task.CompletedTask;
		}
		
		public virtual Task Reset()
		{
			return Task.CompletedTask;
		}
	}
}