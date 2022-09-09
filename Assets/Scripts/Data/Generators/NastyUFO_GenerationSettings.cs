using System;
using Generation.Factories.NastyUFO;
using UnityEngine;

namespace Data.Generators
{
	[Serializable]
	public struct NastyUFO_GenerationSettings
	{
		[Header("Общее")]
		[Space(10)]
		[Tooltip("Дальность чистки объектов в метрах от камеры")]
		[Range(0f, 100f)]public float _spawnRange;
		[Tooltip("Дальность чистки объектов в метрах от камеры")]
		[Range(0f, 150f)]public float _clearingRange;
		[Tooltip("Частота обновления уровня в секундах")]
		public float _levelUpdateRate;
		[Tooltip("Частота обновления сложности игры в секундах")]
		public float _levelDifficultyIncreaseRate;
		[HideInInspector] public Vector3 _groundLevel;
		[HideInInspector] public Transform _generationCenter;
		[HideInInspector] public Camera _gameCamera;
		[HideInInspector] public bool _isGameStarted;
		

		[Header("Дома")]
		[Space(10)]
		public BuildingsFactory.BuildingFactorySettings _buildingsFactorySettings;
		[Tooltip("Передел этажей создаваемых домов где X - Минимум этажей а Y - Максимум")]
		public Vector2Int _buildingsFloorsRandomRange;
		[Tooltip("Растояние между домами")]
		[Range(0f, 10f)]
		public float _buildingsBetweenDistance;

		[Header("Облака")]
		[Space(10)]
		[Tooltip("Модификатор рандомности облаков от центра на линии спауна")]
		public Vector3 _cloudsRandomShift; 
		[Tooltip("Вероятность появления облака")]
		[Range(0f, 1f)] public float _cloudsSpawnChance;
		[Tooltip("Минимальное растояние между облаками")]
		[Range(0f, 30f)] public float _cloudsGapRange;
		[Tooltip("Высота облаков от _generationStartPosition")]
		[Range(0f, 100f)] public float _cloudsHeight;
		[Tooltip("Дополнительные облака вдаль на основе координат первого заспауненного облака на линии игры в апдейт генератора")]
		[Range(0, 20)] public ushort _aditionCloudsOnLine;
		public CloudsFactory.CloudsFactorySettings _cloudsFactorySettings;
	}
}