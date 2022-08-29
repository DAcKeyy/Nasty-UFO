using System;
using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	[Serializable]
	public struct BuildingData
	{
		[Tooltip("Модуль дома")]
		public BuildingFloor GroundFloorElement, MiddleFloorElement, RoofFloorElement;
		//Вставь нужные модули сверху и число элементов снизу 
		private const byte MODULES_COUNT = 3;
		
		public Bounds GetGreatestRenderBounds()
		{
			Bounds best = GroundFloorElement.GetComponent<Renderer>().bounds;

			for (var i = 0; i < MODULES_COUNT - 1; i++)
			{
				Bounds temp = GroundFloorElement.GetComponent<Renderer>().bounds;

				if (Mathf.Abs(temp.center.x) + Mathf.Abs(temp.size.x) > Mathf.Abs(temp.center.x) + Mathf.Abs(best.size.x) &&
				    Mathf.Abs(temp.center.x) + Mathf.Abs(temp.size.z) > Mathf.Abs(temp.center.x) + Mathf.Abs(best.size.z) && 
				    Mathf.Abs(temp.center.x) + Mathf.Abs(temp.size.y) > Mathf.Abs(temp.center.x) + Mathf.Abs(best.size.y))
				{
					best = temp;
				}
			}

			return best;
		}
	}
}