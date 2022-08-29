using System;
using System.Threading.Tasks;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Generators.NastyUFO.Parts.Buildings.States
{
	public class RunState : GeneratorState<ModularBuilding>
	{
		private BuildingsFactory _factory;
		private NastyUFOLevelGeneration_Settings _generalSettings;
		private MonoPool<ModularBuilding> _monoPool;
		
		public RunState(
			MonoPool<ModularBuilding> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(monoPool)
		{
			_monoPool = monoPool;
			_factory = new BuildingsFactory(settings._buildingsFactorySettings);
			_generalSettings = settings;
		}
		
		public override Task Create()
		{
			ModularBuilding currentBuilding = null;

			//Если самый первый дом в игре
			if (_monoPool.PrefabPool.Count == 0)
			{
				var edgePoint = new Vector3(
					-_generalSettings._spawnRange, 
					0,//TODO Magic number
					_generalSettings._generationCenter.position.z);
				
				currentBuilding = CreateBuilding(edgePoint, Quaternion.Euler(0,-180, 0));//TODO Magic number
				
				AssembleAndPool(currentBuilding);
			}
			
			//до края чистки
			while (_monoPool.GetLast().transform.position.x < _generalSettings._spawnRange)
			{
				Quaternion newRotation = Quaternion.Euler(0, -180, 0);//TODO Magic number
				Vector3 lastBuildingPos = _monoPool.GetLast().transform.position;
				ModularBuilding building = _factory.Create(lastBuildingPos, newRotation);
				Vector3 newBuildPosition = GetAbleToBuildPoint(lastBuildingPos, building.BuildingData.GetGreatestRenderBounds());

				currentBuilding = CreateBuilding(newBuildPosition, newRotation);

				currentBuilding.transform.position = newBuildPosition;

				AssembleAndPool(currentBuilding);
			}

			return Task.CompletedTask;
		}

		public override Task Update()
		{
			ModularBuilding currentBuilding = null;

			//TODO Костыль на подвязку по оси Х, хз как фиксить
			while (_monoPool.GetLast().transform.position.x < _generalSettings._spawnRange + _generalSettings._generationCenter.position.x)
			{
				Vector3 newBuildPosition = _monoPool.GetLast().transform.position;
				Quaternion newRotation = Quaternion.Euler(0, -180, 0);
				
				currentBuilding = CreateBuilding(newBuildPosition, newRotation);

				currentBuilding.transform.position = GetAbleToBuildPoint(currentBuilding.transform.position, currentBuilding.BuildingData.GetGreatestRenderBounds());

				AssembleAndPool(currentBuilding);
			}
			
			return Task.CompletedTask;
		}

		private void AssembleAndPool(ModularBuilding building)
		{
			if (building == null) throw new NullReferenceException("Can't assemble null ModularBuilding");
			
			var floor = (ushort) Random.Range(
				_generalSettings._buildingsFloorsRandomRange.x,
				_generalSettings._buildingsFloorsRandomRange.y);
			
			building.AssembleBuilding(floor);
			
			_monoPool.AddObject(building);
		}
		
		private ModularBuilding CreateBuilding(Vector3 position, Quaternion rotation)
		{
			ModularBuilding building = _factory.Create(position, rotation);

			return building;
		}

		private Vector3 GetAbleToBuildPoint(Vector3 place, Bounds buildBounds)
		{
			//TODO Костыльный метод. Возможно в нём происходит арефмитический пиздец
			var lastBound = _monoPool.GetLast().BuildingData.GetGreatestRenderBounds();
			lastBound.Contains(new Vector3(place.x,place.y + 0.001f, place.z));
			var awayDistance = buildBounds.size.x + lastBound.size.x;
			
			var correctPoint = new Vector3(_monoPool.GetLast().transform.position.x + awayDistance, place.y, place.z);
			
			return correctPoint;
		}
	}
}