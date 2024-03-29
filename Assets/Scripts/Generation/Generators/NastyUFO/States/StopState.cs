﻿using Data.Generators;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class StopState : GeneratorState<MonoBehaviour>
	{
		public StopState(
			ref MonoPool<MonoBehaviour> coinPool) : base(coinPool)
		{
			
		}
	}
}