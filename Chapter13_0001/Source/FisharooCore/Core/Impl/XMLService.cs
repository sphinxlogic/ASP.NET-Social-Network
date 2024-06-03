// good articles for serializing
// http://msdn.microsoft.com/en-us/magazine/cc301761.aspx
// http://msdn.microsoft.com/en-us/magazine/cc301767.aspx
// http://msdn.microsoft.com/en-us/magazine/cc188950.aspx


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class XMLService : IXMLService
    {
        public XMLService()
        {
            
        }

        public static string Serialize<T>(T objectToSerialize)
        {
            string result;
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, new UTF8Encoding());
            XmlSerializer serializer =
                    new XmlSerializer(typeof(T));
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = ' ';
            writer.Indentation = 3;
            serializer.Serialize(writer, objectToSerialize);
            byte[] Result = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(Result, 0, (int)ms.Length);
            result = Encoding.UTF8.GetString(Result, 0, (int)ms.Length);

            return result;
        }

        public static T Deserialize<T>(string stringToDeserialize)
        {
            XmlReader reader = XmlReader.Create(new StringReader(stringToDeserialize));
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result;
            result = (T)serializer.Deserialize(reader);
            return result;
        }
    }
}
