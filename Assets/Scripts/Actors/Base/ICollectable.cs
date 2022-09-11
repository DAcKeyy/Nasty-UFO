using UnityEngine;

namespace Actors.Base
{
	public interface ICollectable
	{
		public void CollectIt(Collider collider);
	}
}