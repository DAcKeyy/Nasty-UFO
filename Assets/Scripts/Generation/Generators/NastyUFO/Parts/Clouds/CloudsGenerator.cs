using System.Collections.Generic;
using System.Threading.Tasks;
using Actors.NastyUFO;
using Data.Generators;
using Generation.Base;
using Generation.Contexts.NastyUFO.States;
using Generation.Factories.NastyUFO;
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
				new CloudsGenerator_AwaitngState(ref cloudsMonoPool, settings, cloudsFactory)
			};

			CurrentState = StatesList[0];
		}
		
		//TODO Создавать облака не только в даль по игре но и в ширь, в горизонт
		
		public override void Create() => CurrentState.Create();
		
		public override void Update() => CurrentState.Update();
	}
}
