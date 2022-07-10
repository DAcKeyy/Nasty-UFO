using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Base;
using Generation.Contexts.NastyUFO;
using Generation.Factories.NastyUFO;
using Generation.Generators.NastyUFO.Parts.Buildings;
using Generation.Generators.NastyUFO.Parts.Buildings.States;
using Generation.Generators.NastyUFO.States;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO
{
	public class NastyUfoObjectGenerator : ObjectGenerator<MonoBehaviour>
	{
		private readonly ObjectGenerator<ModularBuilding> _buildingsGenerator;
		private readonly ObjectGenerator<Cloud> _cloudsGenerator;
		private MonoPool<ModularBuilding> _buildingPool;
		private MonoPool<Cloud> _cloudPool;

		public NastyUfoObjectGenerator(
			ref MonoPool<MonoBehaviour> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(ref monoPool)
		{
			InitPools();
			
			//Делаем генераторы по бизнесс логике
			_buildingsGenerator = new BuildingsGenerator(ref _buildingPool, settings);
			_cloudsGenerator = new CloudsGenerator(ref _cloudPool,settings, new CloudsFactory(settings._cloudsFactorySettings));
			
			StatesList = new List<GeneratorState<MonoBehaviour>>()
			{
				new NastyUfoObjectGenerator_AwaitInputState(ref MonoPool, settings),
				new NastyUfoObjectGenerator_RunState(ref MonoPool, settings),
				new NastyUfoObjectGenerator_StopState(ref MonoPool, settings)
			};

			CurrentState = StatesList[0];
		}
		
		public override async Task Create()
		{
			await _buildingsGenerator.Create();
			await _cloudsGenerator.Create();
		}

		public override async Task Update()
		{
			await _buildingsGenerator.Update();
			await _cloudsGenerator.Update();
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