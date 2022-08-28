using System;
using System.Collections.Generic;
using Miscellaneous.StateMachines.Base;
using UnityEngine;

namespace Miscellaneous.GameController
{
    public abstract class GameManager : MonoBehaviour
    {
        protected StateMachine StateMachine;

        public void SwitchState(Type newState)
        {
            StateMachine.SwitchState(newState);
        }
        
        public void SetStateList(List<State> listState)
        {
            StateMachine = new StateMachine(listState);
        }
        
        async void Update()
        {
            await StateMachine?.Update()!;
        }
    }
}
