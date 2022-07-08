using System.Collections.Generic;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Base
{
	public abstract class BusinessGarbageCollector
	{
		private BusinessGarbageCollectorStrategy _currentStrategy;

		//Ну типа идея такая что собрищик хуйни не знает что он делает и можно будет потом менять контекст его работы
		protected BusinessGarbageCollector(
			BusinessGarbageCollectorStrategy workingStrategy)
		{
			_currentStrategy = workingStrategy;
		}
		
		public virtual void DoJob()
		{
			_currentStrategy.DestroyFuckingObjects();
		}
	}
}
