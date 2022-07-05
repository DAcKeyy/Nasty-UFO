using UnityEngine;

namespace Actors.Camera
{
    public interface ICamera
    {
        public void FollowTarget(GameObject target);
    }
}