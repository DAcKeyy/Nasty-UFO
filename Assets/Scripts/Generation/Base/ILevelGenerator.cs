namespace Scenes.Generation.Base
{
    public interface ILevelGenerator
    {
        public void Create();
        public void Update();
        public void SetMode(int mode);
    }
}