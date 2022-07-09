using Actors.NastyUFO;
using Data.Generators;
using SceneBehavior.NastyUFOGame;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private LevelGenerationSettings_ScriptableObject _gameSettings;
		[SerializeField] private InputActionAsset _inputActionAsset;
		[SerializeField] private UFO _player;

		private InputAction _jumpAction;
		private InputAction _pauseAction;
		private UFOGameSystem _theGame;
		
		public void Start()
		{
			//По сути я ебать как скрыл логику игры что на сверхверхнем уровне можно заняться чем-нибудь другим
			_theGame = new UFOGameSystem(_gameSettings._settings, _player);

			_jumpAction = _inputActionAsset.FindActionMap("Game").FindAction("Jump");
			_pauseAction = _inputActionAsset.FindActionMap("Game").FindAction("Pause");
			_jumpAction.performed += context => _theGame.Jump();
			_pauseAction.performed += context => _theGame.Pause();
			
			_theGame.Start();
		}
	}
}