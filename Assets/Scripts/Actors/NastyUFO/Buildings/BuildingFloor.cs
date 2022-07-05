using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	public class BuildingFloor : MonoBehaviour
	{
		[field: SerializeField] public Type FloorType { get; private set; } = Type.MiddleFlor;

		public enum Type
		{
			GroundFloor,
			MiddleFlor,
			Roof
		}
	}
}