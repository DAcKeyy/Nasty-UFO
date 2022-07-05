using Miscellaneous.Interfaces;
using Scenes.Actors.NastyUFO;
using UnityEngine;

namespace Scenes.Generation.Factories.NastyUFO
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