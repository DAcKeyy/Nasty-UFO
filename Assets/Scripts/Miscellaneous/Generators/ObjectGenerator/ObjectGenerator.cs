﻿using System;
using System.Collections.Generic;
using System.Linq;
using Miscellaneous.Pools;
using UnityEngine;

namespace Miscellaneous.Generators.ObjectGenerator
{
    public abstract class ObjectGenerator<T> where T : MonoBehaviour //То что возвращает
    {
        protected MonoPool<T> MonoPool;
        public List<GeneratorState<T>> StatesList { get; protected set; }
        public GeneratorState<T> CurrentState { get; protected set; }

        //У него есть пул и в него он срёт контекстную генерацию по заданной логике
        protected ObjectGenerator(MonoPool<T> monoPool)
        {
            MonoPool = monoPool;
        }

        public virtual void Create()
        {
            if (CurrentState == null) throw new Exception("Где стейт в генераторе?");
            CurrentState.Create();
            Debug.Log("Создаю хуйню");
        }

        public virtual void Update()
        {
            if (CurrentState == null) throw new Exception("Где стейт в генераторе?");
            CurrentState.Update();
            Debug.Log("Обновляю хуйню");
        }

        public void SwitchState<TStateType>(TStateType newState) where TStateType : GeneratorState<T>
        {
            var state = StatesList.FirstOrDefault(state => state is TStateType);
            if (state == null) throw new NotFiniteNumberException($"{typeof(T)} not initialized in states list");
            
            //TODO Если текущий стейт не выполнил работу то await
            CurrentState = newState;
        }
    }
}