using Actors.NastyUFO;
using Generation.Base;
using Miscellaneous.Pools;

namespace Generation.Contexts.NastyUFO
{
	public class LandGenerator : LevelGenerator<Land>
	{
		private MonoPool<Land> _landPool;
		
		public LandGenerator(ref MonoPool<Land> pool) : base(ref pool)
		{
			_landPool = pool;
		}
		public override void Create()
		{
			throw new System.NotImplementedException();
		}

		public override void Update()
		{
			throw new System.NotImplementedException();
		}

		public override void SetMode(int mode)
		{
			throw new System.NotImplementedException();
		}
	}
}
