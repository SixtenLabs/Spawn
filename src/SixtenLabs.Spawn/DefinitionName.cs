using System;

namespace SixtenLabs.Spawn
{
  /// <summary>
  /// This is used for the name of a definition.
  /// 
  /// There are two possible names to set.
  /// 
  /// The original name and the translated name.
  /// 
  /// This allows for consuming a spec file that holds one name
  /// and translating it to another before creating the output.
  /// 
  /// This is useful when wrapping an interop dll which will typically
  /// have poor C# names.
  /// </summary>
  public class DefinitionName
  {
    /// <summary>
    /// This should be used whenever generating code.
    /// It will return Translated if it is set otherwise it will return Original
    /// </summary>
    public string Output
    {
      get
      {
        if(!string.IsNullOrEmpty(Translated))
        {
          return Translated;
        }
        else if(!string.IsNullOrEmpty(Original))
        {
          return Original;
        }
        else
        {
          throw new ArgumentNullException("The OriginalName or TranslatedName must be set.");
        }
      }
    }

    /// <summary>
		/// This name is typically the name used for this definition in the source data file.
		/// This value will also be used for the Translated name if the translated name is not set.
		/// </summary>
    public string Original { get; set; }

    /// <summary>
    /// Use this to change the original name if needed.
    /// If this property is null or empty the OriginalName property will be used.
    /// </summary>
    public string Translated { get; set; }
  }
}
