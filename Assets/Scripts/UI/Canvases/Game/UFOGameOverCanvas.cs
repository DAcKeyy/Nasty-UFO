using Data.Saving;
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
        [SerializeField] private TMP_Text _scoreValueText;
        [SerializeField] private TMP_Text _scoreBestValueText;
        [SerializeField] private TMP_Text _commentText;
        [SerializeField] private Button _okButton;

        public void Update()
        {
            //_scoreValueText.text = GlobalPlayerPrefs.CurrentScore.ToString();
            //_scoreBestValueText.text = GlobalPlayerPrefs.BestScore.ToString();
        }
    }
}