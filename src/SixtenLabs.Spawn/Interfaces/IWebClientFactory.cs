using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public interface IWebClientFactory
	{
		string DownloadSpecFromUri(string uri);
	}
}
