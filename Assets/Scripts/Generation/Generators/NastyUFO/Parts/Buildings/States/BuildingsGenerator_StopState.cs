using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Factories.NastyUFO;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Generation.Generators.NastyUFO.Parts.Buildings.States
{
	public class BuildingsGenerator_StopState : GeneratorState<ModularBuilding>
	{
		public BuildingsGenerator_StopState(
			MonoPool<ModularBuilding> monoPool) : base(monoPool)
		{

		}
		
		public override void Create()
		{
			Debug.Log("Я остановлен");
		}

		public override void Update()
		{
			Debug.Log("Я остановлен");
		}

		private Vector3 CalculatePlace()
		{
			
			return Vector3.Zero;
		}
	}
}