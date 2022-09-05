using UnityEngine;

namespace Data.Generators
{
	[CreateAssetMenu(fileName = "Generation Settings", menuName = "ScriptableObjects/Level Generation Settings", order = 1)]
	public class LevelGenerationSettings_ScriptableObject : ScriptableObject
	{
		public NastyUFO_GenerationSettings _settings;
	}
}