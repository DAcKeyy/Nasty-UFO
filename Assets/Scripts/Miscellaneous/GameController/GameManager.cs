using Miscellaneous.Saving;
using Miscellaneous.StateMachines.Base;
using UnityEngine;

namespace Miscellaneous.GameController
{
    public abstract class GameManager : MonoBehaviour
    {
        protected StateMachine SceneStateMachine = new StateMachine();

        protected virtual async void Update()
        {
            await SceneStateMachine.Update();
        }

        public void OnDestroy()
        {
            SceneStateMachine.ShutDown();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Debug.Log($"Pause status: {pauseStatus}");
            if(pauseStatus) GlobalPlayerPrefs.IsItAllReadyLunched = false;
            else GlobalPlayerPrefs.IsItAllReadyLunched = true;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if(hasFocus) GlobalPlayerPrefs.IsItAllReadyLunched = true;
        }

        private void OnApplicationQuit()
        {
            Exit();
        }

        public virtual void Exit()
        {
            GlobalPlayerPrefs.IsItAllReadyLunched = false;
            Application.Quit();
        }
    }
}
