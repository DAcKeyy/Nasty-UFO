using Data.Saving;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Canvases
{
    public class FlappyBirdGameOverCanvas : MonoBehaviour, ICanvas
    {
        [SerializeField] private TMP_Text _scoreValueText;
        [SerializeField] private TMP_Text _scoreBestValueText;
        [SerializeField] private TMP_Text _commentText;
        [SerializeField] private Button _okButton;

        /*
        public void Init(SignalBus signalBus)
        {
            _okButton.onClick.AddListener(() => SceneManager.LoadScene("Game"));
            
            //TODO Добавить посередник между канвасами
            signalBus.Subscribe<PlayerDiedSignal>(x =>
            {
                gameObject.SetActive(true);
                Update();
            });
        }*/

        public void Update()
        {
            _scoreValueText.text = GlobalPlayerPrefs.CurrentScore.ToString();
            _scoreBestValueText.text = GlobalPlayerPrefs.BestScore.ToString();
        }
    }
}