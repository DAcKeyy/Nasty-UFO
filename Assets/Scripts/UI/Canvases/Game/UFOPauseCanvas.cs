using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    [RequireComponent(typeof(Canvas))]
    public class UFOPauseCanvas : MonoBehaviour, ICanvas
    {
        public Canvas Canvas => GetComponent<Canvas>();
        
        [SerializeField] private Button _resumeButton;
    }
}