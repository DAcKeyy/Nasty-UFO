using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Miscellaneous.Pools
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        public Dictionary<int,T> PrefabPool { get; }
        public Action<T> ObjectAdded = delegate(T behaviour) {  };
        public Action<T> ObjectRemoved = delegate(T behaviour) {  };
        public Action<T> ObjectChanged = delegate(T behaviour) {  };

        public MonoPool() {
            PrefabPool = new Dictionary<int,T>();
        }

        public void AddObject(T prefab)
        {
            PrefabPool.Add(prefab.GetInstanceID(), prefab);
            ObjectAdded(prefab);
        }

        public T GetLast()
        {
            if(PrefabPool.Count != 0) return null;
            else return PrefabPool.Last().Value;
        }
        
        public T GetFirst()
        {
            if(PrefabPool.Count != 0) return null;
            else return PrefabPool.First().Value;
        }

        public void ChangeObject(T monoBehaviour)
        {
            ObjectChanged(monoBehaviour);
        }

        public void Destroy(int id)
        {
            if (PrefabPool.ContainsKey(id))
            {
                var obj = PrefabPool[id];
                ObjectRemoved(obj);
                UnityEngine.Object.Destroy(obj.gameObject);
            }
            
            PrefabPool.Remove(id);
        }
    }
}
