using System.Threading.Tasks;
using Miscellaneous.StateMachines.Base;
using UI.Canvases.Loading;

namespace SceneBehavior.Loading.States
{
	public class LoadingGameMachineState: MachineState
	{
		private LoadingCanvas _loadingCanvas;
		
		public LoadingGameMachineState(LoadingCanvas loadingCanvas)
		{
			_loadingCanvas = loadingCanvas;
		}

		public override Task OnEnter()
		{
			_loadingCanvas.gameObject.SetActive(true);
			return Task.CompletedTask;
		}

		public override Task OnExit()
		{
			if(_loadingCanvas != null) _loadingCanvas.gameObject.SetActive(false);
			return Task.CompletedTask;
		}
	}
}