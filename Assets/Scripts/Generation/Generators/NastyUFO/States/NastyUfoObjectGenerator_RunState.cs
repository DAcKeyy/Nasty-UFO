using Data.Generators;
using Generation.Base;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class NastyUfoObjectGenerator_RunState : GeneratorState<MonoBehaviour>
	{
		public NastyUfoObjectGenerator_RunState(
			ref MonoPool<MonoBehaviour> pool, 
			NastyUFOLevelGeneration_Settings settings) : base(ref pool, settings)
		{
			
		}
	}
}