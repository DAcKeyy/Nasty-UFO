using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.GarbageCollection.NastyUFO;
using Generation.GarbageCollection.NastyUFO.Strategies;
using Generation.Generators.NastyUFO.States;
using Miscellaneous.Pools;
using SceneBehavior.NastyUFOGame.Base;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameLunched_State : GameState
	{
		private readonly ObjectGenerator<MonoBehaviour> _levelGenerator;
		private readonly NastyUFOLevelGeneration_Settings _settings;
		private MonoPool<MonoBehaviour> _monoPool;
		private readonly UFO _player;

		public GameLunched_State(
			ref ObjectGenerator<MonoBehaviour> levelGenerator, 
			NastyUFOLevelGeneration_Settings settings,
			UFO player,
			ref MonoPool<MonoBehaviour> monoPool) : base()
		{
			_monoPool = monoPool;
			_levelGenerator = levelGenerator;
			_settings = settings;
			_player = player;
		}

		public override async Task Enter()
		{
			await base.Enter();
			
			_player.BeginSweeping();
			_levelGenerator.SwitchState(new NastyUfoObjectGenerator_RunState());
			
			var gc = new NastyUFOGC(new InRadiusStrategy(ref _monoPool, _settings._clearingRange, _player.transform));
			
			while (IsActive)
			{
				await _levelGenerator.Update();
				await gc.DoJob();
				await Task.Delay((int)(_settings._levelUpdateRate * 1000));
			}
		}
	}
}