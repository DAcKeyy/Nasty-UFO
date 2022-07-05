using UnityEngine;

namespace Data.Generators
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level Generation Settings", order = 1)]
	public class LevelGenerationScriptableObjectSettings : ScriptableObject
	{
		public NastyUFOLevelGenerationSettings _settings;
	}
}