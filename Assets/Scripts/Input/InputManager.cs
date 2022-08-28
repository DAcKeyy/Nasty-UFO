using UnityEngine.InputSystem;

namespace Input
{
	public class InputManager
	{
		public static InputManager CurrentInputManager;
		public readonly InputAction JumpAction;
		public readonly InputAction PauseAction;

		public InputManager(InputActionAsset actionAsset)
		{
			JumpAction = actionAsset.FindActionMap("Game").FindAction("Jump");
			PauseAction = actionAsset.FindActionMap("Game").FindAction("Pause");
		}
	}
}