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
        public TextController MetersCounter => _metersCounter;
        public TextController CoinsCounter => _coinsCounter;
        public Button MenuButton => _menuButton;
        
        [SerializeField] private Button _menuButton;
        [SerializeField] private TextController _metersCounter;
        [SerializeField] private TextController _coinsCounter;
    }
}