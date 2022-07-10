using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Generation.Generators.NastyUFO.Parts.Buildings.States;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.Parts.Buildings
{
	public class BuildingsGenerator : ObjectGenerator<ModularBuilding>
	{
		public BuildingsGenerator(
			ref MonoPool<ModularBuilding> buildingsMonoPool,
			NastyUFOLevelGeneration_Settings settings) : base(ref buildingsMonoPool)
		{
			StatesList = new List<GeneratorState<ModularBuilding>>()
			{
				new BuildingsGenerator_RunState(ref buildingsMonoPool, settings)
			};

			CurrentState = StatesList[0];
		}

		public override async Task Create() => await CurrentState.Create();
		
		public override async Task Update() => await CurrentState.Update();
	}
}