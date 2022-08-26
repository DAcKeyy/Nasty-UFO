using System;
using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	[Serializable]
	[RequireComponent(typeof(BoxCollider))]
	public class BuildingFloor : MonoBehaviour
	{
		[Header("Убедись что префаб модуля на 0 0 0 координатах")]
		
		public Type _floorType = Type.MiddleFlor;
		
		public enum Type
		{
			GroundFloor,
			MiddleFlor,
			Roof
		}
		
		public void ChangeModuleColor(Color color)
		{
			//TODO Поиск по материалам и изменение цвета нужной переменной материала, главное чтобы материал был стандартзирован
		}
	}
}