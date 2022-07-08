using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.GarbageCollection.NastyUFO;
using Generation.GarbageCollection.NastyUFO.Strategies;
using Miscellaneous.Pools;
using SceneBehavior.NastyUFOGame.Base;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameStartup_State : GameState
	{
		private readonly LevelGenerator<MonoBehaviour> _ufoLevelGenerator;
		private readonly StateMachine _thisMachine;
		private readonly UFO _player;
		private readonly NastyUFOLevelGeneration_Settings _levelGenerationSetting;
		private MonoPool<MonoBehaviour> _monoPool;
		
		public GameStartup_State(
			ref LevelGenerator<MonoBehaviour> levelGenerator, 
			StateMachine thisMachine, 
			NastyUFOLevelGeneration_Settings levelGenerationSetting,
			ref MonoPool<MonoBehaviour> monoPool,
			UFO player) : base()
		{
			_player = player;
			_monoPool = monoPool;
			_ufoLevelGenerator = levelGenerator;
			_thisMachine = thisMachine;
			_levelGenerationSetting = levelGenerationSetting;
		}
		
		public async override Task Enter()
		{
			_ufoLevelGenerator.SetMode(1);
			
			_ufoLevelGenerator.Create();
			NastyUFOGC GC = new NastyUFOGC(new InRadiusStrategy(ref _monoPool, _levelGenerationSetting._clearingRange, _player.transform));
			
			while (true)
			{
				await Task.Delay((int)(_levelGenerationSetting._levelUpdateRate * 1000));
				_ufoLevelGenerator.Update();
				GC.DoJob();
			}
		}

		public async override Task Jump()
		{
			await _thisMachine.SwitchState<GameLunched_State>();
		}
	}
}