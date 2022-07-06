using System.Threading.Tasks;
using Data.Generators;
using Generation.Base;
using SceneBehavior.NastyUFOGame.Base;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameStartup_State : GameState
	{
		private ILevelGenerator _UfoLevelGenerator;
		private StateMachine _thisMachine;
		private NastyUFOLevelGeneration_Settings _levelGenerationSetting;
		
		public GameStartup_State(ILevelGenerator levelGenerator, StateMachine thisMachine, NastyUFOLevelGeneration_Settings levelGenerationSetting) : base()
		{
			_UfoLevelGenerator = levelGenerator;
			_thisMachine = thisMachine;
			_levelGenerationSetting = levelGenerationSetting;
		}
		
		public async override Task Enter()
		{
			_UfoLevelGenerator.SetMode(1);
			
			_UfoLevelGenerator.Create();
			
			while (true)
			{
				await Task.Delay((int)(_levelGenerationSetting._levelUpdateRate * 1000));
				Debug.Log((int)(_levelGenerationSetting._levelUpdateRate * 1000));
				_UfoLevelGenerator.Update();

			}
		}

		public async override Task Jump()
		{
			await _thisMachine.SwitchState<GameLunched_State>();
		}
	}
}