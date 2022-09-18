using System.Threading.Tasks;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.States
{
	public class RunState : GeneratorState<MonoBehaviour>
	{
		private readonly ObjectGenerator<ModularBuilding> _buildingsGenerator;
		private readonly ObjectGenerator<Cloud> _cloudsGenerator;

		public RunState(
			ref MonoPool<MonoBehaviour> coinPool,
			ObjectGenerator<ModularBuilding> buildingsGenerator,
			ObjectGenerator<Cloud> cloudsGenerator) : base(coinPool)
		{
			_buildingsGenerator = buildingsGenerator;
			_cloudsGenerator = cloudsGenerator;
		}
		
		public override async Task Create()
		{
			await _buildingsGenerator.Create();
			await _cloudsGenerator.Create();
		}

		public override async Task Update()
		{
			await _buildingsGenerator.Update();
			await _cloudsGenerator.Update();
		}
	}
}