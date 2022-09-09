using System;
using System.Threading.Tasks;
using Actors.NastyUFO.Buildings;
using Data.Difficulty;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Generators.NastyUFO.Parts.Buildings.States
{
	public class RunState : GeneratorState<ModularBuilding>
	{
		private readonly BuildingsFactory _factory;
		private readonly MonoPool<ModularBuilding> _buildingsPool;
		private readonly UFO_DifficultyController _difficultyController;
		private readonly Quaternion _buildingRotation = Quaternion.Euler(0, -180, 0);//TODO Magic number, решить головоломку с поворотом
		private readonly Vector3 _groundLevel;
		private readonly float _buildingsBetweenDistance;
		private readonly float _spawnRange;

		private float NewSpawnDistanceOnX {
			get
			{
				var groundUnderCenter = new Vector3(_generationCenter.x, _groundLevel.y, _generationCenter.z);
				var dist = Vector3.Distance(_generationCenter,groundUnderCenter);
				var newX = _difficultyController.GenerationSettings._generationCenter.position.x + dist;
				var newSpawnRange = _spawnRange + _difficultyController.GenerationSettings._generationCenter.position.x;
				if (newX < newSpawnRange) return newX;
				else return newSpawnRange;
			}
		}

		private float LastOneBoundsEdgeOnX => _lastBuilding.GetWorldGroundedConnectionPoint(ModularBuilding.Side.Right).x +
		                              _factory.BiggestBuildingsBound.size.x;
		
		private Vector3 _generationCenter;
		private ModularBuilding _lastBuilding;
		private Vector3 _newBuildPosition;

		public RunState(MonoPool<ModularBuilding> buildingsPool, 
			UFO_DifficultyController difficultyController) : base(buildingsPool)
		{
			if (difficultyController.GenerationSettings._spawnRange > difficultyController.GenerationSettings._clearingRange)
				throw new Exception("Spawn range is greater than level Clearing range");

			_groundLevel = difficultyController.GenerationSettings._groundLevel;
			_spawnRange = difficultyController.GenerationSettings._spawnRange;
			_buildingsBetweenDistance = difficultyController.GenerationSettings._buildingsBetweenDistance;
			_buildingsPool = buildingsPool;
			_factory = new BuildingsFactory(difficultyController.GenerationSettings._buildingsFactorySettings);
			_difficultyController = difficultyController;
		}
		
		public override async Task Create()
		{
			if (_buildingsPool.PrefabPool.Count != 0)
			{
				throw new Exception($"А дома то уже есть, аж {_buildingsPool.PrefabPool.Count} штуки");
			}

			//Если самый первый дом в игре
			if (_buildingsPool.PrefabPool.Count == 0)
			{
				_generationCenter = _difficultyController.GenerationSettings._generationCenter.position;
				
				var groundUnderCenter = new Vector3(_generationCenter.x, _groundLevel.y, _generationCenter.z);
				var dist = Vector3.Distance(_generationCenter,groundUnderCenter);
				var edgePoint = new Vector3(_generationCenter.x - dist, _groundLevel.y, _generationCenter.z);
				
				if (Vector3.Distance(edgePoint, _generationCenter) > _spawnRange)
				{
					_lastBuilding = null;
					Debug.Log("А тарелка то взлетела");
					return;
				}
				
				_lastBuilding = _factory.Create(edgePoint, _buildingRotation);
				
				AssembleAndPool(_lastBuilding);
			}
			
			//Дострой все остальные дома как обычно
			await Update();
		}

		public override async Task Update()
		{
			if (_lastBuilding == null)
			{
				await Create();
				if(_lastBuilding == null) return;
			}
			
			var buildingsBetweenDistance = (_lastBuilding.BuildingData.GetSumOfBounds().size.x / 2) + _buildingsBetweenDistance;
			_generationCenter = _difficultyController.GenerationSettings._generationCenter.position;
				
			_newBuildPosition = new Vector3(
				_lastBuilding.GetWorldGroundedConnectionPoint(ModularBuilding.Side.Right).x + buildingsBetweenDistance,
				_groundLevel.y,
				_generationCenter.z);
			

			//TODO Подвязка по оси Х, может сделать по любой оси?...
			while (LastOneBoundsEdgeOnX < NewSpawnDistanceOnX)
			{
				_lastBuilding = _factory.Create(_newBuildPosition, _buildingRotation);
				buildingsBetweenDistance = _lastBuilding.BuildingData.GetSumOfBounds().size.x + _buildingsBetweenDistance;

				_newBuildPosition.x += buildingsBetweenDistance;

				_lastBuilding.transform.position = _newBuildPosition;

				AssembleAndPool(_lastBuilding);
			}

			await Task.CompletedTask;
		}

		private void AssembleAndPool(ModularBuilding building)
		{
			if (building == null) throw new NullReferenceException("Can't assemble null ModularBuilding");
			
			var floor = (ushort) Random.Range(
				_difficultyController.GenerationSettings._buildingsFloorsRandomRange.x,
				_difficultyController.GenerationSettings._buildingsFloorsRandomRange.y);
			
			building.AssembleBuilding(floor);
			
			_buildingsPool.AddObject(building);
		}
	}
}