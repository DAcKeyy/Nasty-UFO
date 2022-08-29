using System.Threading.Tasks;
using Actors.NastyUFO.Buildings;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Generation.Generators.NastyUFO.Parts.Buildings.States
{
	public class StopState : GeneratorState<ModularBuilding>
	{
		public StopState(
			MonoPool<ModularBuilding> monoPool) : base(monoPool)
		{

		}
		
		public override Task Create()
		{
			Debug.Log("Я остановлен");
			
			return Task.CompletedTask;
		}

		public override Task Update()
		{
			Debug.Log("Я остановлен");
			
			return Task.CompletedTask;
		}

		private Vector3 CalculatePlace()
		{
			return Vector3.Zero;
		}
	}
}