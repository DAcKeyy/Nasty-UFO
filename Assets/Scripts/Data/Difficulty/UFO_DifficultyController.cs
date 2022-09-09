using Actors.NastyUFO;
using Data.Generators;

namespace Data.Difficulty
{
	public class UFO_DifficultyController
	{
		public NastyUFO_GenerationSettings GenerationSettings => _generationSettings;

		private NastyUFO_DifficultySettings _difficultySettings;
		private NastyUFO_GenerationSettings _generationSettings;
		private NastyUFO_GenerationSettings _startGenerationSettings;
		private UFO _player;

		public UFO_DifficultyController(
			LevelGenerationSettings_ScriptableObject settingsScriptableObject,
			DifficultySettings_ScriptableObject difficultySettingsScriptableObject,
			UFO player)
		{
			_player = player;
			_generationSettings = settingsScriptableObject._settings;
			_startGenerationSettings = settingsScriptableObject._settings;
			_difficultySettings = difficultySettingsScriptableObject._settings;
		}
		
		public void UpdateDifficulty(float currentTime)
		{
			_generationSettings._buildingsFloorsRandomRange.x *= (int)_difficultySettings._floorRandomnessMultiplierCurve.Evaluate(currentTime);
			_generationSettings._buildingsFloorsRandomRange.y *= (int)_difficultySettings._floorRandomnessMultiplierCurve.Evaluate(currentTime);
			_player.ChangeFlySpeed(_difficultySettings._playerSpeedMultiplierCurve.Evaluate(currentTime));
		}
	}	
}