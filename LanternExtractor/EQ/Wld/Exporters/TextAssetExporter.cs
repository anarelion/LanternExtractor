using System.IO;
using System.Text;
using LanternExtractor.EQ.Wld.Fragments;

namespace LanternExtractor.EQ.Wld.Exporters
{
    public abstract class TextAssetExporter
    {
        protected StringBuilder _export = new StringBuilder();

        public abstract void AddFragmentData(WldFragment data);

        public void WriteAssetToFile(string fileName)
        {
            string directory = Path.GetDirectoryName(fileName);

            if (string.IsNullOrEmpty(directory))
            {
                return;
            }
            
            Directory.CreateDirectory(directory);
            
            File.WriteAllText(fileName, _export.ToString());
        }
    }
}