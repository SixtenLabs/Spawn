namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Define a region directive
	/// </summary>
	public class RegionDefinition : Definition
	{
    public RegionDefinition(string name = null)
      : base(name)
    {
    }

    public bool UseRegionDirective { get; set; }
  }
}
