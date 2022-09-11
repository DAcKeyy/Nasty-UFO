using System;
using Data.Shaders;
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
		
		public void ChangeModuleColor(Color_Variation_URP shaderData)
		{
			var newProperties = new MaterialPropertyBlock();

			newProperties.SetColor("_Base_Color",shaderData._baseColor);
			newProperties.SetColor("_Shadow",shaderData._shadow);
			newProperties.SetColor("_Accent",shaderData._accent);
			newProperties.SetFloat("_Saturation",shaderData._saturation);
			
			GetComponent<Renderer>().SetPropertyBlock(newProperties, 0);
		}
	}
}