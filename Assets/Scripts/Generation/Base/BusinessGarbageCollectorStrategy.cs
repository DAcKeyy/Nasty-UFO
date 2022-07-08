using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Base
{
	public abstract class BusinessGarbageCollectorStrategy
	{
		private readonly MonoPool<MonoBehaviour> _pool;
		
		public BusinessGarbageCollectorStrategy(ref MonoPool<MonoBehaviour> monos)
		{
			_pool = monos;
		}

		public virtual void DestroyFuckingObjects()
		{
			for (int i = 0; i < _pool.PrefabPool.Count; i++)
			{
				_pool.PrefabPool.RemoveAt(i);
			}
		}
	}
}