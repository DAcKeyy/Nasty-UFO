using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Base
{
    public abstract class ObjectGenerator<T> where T : MonoBehaviour //То что возврощает
    {
        private MonoPool<T> _pool;
        public List<GeneratorState> StatesList { get; protected set; }
        public GeneratorState CurrentState { get; protected set; }

        //У него есть пул и в него он срёт контекстную генерацию по заданной логике
        protected ObjectGenerator(ref MonoPool<T> pool)
        {
            _pool = pool;
        }

        public virtual async Task Create()
        {
            if (CurrentState == null) throw new Exception("Где стейт в генераторе?");
            await CurrentState.Create();
            Debug.Log("Создаю хуйню");
        }

        public virtual async Task Update()
        {
            if (CurrentState == null) throw new Exception("Где стейт в генераторе?");
            await CurrentState.Update();
            Debug.Log("Обновляю хуйню");
        }

        public void SwitchState<TStateType>(TStateType newState) where TStateType : GeneratorState
        {
            var state = StatesList.FirstOrDefault(state => state is TStateType);
            if (state == null) throw new NotFiniteNumberException($"{typeof(T)} not initialized in states list");
            
            //TODO Если текущий стейт не выполнил работу то await
            CurrentState = newState;
        }
    }
}