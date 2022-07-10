using System.Threading.Tasks;
using Data.Generators;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Base
{
	//Дублирует методы генератора
	public abstract class GeneratorState<T> where T : MonoBehaviour
	{
		protected MonoPool<T> MonoPool;
		protected NastyUFOLevelGeneration_Settings Settings;
		
		
		protected GeneratorState(
			ref MonoPool<T> pool,
			NastyUFOLevelGeneration_Settings settings)
		{
			MonoPool = pool;
			Settings = settings;
		}
		
		public virtual Task Create()
		{
			Debug.Log("Генерирую хуйню");
			return Task.CompletedTask;
		}

		public virtual Task Update()
		{
			Debug.Log("Обновляю хуйню");
			return Task.CompletedTask;
		}
	}
}