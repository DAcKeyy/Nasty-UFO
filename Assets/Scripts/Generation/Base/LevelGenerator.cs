using System;
using Generation.Contexts.NastyUFO;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Base
{
    public abstract class LevelGenerator<T> where T : MonoBehaviour
    {
        private MonoPool<T> _pool;
        
        public LevelGenerator(ref MonoPool<T> pool)
        {
            _pool = pool;
        }

        public virtual void Create()
        {
            Debug.Log("Генерирую хуйню");
        }

        public virtual void Update()
        {
            Debug.Log("Обновляю хуйню");
        }

        public virtual void SetMode(int mode)
        {
            throw new NotImplementedException($"{nameof(NastyUFOLevelGenerator)} doesn't have mode indexed as {mode}");
        }
    }
}