using System;
using System.Threading.Tasks;
using Actors.NastyUFO.Buildings;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using SceneBehavior.UFOGame.Difficulty;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Generators.NastyUFO.Parts.Buildings.States
{
	public class RunState : GeneratorState<ModularBuilding>
	{
		private readonly BuildingsFactory _factory;
		private readonly MonoPool<ModularBuilding> _buildingsPool;
		private readonly UFO_DifficultyController _difficultyController;
		private readonly Quaternion _buildingRotation = Quaternion.Euler(0, -180, 0);//TODO Magic number
		
		private float NewSpawnDistance_X =>
			_difficultyController.GenerationSettings._spawnRange +
			_difficultyController.GenerationSettings._generationCenter.position.x;
		
		private float LastBuildingPosition_X => 
			_buildingsPool.GetLast().GetWorldGroundedConnectionPoint(ModularBuilding.Side.Right).x + 
			_factory.BiggestBuildingsBound.size.x;
		
		private ModularBuilding _currentBuilding;
		private ModularBuilding _lastBuilding;
		private Vector3 _newBuildPosition;
		private float _buildingsBetweenDistance;
		
		public RunState(MonoPool<ModularBuilding> buildingsPool, 
			UFO_DifficultyController difficultyController) : base(buildingsPool)
		{
			if (difficultyController.GenerationSettings._spawnRange > difficultyController.GenerationSettings._clearingRange)
				throw new Exception("Spawn range is greater than level Clearing range");
			
			_buildingsPool = buildingsPool;
			_factory = new BuildingsFactory(difficultyController.GenerationSettings._buildingsFactorySettings);
			_difficultyController = difficultyController;
		}
		
		public override async Task Create()
		{
			//Если самый первый дом в игре
			if (_buildingsPool.PrefabPool.Count == 0)
			{
				_currentBuilding = _factory.Create(Vector2.zero, _buildingRotation);

				var generationCenterPosition = _difficultyController.GenerationSettings._generationCenter.position;
				
				var edgePoint = new Vector3(
					-_difficultyController.GenerationSettings._spawnRange + _factory.BiggestBuildingsBound.size.x + generationCenterPosition.x, 
					0,//TODO Magic number
					generationCenterPosition.z);

				_currentBuilding.transform.position = edgePoint;
				
				AssembleAndPool(_currentBuilding);
			}
			
			//Дострой все остальные дома как обычно
			await Update();
		}

		public override Task Update()
		{
			_buildingsBetweenDistance = (_currentBuilding.BuildingData.GetSumOfBounds().size.x / 2) + _difficultyController.GenerationSettings._buildingsBetweenDistance;
			_lastBuilding = _buildingsPool.GetLast();
			_newBuildPosition = new Vector3(
				_lastBuilding.GetWorldGroundedConnectionPoint(ModularBuilding.Side.Right).x + _buildingsBetweenDistance,
				_lastBuilding.GetWorldGroundedConnectionPoint(ModularBuilding.Side.Right).y,
				_difficultyController.GenerationSettings._generationCenter.position.z);
			
			//TODO Подвязка по оси Х, может сделать по любой оси?...
			while (LastBuildingPosition_X < NewSpawnDistance_X)
			{
				_currentBuilding = _factory.Create(_newBuildPosition, _buildingRotation);
				_buildingsBetweenDistance = _currentBuilding.BuildingData.GetSumOfBounds().size.x + _difficultyController.GenerationSettings._buildingsBetweenDistance;

				_newBuildPosition.x += _buildingsBetweenDistance;

				_currentBuilding.transform.position = _newBuildPosition;

				AssembleAndPool(_currentBuilding);
			}
			
			return Task.CompletedTask;
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