using System.Threading.Tasks;
using Generation.Base;
using Miscellaneous.GC;
using Miscellaneous.Pools;
using UnityEngine;
using Unity.Mathematics;

namespace Generation.GarbageCollection.NastyUFO.Strategies
{
	public class InRadiusStrategy : BusinessGarbageCollectorStrategy
	{
		private readonly MonoPool<MonoBehaviour> _monoPool;
		private readonly float _radius;
		private readonly Transform _centerObj;
		
		public InRadiusStrategy(
			ref MonoPool<MonoBehaviour> monoPool,
			float radius,
			Transform centerObj) : base(ref monoPool)
		{
			_monoPool = monoPool;
			_radius = radius;
			_centerObj = centerObj;
		}
		
		public override Task DestroyFuckingObjects()
		{
			Vector3 centerPosition = _centerObj.transform.position;
			
			for (var i = 0; i < _monoPool.PrefabPool.Count; i++)
			{
				Vector3 objPosition = _monoPool.PrefabPool[i].transform.position;
				
				if(math.abs(objPosition.x - centerPosition.x) > _radius) _monoPool.DestroyAt(i);
				if(math.abs(objPosition.y - centerPosition.y) > _radius) _monoPool.DestroyAt(i);
				if(math.abs(objPosition.z - centerPosition.z) > _radius) _monoPool.DestroyAt(i);
			}
			
			return Task.CompletedTask;
		}
	}
}