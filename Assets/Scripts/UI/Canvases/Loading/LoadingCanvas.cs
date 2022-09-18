using UI.Base;
using UnityEngine;

namespace UI.Canvases.Loading
{
	[RequireComponent(typeof(Canvas))]
	public class LoadingCanvas : MonoBehaviour, ICanvas
	{
		public Canvas Canvas => GetComponent<Canvas>();
	}
}
