using UnityEngine;

namespace Data.Difficulty
{
	[CreateAssetMenu(fileName = "Difficulty Settings", menuName = "ScriptableObjects/Level Difficulty Settings", order = 1)]
	public class DifficultySettings_ScriptableObject : ScriptableObject
	{
		public NastyUFO_DifficultySettings _settings;
	}
}