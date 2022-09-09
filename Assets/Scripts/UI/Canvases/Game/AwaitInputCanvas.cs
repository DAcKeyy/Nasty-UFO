using UI.Base;
using UnityEngine;

namespace UI.Canvases
{
    [RequireComponent(typeof(Canvas))]
    public class AwaitInputCanvas : MonoBehaviour , ICanvas
    {
        public Canvas Canvas => GetComponent<Canvas>();
    }
}