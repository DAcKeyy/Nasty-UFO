using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	public class InputManager
	{
		public static InputManager CurrentInputManager
		{
			get => currentInputManager;//TODO Можно добаить поиск экешен карты через Resourses.Load() но нахрена?..
			set => currentInputManager = value;
		}
		public readonly InputAction JumpAction;
		public readonly InputAction PauseAction;
		public readonly InputAction GameExitAction;
		
		private static InputManager currentInputManager;
		
		public InputManager(InputActionAsset actionAsset, PlayerInput playerInput)
		{
			JumpAction = actionAsset.FindActionMap("UI").FindAction("Jump");
			PauseAction = actionAsset.FindActionMap("UI").FindAction("Pause");
			GameExitAction = actionAsset.FindActionMap("UI").FindAction("Exit");
			
			JumpAction.Enable();
			PauseAction.Enable();
			GameExitAction.Enable();
		}
	}
}