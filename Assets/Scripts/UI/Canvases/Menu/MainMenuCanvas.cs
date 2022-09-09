using UI.Base;
using UnityEngine;

namespace UI.Canvases.Menu
{
	//TODO Канвасы для настроек и для интро
	[RequireComponent(typeof(Canvas))]
	public class MainMenuCanvas : MonoBehaviour, ICanvas
	{
		public Canvas Canvas => GetComponent<Canvas>();
	}
}