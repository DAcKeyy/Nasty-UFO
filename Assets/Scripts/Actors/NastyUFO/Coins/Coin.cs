﻿using System;
using Actors.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Actors.NastyUFO.Coins
{
	[RequireComponent(typeof(SphereCollider))]
	public class Coin : MonoBehaviour, ICollectable
	{
		public SphereCollider _sphereCollider;
		public float _value;
		public Action<Coin> CoinCollected;
		public UnityEvent _collected;

		private void Reset()
		{
			_sphereCollider = GetComponent<SphereCollider>();
			_sphereCollider.isTrigger = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			CollectIt(other);
		}
		
		public void Disappear()
		{
			Destroy(gameObject);
		}

		public void CollectIt(Collider collider)
		{
			if (collider.TryGetComponent(out UFO player))
			{
				CoinCollected?.Invoke(this);
				_collected.Invoke();
				Disappear();
				//TODO анимация изчезновения
			}
		}
	}
}