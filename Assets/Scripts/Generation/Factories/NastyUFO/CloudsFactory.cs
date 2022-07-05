using System;
using System.Collections.Generic;
using Actors.NastyUFO;
using Miscellaneous.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Factories.NastyUFO
{
	public class CloudsFactory : IFactory<Vector3,Cloud>
	{

		private readonly CloudsFactorySettings _settings;
		private readonly GameObject _cloudsParent;

		public CloudsFactory(CloudsFactorySettings settings)
		{
			_settings = settings;

			_cloudsParent = new GameObject("Clouds") {
				transform = {
					position = Vector3.zero,
				}
			};
		}
		
		public Cloud Create(Vector3 position)
		{
			return UnityEngine.Object.Instantiate(
					_settings._cloudsPoolList[Random.Range(0,_settings._cloudsPoolList.Count)], //рандом дом из списка
					position, 
					Quaternion.Euler(position), 
					_cloudsParent.transform)
				.GetComponent<Cloud>();
		}
		
		[Serializable]
		public struct CloudsFactorySettings
		{
			public List<Cloud> _cloudsPoolList;
		}
	}
}
