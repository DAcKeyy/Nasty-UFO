using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Factories.NastyUFO;
using Generation.Generators.NastyUFO.Parts.Clouds.States;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.Pools;

namespace Generation.Contexts.NastyUFO
{
	public class CloudsGenerator : ObjectGenerator<Cloud>
	{
		public CloudsGenerator(
			ref MonoPool<Cloud> cloudsMonoPool,
			NastyUFOLevelGeneration_Settings settings, 
			CloudsFactory cloudsFactory) : base(cloudsMonoPool)
		{
			StatesList = new List<GeneratorState<Cloud>>()
			{
				new AwaitingState(ref cloudsMonoPool, settings, cloudsFactory)
			};

			CurrentState = StatesList[0];
		}
		
		//TODO Создавать облака не только в даль по игре но и в ширь, в горизонт
		
		public override Task Create() => CurrentState.Create();
		
		public override Task Update() => CurrentState.Update();
	}
}
