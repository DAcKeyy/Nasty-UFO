using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases.Menu
{
	//TODO Канвасы для настроек и для интро
	[RequireComponent(typeof(Canvas))]
	public class MainMenuCanvas : MonoBehaviour, ICanvas
	{
		public Canvas Canvas => GetComponent<Canvas>();
		public Button PlayButton => _playButton;
		public Button SettingsButton => _settingsButton;
		public Button DevelopersButton => _developersButton;

		[SerializeField] private Button _playButton;
		[SerializeField] private Button _settingsButton;
		[SerializeField] private Button _developersButton;
	}
}