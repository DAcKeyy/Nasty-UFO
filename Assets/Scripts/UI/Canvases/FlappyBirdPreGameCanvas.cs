using UI.Base;
using UnityEngine;

namespace UI.Canvases
{
    public class FlappyBirdPreGameCanvas : MonoBehaviour , ICanvas
    {
        /*
        public void Init(SignalBus signalBus)
        {
            //TODO Добавить посередник между канвасами
            signalBus.Subscribe<GameStarted>(x => {
                gameObject.SetActive(false);
            });
        }*/

        public void Update()
        {
            //throw new System.NotImplementedException();
        }
    }
}