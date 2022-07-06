using System;
using Generation.Factories.NastyUFO;
using UnityEngine;

namespace Data.Generators
{
	[Serializable]
	public struct NastyUFOLevelGeneration_Settings
	{
		[Header("Общее")]
		[Space(10)]
		//TODO От _generationStartPosition можно избавиться просчитывая _clearingRange и позицию игрока
		[Tooltip("Точка от которой в начале всё созадётся")]
		public Vector2 _generationStartPosition;
		[Tooltip("Частота обновления уровня в секундах (костыль прозводительности)")]
		public float _levelUpdateRate;
		[Tooltip("Дальность чистки обектов в метрах от камеры")]
		[Range(0f, 100f)]public float _clearingRange;
		[Tooltip("Центр от чего всё будет генерироваться и просчитываться (этот трансформ может двигаться)")]
		public Transform _generationCenter;
		

		[Header("Дома")]
		[Space(10)]
		public BuildingsFactory.BuildingFactorySettings _buildingsFactorySettings;
		[Tooltip("Дистанция в метрах между границами зданий")]
		[Range(0, 20)] public float _buildingDistanceGap;
		[Tooltip("Передел этажей создаваемых домов где X - Минимум этажей а Y - Максимум")]
		public Vector2Int _buildingsFloorsRandomRange;
		
		
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