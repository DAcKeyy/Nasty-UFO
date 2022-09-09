using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO.Buildings;
using Data.Difficulty;
using Generation.Generators.NastyUFO.Parts.Buildings.States;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Generators.NastyUFO.Parts.Buildings
{
	public class BuildingsGenerator : ObjectGenerator<ModularBuilding>
	{
		public BuildingsGenerator(
			ref MonoPool<ModularBuilding> buildingsMonoPool,
			UFO_DifficultyController difficultyController) : base(buildingsMonoPool)
		{
			StatesList = new List<GeneratorState<ModularBuilding>>()
			{
				new RunState(buildingsMonoPool, difficultyController),
				new StopState(buildingsMonoPool)
			};

			CurrentState = StatesList[0];
		}

		public override async Task Create() => await CurrentState.Create();
		
		public override async Task Update() => await CurrentState.Update();
	}
}