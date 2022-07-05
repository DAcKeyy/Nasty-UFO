using UnityEngine;

namespace SceneContext.UFO.Base
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