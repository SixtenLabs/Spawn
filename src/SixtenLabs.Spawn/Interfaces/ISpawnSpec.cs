using AutoMapper;

namespace SixtenLabs.Spawn
{
	public interface ISpawnSpec<T> where T : class
	{
		void ProcessRegistry();

		void CreateSpecTree(IMapper mapper);

		T SpecTree { get; }

    ISpecMapper SpecMapper { get; }
  }
}
