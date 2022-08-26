using System.Collections.Generic;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Contexts.NastyUFO;
using Generation.Factories.NastyUFO;
using Generation.Generators.NastyUFO.Parts.Buildings;
using Generation.Generators.NastyUFO.States;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO
{
	public class UFOObjectGenerator : ObjectGenerator<MonoBehaviour>
	{
		private readonly ObjectGenerator<ModularBuilding> _buildingsGenerator;
		private readonly ObjectGenerator<Cloud> _cloudsGenerator;
		private MonoPool<ModularBuilding> _buildingPool;
		private MonoPool<Cloud> _cloudPool;

		public UFOObjectGenerator(
			ref MonoPool<MonoBehaviour> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(monoPool)
		{
			InitPools();
			
			//Делаем генераторы по бизнесс логике
			_buildingsGenerator = new BuildingsGenerator(ref _buildingPool, settings);
			_cloudsGenerator = new CloudsGenerator(ref _cloudPool,settings, new CloudsFactory(settings._cloudsFactorySettings));
			
			StatesList = new List<GeneratorState<MonoBehaviour>>()
			{
				new UFOObjectGenerator_AwaitInputState(ref MonoPool, settings, _buildingsGenerator, _cloudsGenerator),
				new UFOObjectGenerator_RunState(ref MonoPool, settings, _buildingsGenerator, _cloudsGenerator),
				new UFOObjectGenerator_StopState(ref MonoPool, settings)
			};

			CurrentState = StatesList[0];
		}
		
		public override void Create()
		{
			_buildingsGenerator.Create();
			_cloudsGenerator.Create();
		}

		public override void Update()
		{
			_buildingsGenerator.Update();
			_cloudsGenerator.Update();
		}

		private void InitPools()
		{
			//Создаём пулы
			_buildingPool = new MonoPool<ModularBuilding>();
			_cloudPool = new MonoPool<Cloud>();
			//Собскрайбимся к тому что там создалось и прокидываем в основной пул
			_buildingPool.ObjectAdded += building => MonoPool.AddObject(building);
			_cloudPool.ObjectAdded += cloud => MonoPool.AddObject(cloud);
		}
	}
}