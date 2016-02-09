using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.DynamicParsers
{
    public interface IContentProvider
    {
        TextReader GetContent(string contentSource);
    }
    public class FileContentProvider : IContentProvider
    {
        public TextReader GetContent(string contentSource)
        {
            return File.OpenText(contentSource);
        }
    }
}
