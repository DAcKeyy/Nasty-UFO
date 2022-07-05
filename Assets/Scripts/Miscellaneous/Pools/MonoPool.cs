using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Pools
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        public List<T> PrefabPool { get; }

        public MonoPool() {
            PrefabPool = new List<T>();
        }

        public void AddObject(T prefab)
        {
            PrefabPool.Add(prefab);
        }

        public T GetLast()
        {
            try
            {
                return PrefabPool[PrefabPool.Count - 1];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
                return null;
            }
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

        public void DestroyAt(int index)
        {
            UnityEngine.Object.Destroy(PrefabPool[index].gameObject);
            PrefabPool.RemoveAt(index);
        }
    }
}
