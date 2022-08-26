using System.Threading.Tasks;

namespace SceneBehavior.NastyUFOGame.Base
{
	public abstract class GameState
	{
		protected bool IsActive;
		
		public virtual void Enter()
		{
			IsActive = true;//!!!!!!!!

		}

		public virtual void Pause()
		{

		}	

		public virtual void Exit()
		{
			IsActive = false;//!!!!!!!!

		}

		public virtual void Jump()
		{

		}
		
		public virtual void Reset()
		{

		}

		public override string ToString()
		{
			return this.GetType().Name;
		}
	}
}