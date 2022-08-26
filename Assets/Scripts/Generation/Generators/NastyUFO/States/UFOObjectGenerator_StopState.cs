using Data.Generators;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class UFOObjectGenerator_StopState : GeneratorState<MonoBehaviour>
	{
		public UFOObjectGenerator_StopState(
			ref MonoPool<MonoBehaviour> pool, 
			NastyUFOLevelGeneration_Settings settings) : base(pool)
		{
			
		}
		
		
	}
}