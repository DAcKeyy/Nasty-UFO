using Miscellaneous.Pools;
using UnityEngine;

namespace Miscellaneous.Generators.ObjectGenerator
{
	//Дублирует методы генератора
	public abstract class GeneratorState<T> where T : MonoBehaviour
	{
		protected MonoPool<T> MonoPool;

		protected GeneratorState(MonoPool<T> pool)
		{
			MonoPool = pool;
		}
		
		public virtual void Create()
		{
			Debug.Log("Генерирую хуйню");
		}

		public virtual void Update()
		{
			Debug.Log("Обновляю хуйню");
		}
	}
}