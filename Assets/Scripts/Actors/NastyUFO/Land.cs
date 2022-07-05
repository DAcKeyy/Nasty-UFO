using UnityEngine;

namespace Actors.NastyUFO
{
	[RequireComponent(typeof(BoxCollider))]
	public class Land : MonoBehaviour
	{
		public BoxCollider LandBoxCollider { get; private set; }
		
		private void Awake()
		{
			LandBoxCollider = GetComponent<BoxCollider>();
		}
	}
}
