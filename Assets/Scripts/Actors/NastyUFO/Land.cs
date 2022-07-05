using System;
using UnityEngine;

namespace Scenes.Actors.NastyUFO
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
