using System.Threading.Tasks;
using UnityEngine;

namespace Generation.Base
{
	//Дублирует методы генератора
	public abstract class GeneratorState
	{
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