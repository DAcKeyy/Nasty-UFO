using System.Threading.Tasks;
using Generation.Base;
using SceneBehavior.NastyUFOGame.Base;
using UnityEngine;

namespace SceneBehavior.NastyUFOGame.GameStates
{
	public class GameStartup_State : GameState
	{
		private ILevelGenerator _UfoLevelGenerator;
		private StateMachine _thisMachine;
		
		public GameStartup_State(ILevelGenerator levelGenerator, StateMachine thisMachine) : base()
		{
			_UfoLevelGenerator = levelGenerator;
			_thisMachine = thisMachine;
		}
		
		public override Task Enter()
		{
			_UfoLevelGenerator.Create();
			
			return Task.CompletedTask;
		}

		public async override Task Jump()
		{
			await _thisMachine.SwitchState<GameLunched_State>();
		}
	}
}