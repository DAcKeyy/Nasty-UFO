using UI.Base;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    [RequireComponent(typeof(Canvas))]
    public class UFOGameCanvas : MonoBehaviour, ICanvas
    {
        public Canvas Canvas => GetComponent<Canvas>();
        public UICounter ScoreCounter => _scoreCounter;
        public Button MenuButton => _menuButton;
        
        [SerializeField] private UICounter _scoreCounter;
        [SerializeField] private Button _menuButton;
    }
}