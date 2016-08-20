namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Define a namespace
	///  
	/// The namespace keyword is used to declare a scope that contains a set of related objects. 
	/// You can use a namespace to organize code elements and to create globally unique types.
	/// 
	/// Within a namespace, you can declare one or more of the following types:
	///    another namespace
	///    class
	///    interface
	///    struct
	///    enum
	///    delegate
	/// 
	/// </summary>
	public class NamespaceDefinition : Definition
	{
    public NamespaceDefinition(string name = null)
      : base(name)
    {
    }
  }
}
