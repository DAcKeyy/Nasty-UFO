using System.Threading.Tasks;
using Actors.Movement;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.GarbageCollection.NastyUFO;
using Generation.GarbageCollection.NastyUFO.Strategies;
using Generation.Generators.NastyUFO.States;
using Miscellaneous.Generators.ObjectGenerator;
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
		private readonly StateMachine _thisMachine;
		private readonly UFOMovement _ufoMovement;

		public GameLunched_State(
			ref ObjectGenerator<MonoBehaviour> levelGenerator, 
			NastyUFOLevelGeneration_Settings settings,
			UFO player, UFOMovement ufoMovement,
			StateMachine thisMachine, 
			ref MonoPool<MonoBehaviour> monoPool)
		{
			_ufoMovement = ufoMovement;
			_thisMachine = thisMachine;
			_monoPool = monoPool;
			_levelGenerator = levelGenerator;
			_settings = settings;
			_player = player;
		}

		public override void Enter()
		{
			base.Enter();
			
			_player.BeginSweeping();
			_player.Died +=  ufo => _thisMachine.SwitchState<GameEnded_State>();
			_levelGenerator.SwitchState(new UFOObjectGenerator_RunState(ref _monoPool, _settings));
			
			var gc = new NastyUFOGC(new InRadiusStrategy(ref _monoPool, _settings._clearingRange, _player.transform));
			
			while (IsActive)
			{
				_levelGenerator.Update();
				gc.DoJob();
				Task.Delay((int)(_settings._levelUpdateRate * 1000));
			}
		}

		public override void Jump()
		{
			//TODO Либо тут либо в тарелке делать задержку пока идёт прыжок
			
			_ufoMovement.Jump();
		}
	}
}