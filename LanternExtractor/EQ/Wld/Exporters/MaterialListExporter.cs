using System;
using System.Collections.Generic;
using System.Linq;
using LanternExtractor.EQ.Wld.Fragments;
using LanternExtractor.EQ.Wld.Helpers;

namespace LanternExtractor.EQ.Wld.Exporters
{
    public class MaterialListExporter : TextAssetExporter
    {
        private List<string> _processedFragments = new List<string>();
        
        public MaterialListExporter()
        {
            _export.AppendLine(LanternStrings.ExportHeaderTitle + "Material List Information");
            _export.AppendLine(LanternStrings.ExportHeaderFormat +
                               "MaterialName, FrameCount, FrameTimeMs");
        }

        public override void AddFragmentData(WldFragment data)
        {
            MaterialList list = data as MaterialList;

            if (list == null)
            {
                return;
            }

            foreach (Material material in list.Materials)
            {
                if (material.ShaderType == ShaderType.Invisible)
                {
                    continue;
                }

                if (material.BitmapInfoReference == null)
                {
                    continue;
                }

                if (_processedFragments.Contains(material.ShaderType + material.Name))
                {
                    continue;
                }

                _processedFragments.Add(material.ShaderType + material.Name);

                string materialPrefix = MaterialList.GetMaterialPrefix(material.ShaderType);
                string materialName = materialPrefix + FragmentNameCleaner.CleanName(material);

                _export.Append(materialName);
                _export.Append(",");
                _export.Append(material.BitmapInfoReference.BitmapInfo.BitmapNames.Count);
                _export.Append(",");

                if (!material.BitmapInfoReference.BitmapInfo.IsAnimated)
                {
                    _export.Append(material.GetFirstBitmapNameWithoutExtension());
                }
                else
                {
                    _export.Append(material.BitmapInfoReference.BitmapInfo.AnimationDelayMs);
                    _export.Append(",");

                    var allBitmapNames = material.GetAllBitmapNames();

                    for (var i = 0; i < allBitmapNames.Count; i++)
                    {
                        string bitmapName = allBitmapNames[i];
                        _export.Append(bitmapName);

                        if (i < allBitmapNames.Count - 1)
                        {
                            _export.Append(";");
                        }
                    }
                }
                
                _export.AppendLine();
            }
        }
    }
}