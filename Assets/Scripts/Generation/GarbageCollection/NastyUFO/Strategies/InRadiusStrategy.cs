using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
			if (radius <= 0) throw new Exception("GS clear radius must be greater than zero!");
			_monoPool = monoPool ?? throw new NullReferenceException();
			_radius = radius;
			_centerObj = centerObj;
		}
		
		public override Task DestroyFuckingObjects()
		{
			Vector3 centerPosition = _centerObj.transform.position;
			MonoBehaviour[] monoPoolPrefabPool =  _monoPool.PrefabPool.Values.ToArray();
			
			//TODO Этот код можно ускорить!
			foreach (var obj in monoPoolPrefabPool)
			{
				Vector3 objPosition = obj.transform.position;

				if ((math.abs(centerPosition.x - objPosition.x)) > _radius ||
				    (math.abs(centerPosition.y - objPosition.y)) > _radius ||
				    (math.abs(centerPosition.z - objPosition.z)) > _radius)
				{
					_monoPool.Destroy(obj.GetInstanceID());
				}
			}
			
			return Task.CompletedTask;
		}
	}
}