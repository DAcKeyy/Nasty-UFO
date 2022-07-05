using System;
using Data.Generators;
using Scenes.Actors.NastyUFO;
using Scenes.Generation.Base;
using Scenes.Generation.Factories.NastyUFO;
using UnityEngine;

namespace Scenes.Generation.Contexts.NastyUFO
{
	public class NastyUFOLevelGenerator : ILevelGenerator
	{
		//Генератор сам не знает что он генерирует... хе хе
		private readonly ILevelGenerator _buildingsGenerator;
		private readonly ILevelGenerator _cloudsGenerator;
		private readonly NastyUFOLevelGenerationSettings _levelGenerationSettings;
		private readonly UFO _player;
		private Mode _generatorMod = Mode.WaitToRun;
		private Vector2 _lastPlayerPosition;
		private bool isGameStarted;
		
		//конструктора не пугаться, эти все вещи нужны генераторам
		protected NastyUFOLevelGenerator(
			NastyUFOLevelGenerationSettings settings,
			UFO player,
			Camera mainCamera)
		{
			_player = player;
			_buildingsGenerator = new BuildingsGenerator(settings, new BuildingsFactory(settings._buildingsFactorySettings), mainCamera, player);
			_cloudsGenerator = new CloudsGenerator(settings, new CloudsFactory(settings._cloudsFactorySettings), mainCamera, player);
		}
		
		public void Create()
		{
			_lastPlayerPosition = _player.transform.position;
			_buildingsGenerator.Create();
			_cloudsGenerator.Create();
		}

		public void Update()
		{
			var position = _player.transform.position;
			var playerPathDistance = (Vector2)position - _lastPlayerPosition;
			
			_buildingsGenerator.Update();
			_cloudsGenerator.Update();
			
			_lastPlayerPosition = position;
		}

		public void SetMode(int mode)
		{
			if (Enum.IsDefined(typeof(Mode), mode)) _generatorMod = (Mode) mode;
			else throw new NotImplementedException($"{nameof(NastyUFOLevelGenerator)} doesn't have mode indexed as {mode}");
		}

		private enum Mode
		{
			WaitToRun = 1,
			Run = 2
		}
	}
}