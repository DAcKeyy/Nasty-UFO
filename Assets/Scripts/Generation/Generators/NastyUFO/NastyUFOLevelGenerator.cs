using System;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Generation.GarbageCollection.NastyUFO;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Contexts.NastyUFO
{
	public class NastyUFOLevelGenerator : LevelGenerator<MonoBehaviour>
	{
		private readonly LevelGenerator<ModularBuilding> _buildingsGenerator;
		private readonly LevelGenerator<Cloud> _cloudsGenerator;
		
		private readonly NastyUFOLevelGeneration_Settings _levelGenerationSettings;
		
		private MonoPool<MonoBehaviour> _monoPool;
		private MonoPool<ModularBuilding> _buildingPool;
		private MonoPool<Cloud> _cloudPool;
		
		private Mode _generatorMod = Mode.WaitToRun;
		private bool isGameStarted;
		
		public NastyUFOLevelGenerator(
			ref MonoPool<MonoBehaviour> monoPool,
			NastyUFOLevelGeneration_Settings settings,
			UFO player,
			Camera mainCamera) : base(ref monoPool)
		{
			_monoPool = monoPool;
			//Создаём пулы
			_buildingPool = new MonoPool<ModularBuilding>();
			_cloudPool = new MonoPool<Cloud>();
			//Собскрайбимся к тому что там создалось и прокидываем в основной пул
			_buildingPool.ObjectAdded += building => _monoPool.AddObject(building);
			_cloudPool.ObjectAdded += cloud => _monoPool.AddObject(cloud);
			//Делаем генераторы по бизнесс логике
			_buildingsGenerator = new BuildingsGenerator(ref _buildingPool,settings, new BuildingsFactory(settings._buildingsFactorySettings), mainCamera, player);
			_cloudsGenerator = new CloudsGenerator(ref _cloudPool,settings, new CloudsFactory(settings._cloudsFactorySettings), mainCamera, player);
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

		public override void SetMode(int mode)
		{
			if (Enum.IsDefined(typeof(Mode), mode)) 
				_generatorMod = (Mode) mode;
			else base.SetMode(mode);
		}

		private enum Mode
		{
			WaitToRun = 1,
			Run = 2
		}
	}
}