using Actors.NastyUFO;
using Miscellaneous.Interfaces;
using UnityEngine;

namespace Generation.Factories.NastyUFO
{
	public class LandFactory : IFactory<Vector3, Land>
	{
		public Land Create(Vector3 param)
		{
			throw new System.NotImplementedException();
		}

		public struct LandFactorySettings
		{
			public Vector2 LandSizes;
		}
	}
}