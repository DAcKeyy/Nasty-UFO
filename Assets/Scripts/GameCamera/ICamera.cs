using UnityEngine;

namespace GameCamera
{
    public interface ICamera
    {
        public void FollowTarget(GameObject target);
    }
}