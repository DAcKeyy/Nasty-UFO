using Data.Generators;
using Generation.Base;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class NastyUfoObjectGenerator_StopState : GeneratorState<MonoBehaviour>
	{
		public NastyUfoObjectGenerator_StopState(
			ref MonoPool<MonoBehaviour> pool, 
			NastyUFOLevelGeneration_Settings settings) : base(ref pool, settings)
		{
			
		}
		
		
	}
}