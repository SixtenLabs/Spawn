using System.Xml.Linq;

namespace SixtenLabs.Spawn
{
  public interface IXmlFileLoader
  {
    void LoadRegistry();

    XElement Registry { get; set; }
  }
}
