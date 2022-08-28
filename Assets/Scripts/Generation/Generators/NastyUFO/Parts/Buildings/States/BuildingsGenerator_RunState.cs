using System;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

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
			CreateFirstBuilding();
		}

		public override void Update()
		{
			base.Update();
		}

		private ModularBuilding CreateFirstBuilding()
		{
			//TODO Magic number
			ModularBuilding building = _factory.Create(Vector2.zero, Quaternion.Euler(0,180,0));

			Bounds buildingBounds = building.BuildingData.GetMaxRenderBoxSize();

			var centerPos = _generalSettings._generationCenter.position;
			
			building.transform.position = new Vector3(centerPos.x + buildingBounds.size.x - _generalSettings._clearingRange ,0,centerPos.z);
			
			building.AssembleBuilding((ushort)Random.Range(_generalSettings._buildingsFactorySettings._randomFloorsGap.min,
				_generalSettings._buildingsFactorySettings._randomFloorsGap.max));

			return building;
		}
	
		private Vector3 CalculatePlace(Vector3 place, Bounds buildingBounds)
		{
			throw new NotImplementedException();
		}
	}
}