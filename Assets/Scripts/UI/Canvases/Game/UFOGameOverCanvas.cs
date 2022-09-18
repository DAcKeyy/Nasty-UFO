//using Data.Saving;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    [RequireComponent(typeof(Canvas))]
    public class UFOGameOverCanvas : MonoBehaviour, ICanvas
    {
        public Canvas Canvas => GetComponent<Canvas>();
        public Button OkButton => _okButton;
        public Button RestartButton => _restartButton;
        [SerializeField] private TMP_Text _scoreValueText;
        [SerializeField] private TMP_Text _scoreBestValueText;
        [SerializeField] private TMP_Text _commentText;
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _restartButton;

        public void Update()
        {
            //_scoreValueText.text = GlobalPlayerPrefs.CurrentScore.ToString();
            //_scoreBestValueText.text = GlobalPlayerPrefs.BestScore.ToString();
        }
    }
}