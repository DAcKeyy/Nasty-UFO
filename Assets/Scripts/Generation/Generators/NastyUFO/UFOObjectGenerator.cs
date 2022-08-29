using System.Collections.Generic;
using System.Threading.Tasks;
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
		private MonoPool<ModularBuilding> _buildingPool;
		private MonoPool<Cloud> _cloudPool;

		public UFOObjectGenerator(
			ref MonoPool<MonoBehaviour> monoPool,
			NastyUFOLevelGeneration_Settings settings) : base(monoPool)
		{
			InitPools();
			
			//Делаем генераторы по бизнесс логике
			var buildingsGenerator = new BuildingsGenerator(ref _buildingPool, settings);
			var cloudsGenerator = new CloudsGenerator(ref _cloudPool,settings, new CloudsFactory(settings._cloudsFactorySettings));
			
			StatesList = new List<GeneratorState<MonoBehaviour>>()
			{
				new AwaitState(ref MonoPool, settings, buildingsGenerator, cloudsGenerator),
				new RunState(ref MonoPool, settings, buildingsGenerator, cloudsGenerator),
				new StopState(ref MonoPool, settings)
			};
			CurrentState = StatesList[0];
		}
		
		public override async Task Create()
		{
			await CurrentState.Create();
		}

		public override async Task Update()
		{
			await CurrentState.Update();
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