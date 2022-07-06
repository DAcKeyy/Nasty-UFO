using Cinemachine;
using Data.Generators;
using UnityEngine;

namespace GameCamera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FollowingCamera : MonoBehaviour , ICamera
    {
        private CinemachineVirtualCamera _camera;
        private NastyUFOLevelGeneration_Settings _settings;
        
        private void Start()
        {
            _settings = Resources.Load<LevelGenerationSettings_ScriptableObject>("Data/Settings/Level Generation Settings")._settings;
            _camera = GetComponent<CinemachineVirtualCamera>();
            
            //signalBus.Subscribe<PlayerDiedSignal>(x => FollowTarget(null));
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
        
        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _settings._clearingRange);
        }
        
    }
}