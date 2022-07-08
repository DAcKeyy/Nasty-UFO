using System.Collections.Generic;
using Generation.Base;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.GarbageCollection.NastyUFO
{
	public class NastyUFOGC : BusinessGarbageCollector
	{
		public NastyUFOGC(
			BusinessGarbageCollectorStrategy workingStrategy) : base(workingStrategy)
		{
			
		}
	}
}