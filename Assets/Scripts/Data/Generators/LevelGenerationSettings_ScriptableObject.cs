using UnityEngine;

namespace Data.Generators
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level Generation Settings", order = 1)]
	public class LevelGenerationSettings_ScriptableObject : ScriptableObject
	{
		public NastyUFOLevelGeneration_Settings _settings;
	}
}