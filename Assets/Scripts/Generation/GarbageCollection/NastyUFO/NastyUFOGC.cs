using Generation.Base;
using Miscellaneous.GC;

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