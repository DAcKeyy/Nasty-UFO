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
			ref MonoPool<ModularBuilding> buildingsPool) : base(ref buildingsPool)
		{
			StatesList = new List<GeneratorState>()
			{
				new BuildingsGenerator_RunState()
			};

			CurrentState = StatesList[0];
		}

		public override async Task Create() => await CurrentState.Create();
		
		public override async Task Update() => await CurrentState.Update();
	}
}