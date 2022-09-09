using System;
using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	[Serializable]
	[RequireComponent(typeof(BoxCollider))]
	public class BuildingFloor : MonoBehaviour
	{
		public Bounds RenderBounds => _meshFilter.sharedMesh.bounds;
		[SerializeField] private MeshFilter _meshFilter;
		[Header("Убедись что префаб модуля на 0 0 0 координатах")]
		public Type _floorType = Type.MiddleFlor;
		
		private void Reset()
		{
			_meshFilter = GetComponent<MeshFilter>();
		}

		public enum Type
		{
			GroundFloor,
			MiddleFlor,
			Roof
		}
		
		public void ChangeModuleColor(Color general, Color accent, Color shadow)
		{
			//TODO Поиск по материалам и изменение цвета нужной переменной материала, главное чтобы материал был стандартзирован
		}
	}
}