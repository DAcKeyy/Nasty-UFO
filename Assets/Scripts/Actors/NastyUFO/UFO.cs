using Actors.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace Actors.NastyUFO
{
    [SelectionBase]
    [RequireComponent(typeof(UFOMovement))]
    public class UFO : MonoBehaviour
    {
        public bool IsAlive => !_isDied;
        
        [SerializeField] 
        private UnityEvent _dieEvent;
        private bool _isDied;

        private void Start()
        {
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

        public void Die()
        {
            if(_isDied) return;
            
            _dieEvent.Invoke();
            
            _isDied = true;
            
            //TODO Анимация смээрти
            
            GetComponent<UFOMovement>().enabled = false;
            
            //GetComponent<Collider2D>().isTrigger = true;
            
            //_signalBus.TryFire<PlayerDiedSignal>();
            
        }
    }
}
