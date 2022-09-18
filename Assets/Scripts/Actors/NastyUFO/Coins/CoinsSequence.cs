using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.NastyUFO.Coins
{
	[Serializable]
	public class CoinsSequence : MonoBehaviour
	{
		public float Value {
			get
			{
				float value = 0;
				
				foreach (var coin in _coins)
				{
					value += coin._value;
				}

				return value;
			}
		}
		public Bounds _bounds;
		public List<Coin> _coins = new List<Coin>();
		
		public void Reset()
		{
			_coins.Clear();
			
			foreach (Transform child in transform)
			{
				if (child.TryGetComponent(out Coin coin))
				{
					_coins.Add(coin);
					_bounds.Encapsulate(coin._sphereCollider.bounds);
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(_bounds.center + gameObject.transform.position, _bounds.size);
		}	
	}
}