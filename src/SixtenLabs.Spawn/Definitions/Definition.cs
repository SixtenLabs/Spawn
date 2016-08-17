using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public class Definition
	{
		private string translatedName;

		/// <summary>
		/// This name is typically the name used for this definition in the source data file.
		/// This is required. This value will also be used for the Translated name if the translated name is not set.
		/// </summary>
		public string SpecName { get; set; }

		/// <summary>
		/// This is the name that will be used by the generator for this definition.
		/// So if this is a class definition this will be the generated class name.
		/// If this is not set it will use the spec name.
		/// </summary>
		public string TranslatedName
		{
			get
			{
				if (string.IsNullOrEmpty(translatedName))
				{
					return SpecName;
				}
				else
				{
					return translatedName;
				}
			}

			set
			{
				translatedName = value;
			}
		}

		/// <summary>
		/// Use this to capture any information your spec may need.
		/// This would typically be used at the level where your processing
		/// your data before asking the Csharp Generator to generate.
		/// The CSharp generator does not use this field.
		/// </summary>
		public string Tag { get; set; }
	}
}
