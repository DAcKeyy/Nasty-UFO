using System;
using UnityEngine;

namespace Data.Difficulty
{
	[Serializable]
	public struct NastyUFO_DifficultySettings
	{
		[Header("Все кривые по оси Х подразумевают собой проведённое в игре время!")]
		[Space(10)]
		public AnimationCurve _floorRandomnessMultiplierCurve;
		public AnimationCurve _playerSpeedMultiplierCurve;
	}
}