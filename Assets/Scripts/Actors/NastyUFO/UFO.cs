using System;
using Actors.Base;
using Actors.Movement;
using UnityEngine;

namespace Actors.NastyUFO
{
    [SelectionBase]
    [RequireComponent(typeof(UFOMovement))]
    public class UFO : MonoBehaviour, ICrushable
    {
        public Action<UFO> Died = delegate(UFO ufo) {  };
        
        private bool _isDied;
        private UFOMovement _movement;

        private void Start()
        {
            Died += ufo => _isDied = true;
            _movement = GetComponent<UFOMovement>();
        }

        public void Accelerating(bool isAccelerate)
        {
            _movement.ChangeState(UFOMovement.MovementState.Falling);
            
            if(isAccelerate) _movement.StartAcceleration();
            else _movement.StopAcceleration();
        }

        public void Die()
        {
            if(_isDied) return;
            
            Died(this);

            //TODO Анимация смээрти
            
            _movement.enabled = false;
        }

        public void Crush(Collision collision)
        {
            _movement.enabled = false;
            
            Die();
        }

        public void ChangeFlySpeed(float multiplier) => _movement.SpeedMultiplier = multiplier;
    }
}
