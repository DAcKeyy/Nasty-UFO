using Actors.NastyUFO.Buildings;
using UnityEditor;
using UnityEngine;

namespace Editor.CustomEditors
{
	[CustomEditor(typeof(ModularBuilding))]
	public class BuildingEditor : UnityEditor.Editor
	{
		private ModularBuilding _modularBuilding;
		public ushort этажи = 5;

		public void OnEnable()
		{
			_modularBuilding = (ModularBuilding)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.LabelField("\nCustomEditor:\n");
			
			этажи = (ushort)EditorGUILayout.IntField("Сколько этажей", этажи);
			if(GUILayout.Button("Построить дом")) 
			{
				_modularBuilding.Init(_modularBuilding.BuildingData);
				_modularBuilding.AssembleBuilding(этажи);
			}

			if (GUILayout.Button("Снести его"))
			{
				for(var i = _modularBuilding.transform.childCount - 1; i >= 0; i--)
				{
					if (_modularBuilding.transform.GetChild(i).TryGetComponent(out BoxCollider collider))
					{
						collider.size = Vector3.zero;
					}
					else DestroyImmediate(_modularBuilding.transform.GetChild(i).gameObject);
				}
				
				_modularBuilding.TryGetComponent<BoxCollider>(out BoxCollider floorBoxCollider);
				
				if (floorBoxCollider != null) DestroyImmediate(floorBoxCollider);
			}
			
		}
	}
}
