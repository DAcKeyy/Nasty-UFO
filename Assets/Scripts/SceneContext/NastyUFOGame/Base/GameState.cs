using System.Collections;

namespace SceneContext.NastyUFOGame.Base
{
	public abstract class GameState
	{
		protected UFOGameSystem GameSystem;

		public GameState(UFOGameSystem statesGameSystem)
		{
			GameSystem = statesGameSystem;
		}

		public virtual IEnumerator Startup()
		{
			yield break;
		}

		public virtual IEnumerator Pause()
		{
			yield break;
		}	

		public virtual IEnumerator Stop()
		{
			yield break;
		}

		public virtual IEnumerator Jump()
		{
			yield break;
		}
	}
}