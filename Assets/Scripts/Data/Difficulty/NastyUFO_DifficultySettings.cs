using System;
using UnityEngine;

namespace Data.Difficulty
{
	[Serializable]
	public struct NastyUFO_DifficultySettings
	{
		public AnimationCurve _buildingsMinFloorsCurve;
		public AnimationCurve _buildingsMaxFloorsCurve;
		public AnimationCurve _playerSpeedCurve;
	}
}