using System.Threading.Tasks;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Contexts.NastyUFO;
using Generation.Generators.NastyUFO.Parts.Buildings;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class AwaitState : GeneratorState<MonoBehaviour>
	{
		private readonly BuildingsGenerator _buildingsGenerator;
		private readonly CloudsGenerator _cloudsGenerator;

		public AwaitState(
			ref MonoPool<MonoBehaviour> pool,
			BuildingsGenerator buildingsGenerator,
			CloudsGenerator cloudsGenerator) : base(pool)
		{
			_buildingsGenerator = buildingsGenerator;
			_cloudsGenerator = cloudsGenerator;
		}
		
		public override async Task Create()
		{
			await _buildingsGenerator.Create();
			await _cloudsGenerator.Create();
			await Task.CompletedTask;
		}

		public override async Task Update()
		{
			await _buildingsGenerator.Update();
			await _cloudsGenerator.Update();
			await Task.CompletedTask;
		}
	}
}