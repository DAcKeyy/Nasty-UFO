using System.Collections.Generic;
using Miscellaneous.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation.Base
{
	//просто методы для просчёта
	public static class GeneratorTools<T> where T : MonoBehaviour
    {
	    public static void ClearFarObjects(MonoPool<T> objList, float radius, Transform centerObj)
    	{
    		for (var i = 0; i < objList.PrefabPool.Count; i++)
    		{
    			//вытаскиваем объект
    			var obj = objList.PrefabPool[i];
    				
    			//сверяем растояние от ценра всей конетели до этого объекта
    			var distance = Vector3.Distance(centerObj.transform.position, obj.transform.position);
    				
    			//Если дистанция до радиуса чистки меньше то некст 
    			if (distance < radius) continue; 
    				
    			//и по индексу его уничтожаем
    			objList.DestroyAt(i);
    		}
    	}
	    
	    public static T GetRandomObj(List<T> list)
	    {
		    if (list == null) return null;
		    if (list.Count == 1) return list[1];
		    return list[Random.Range(0, list.Count)];
	    }

	    public static float GetBoundsDistance(Bounds bounds_A, Bounds bounds_B)
	    {
		    if (bounds_A.Intersects(bounds_B)) return 0;
			    
		    var point_A = bounds_A.ClosestPoint(bounds_B.center);
		    var point_B = bounds_B.ClosestPoint(bounds_A.center);

		    return Vector3.Distance(point_A, point_B);
	    }

	    public static bool IsInRange(Vector3 Target, Vector3 Center, float range)
	    {
		    return Vector3.Distance(Target, Center) < range;
	    }
    }
}