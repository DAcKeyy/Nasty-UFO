using UnityEngine;

namespace SceneContext.NastyUFOGame.Base
{
	public abstract class StateMachine: MonoBehaviour
	{
		protected GameState State;

		public void SetState(GameState newState)
		{
			State = newState;
			StartCoroutine(State.Startup());
		}
	}
}