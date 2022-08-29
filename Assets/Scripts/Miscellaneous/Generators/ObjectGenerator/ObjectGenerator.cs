using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Miscellaneous.Pools;
using UnityEngine;

namespace Miscellaneous.Generators.ObjectGenerator
{
    public abstract class ObjectGenerator<T> where T : MonoBehaviour //То что возвращает
    {
        protected MonoPool<T> MonoPool;
        protected List<GeneratorState<T>> StatesList { get; set; }
        public GeneratorState<T> CurrentState { get; protected set; }

        //У него есть пул и в него он срёт контекстную генерацию по заданной логике
        protected ObjectGenerator(MonoPool<T> monoPool)
        {
            MonoPool = monoPool;
        }

        public virtual async Task Create()
        {
            if (CurrentState == null) throw new Exception("Где стейт в генераторе?");
            
            await CurrentState.Create();
        }

        public virtual async Task Update()
        {
            if (CurrentState == null) throw new Exception("Где стейт в генераторе?");
            
            await CurrentState.Update();
        }

        public async void SwitchState(Type newState)
        {
            var state = StatesList.Find(x => x.GetType() == newState);
			
            if (state != null)
            {
                if(CurrentState != null) await CurrentState.OnExit();
				
                CurrentState = state;
				
                await CurrentState.OnEnter();
            }
            else
            {
                throw new Exception($"{newState.Name} not initialized in states list");
            }
        }
    }
}