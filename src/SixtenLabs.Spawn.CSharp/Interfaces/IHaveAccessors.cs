namespace SixtenLabs.Spawn.CSharp
{
  public interface IHaveAccessors
  {
    AccessorDefinition Getter { get; set; }

    AccessorDefinition Setter { get; set; }
  }
}
