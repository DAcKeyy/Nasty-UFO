using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Actors.NastyUFO.Buildings;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Generation.Generators.NastyUFO.Parts.Buildings.States;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;
using UnityEngine;

namespace Generation.Generators.NastyUFO.Parts.Buildings
{
	public class BuildingsGenerator : ObjectGenerator<ModularBuilding>
	{
		public BuildingsGenerator(
			ref MonoPool<ModularBuilding> buildingsMonoPool,
			NastyUFOLevelGeneration_Settings settings) : base(buildingsMonoPool)
		{
			StatesList = new List<GeneratorState<ModularBuilding>>()
			{
				new BuildingsGenerator_RunState(buildingsMonoPool, settings),
				new BuildingsGenerator_StopState(buildingsMonoPool)
			};

			CurrentState = StatesList[0];
		}

		public override void Create() => CurrentState.Create();
		
		public override void Update() => CurrentState.Update();
	}
}