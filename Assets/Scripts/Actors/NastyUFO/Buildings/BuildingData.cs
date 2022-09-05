using System;
using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	[Serializable]
	public struct BuildingData
	{
		[Tooltip("Модуль дома")]
		public BuildingFloor _groundFloorElement, _middleFloorElement, _roofFloorElement;
		private const byte MODULES_COUNT = 3;
		
		public Bounds GetSumOfBounds()
		{
			Bounds sumOfBounds = new Bounds();
			Bounds temp;
			
			for (ushort i = 1; i <= MODULES_COUNT; i++)
			{
				temp = i switch {
					1 => _groundFloorElement.RenderBounds,
					2 => _middleFloorElement.RenderBounds,
					3 => _roofFloorElement.RenderBounds,
					_ => throw new Exception("")
				};
				
				sumOfBounds.Encapsulate(temp);
			}

			return sumOfBounds;
		}
	}
}