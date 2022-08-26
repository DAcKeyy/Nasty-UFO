using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace Generation.Generators.NastyUFO.Parts.Buildings.States
{
	public class BuildingsGenerator_RunState : GeneratorState<ModularBuilding>
	{
		private BuildingsFactory _factory;
		private NastyUFOLevelGeneration_Settings _generalSettings;
		
		public BuildingsGenerator_RunState(
			MonoPool<ModularBuilding> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(monoPool)
		{
			_factory = new BuildingsFactory(settings._buildingsFactorySettings);
			_generalSettings = settings;
		}
		
		public override void Create()
		{
			ModularBuilding building =_factory.Create(Vector2.zero, _generalSettings._gameCamera.transform.rotation);

			var buildingsDataList = _generalSettings._buildingsFactorySettings._buildingDataList;
			var buildingsData = buildingsDataList[Random.Range(
				_generalSettings._buildingsFactorySettings._randomFloorsGap.min,
				_generalSettings._buildingsFactorySettings._randomFloorsGap.max)]._buildingData;
			
			building.Init(buildingsData);
			
		}

		public override void Update()
		{
			base.Update();
		}

		private Vector3 CalculatePlace()
		{
			
			return Vector3.Zero;
		}
	}
}