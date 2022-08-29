using System.Threading.Tasks;
using Miscellaneous.Pools;
using UnityEngine;

namespace Miscellaneous.Generators.ObjectGenerator
{
	//Дублирует методы генератора
	public abstract class GeneratorState<T> where T : MonoBehaviour
	{
		protected readonly MonoPool<T> MonoPool;

		protected GeneratorState(MonoPool<T> pool)
		{
			MonoPool = pool;
		}

		public virtual Task OnEnter()
		{
			Debug.Log($"{GetType().Name} state enter OnEnter");
			return Task.CompletedTask;
		}
		
		public virtual Task Create()
		{
			Debug.Log($"{GetType().Name} state enter Create");
			return Task.CompletedTask;
		}

		public virtual Task Update()
		{
			Debug.Log($"{GetType().Name} state enter Update");
			return Task.CompletedTask;
		}
		
		public virtual Task OnExit()
		{
			Debug.Log($"{GetType().Name} state enter OnExit");
			return Task.CompletedTask;
		}
	}
}