using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using SceneBehavior.NastyUFOGame.Base;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameLunched_State : GameState
	{
		private ILevelGenerator _levelGenerator;
		private NastyUFOLevelGeneration_Settings _settings;
		private UFO _player;
		
		public GameLunched_State(
			ILevelGenerator levelGenerator, 
			NastyUFOLevelGeneration_Settings settings,
			UFO player) : base()
		{
			_levelGenerator = levelGenerator;
			_settings = settings;
			_player = player;
		}

		public async override Task Enter()
		{
			_player.BeginSweeping();
			_levelGenerator.SetMode(2);
			while (true)
			{
				_levelGenerator.Update();
				Debug.Log("Не запущен");
				await Task.Delay((int)(_settings._levelUpdateRate * 1000));//ms
			}
		}
	}
}