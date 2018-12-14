using System.IO;
using System.Xml.Serialization;

namespace OriginAPI
{
    /// <summary>
    /// https://www.codeproject.com/Articles/1163664/Convert-XML-to-Csharp-Object
    /// </summary>
    public class XMLSerializer
    {
        public T Deserialize<T>(string input) where T : class {
            var ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input)) {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize) {
            var xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter()) {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }
    }
}
