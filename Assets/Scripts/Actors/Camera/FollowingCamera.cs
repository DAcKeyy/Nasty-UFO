using Cinemachine;
using UnityEngine;

namespace Scenes.Actors.Camera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FollowingCamera : MonoBehaviour , ICamera
    {
        private CinemachineVirtualCamera _camera;
        
        public void Start()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            /*
            signalBus.Subscribe<PlayerDiedSignal>(x => FollowTarget(null));*/
        }
        
        public void FollowTarget(GameObject target)
        {
            if (target == null)
            {
                _camera.Follow = null;
                _camera.enabled = false;
                return;
            }
            
            _camera.enabled = true;
            _camera.Follow = target.transform;
        }
    }
}