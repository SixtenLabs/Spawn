namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Base definition. Holds all common functionality for concrete definitions
	/// </summary>
  public abstract class Definition
  {
    public Definition(string name = null)
    {
      Name.OriginalName = name;
    }

    public DefinitionName Name { get; } = new DefinitionName();

		/// <summary>
		/// Use this to capture any information your spec may need.
		/// This would typically be used at the level where your processing
		/// your data before asking the Csharp Generator to generate.
		/// The CSharp generator does not use this field.
		/// </summary>
		public string Tag { get; set; }
  }
}
