using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.GarbageCollection.NastyUFO;
using Generation.GarbageCollection.NastyUFO.Strategies;
using Generation.Generators.NastyUFO.Parts.Buildings;
using Generation.Generators.NastyUFO.States;
using Miscellaneous.Pools;
using SceneBehavior.NastyUFOGame.Base;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameStartup_State : GameState
	{
		private readonly ObjectGenerator<MonoBehaviour> _ufoObjectGenerator;
		private readonly StateMachine _thisMachine;
		private readonly UFO _player;
		private readonly NastyUFOLevelGeneration_Settings _levelGenerationSetting;
		private MonoPool<MonoBehaviour> _monoPool;
		
		public GameStartup_State(
			ref ObjectGenerator<MonoBehaviour> objectGenerator, 
			StateMachine thisMachine, 
			NastyUFOLevelGeneration_Settings levelGenerationSetting,
			ref MonoPool<MonoBehaviour> monoPool,
			UFO player)
		{
			_player = player;
			_monoPool = monoPool;
			_ufoObjectGenerator = objectGenerator;
			_thisMachine = thisMachine;
			_levelGenerationSetting = levelGenerationSetting;
		}
		
		public override async Task Enter()
		{
			await base.Enter();
			_ufoObjectGenerator.SwitchState(new NastyUfoObjectGenerator_AwaitInputState(ref _monoPool, _levelGenerationSetting));
			await _ufoObjectGenerator.Create();
			
			NastyUFOGC gc = new NastyUFOGC(new InRadiusStrategy(ref _monoPool, _levelGenerationSetting._clearingRange, _player.transform));
			
			while (IsActive)
			{
				await Task.Delay((int)(_levelGenerationSetting._levelUpdateRate * 1000));
				await _ufoObjectGenerator.Update();
				await gc.DoJob();
			}
		}

		public override async Task Jump()
		{
			await _thisMachine.SwitchState<GameLunched_State>();
		}
	}
}