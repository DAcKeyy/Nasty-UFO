using System;
using Actors.Base;
using Actors.Movement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Actors.NastyUFO
{
    [SelectionBase]
    [RequireComponent(typeof(UFOMovement))]
    public class UFO : MonoBehaviour, ICrushable
    {
        public Action<UFO> Died = delegate(UFO ufo) {  };
        private bool _isDied;

        private void Start()
        {
            Died += dsd => _isDied = true;
            /*
            _signalBus.Subscribe<BuildingTouchedSignal>(x => {
                //TODO Исправить двойную проверку что тут, что в доме
                if (x.CollisionObj2D.collider.GetComponent<UFO>() != null)
                {
                    Die();
                }
            });
            
            _signalBus.Subscribe<LandTouchedSignal>(x => {
                //TODO Исправить двойную проверку что тут, что на земле
                if (x.CollisionObj2D.collider.GetComponent<UFO>() != null)
                {
                    Die();
                }
            });
            */
        }

        public void BeginSweeping()
        {
            GetComponent<UFOMovement>().ChangeState(2);
        }
        
        public void Die()
        {
            if(_isDied) return;
            
            Died(this);

            //TODO Анимация смээрти
            
            GetComponent<UFOMovement>().enabled = false;
            
            //GetComponent<Collider2D>().isTrigger = true;
            
            //_signalBus.TryFire<PlayerDiedSignal>();
            
        }

        public void Crush()
        {
            //TODO Логика ломания
        }
    }
}
