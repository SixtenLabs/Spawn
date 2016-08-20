namespace SixtenLabs.Spawn.CSharp
{
	public class ParameterDefinition : Definition
	{
    public ParameterDefinition(string name = null)
      : base(name)
    {
    }

    public bool IsPointer { get; set; }

    public DefinitionName ParameterType { get; set; } = new DefinitionName();
  }
}
