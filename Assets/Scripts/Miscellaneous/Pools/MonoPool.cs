using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Miscellaneous.Pools
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        public List<T> PrefabPool { get; }
        public Action<T> ObjectAdded = delegate(T behaviour) {  };
        public Action<T> ObjectRemoved = delegate(T behaviour) {  };
        public Action<T> ObjectChanged = delegate(T behaviour) {  };

        public MonoPool() {
            PrefabPool = new List<T>();
        }

        public void AddObject(T prefab)
        {
            PrefabPool.Add(prefab);
            ObjectAdded(prefab);
        }

        public T GetLast()
        {
            //TODO Этот код можно ускорить

            var LastOne = PrefabPool.Last();
            if (PrefabPool.Last() == null)
            {
                //
            }
            else
            {
                
            }
            
            return LastOne;
        }
        
        public T GetFirst()
        {
            try
            {
                return PrefabPool[0];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void ChangeObject(T monoBehaviour)
        {
            ObjectChanged(monoBehaviour);
        }
        
        public void DestroyThis(int index)
        {
            ObjectRemoved(PrefabPool[index]);
            UnityEngine.Object.Destroy(PrefabPool[index].gameObject);
            PrefabPool.RemoveAt(index);
        }
    }
}
