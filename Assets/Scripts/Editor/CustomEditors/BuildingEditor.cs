using Actors.NastyUFO.Buildings;
using Data.Generators;
using UnityEditor;
using UnityEngine;

namespace Editor.CustomEditors
{
	[CustomEditor(typeof(ModularBuilding))]
	public class BuildingEditor : UnityEditor.Editor
	{
		private ModularBuilding _modularBuilding;
		private NastyUFO_GenerationSettings _generationSettings;
		public ushort этажи = 5;

		public void OnEnable()
		{
			_modularBuilding = (ModularBuilding)target;
			var scriptable = Resources.Load("Data/Settings/Level Generation Settings") as LevelGenerationSettings_ScriptableObject;
			_generationSettings = scriptable._settings;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.LabelField("\nCustomEditor:\n");
			
			этажи = (ushort)EditorGUILayout.IntField("Сколько этажей", этажи);
			if(GUILayout.Button("Построить дом")) 
			{
				_modularBuilding.Init(_modularBuilding.BuildingData);
				_modularBuilding.AssembleBuilding(этажи, _generationSettings._houseColors[Random.Range(0, _generationSettings._houseColors.Count)]);
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
