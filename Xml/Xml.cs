using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Utilities
{
    public static class Xml
    {
        /// <summary>
        /// Parse an object to Xml and write it to file system.
        /// </summary>
        /// <param name="obj">Object who will be write in Xml</param>
        /// <param name="filename">Path where will be write Xml file</param>
        public static void WriteToXml(object obj, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(writer, obj);
                writer.Flush();
            }
        }

        /// <summary>
        /// Parse an object to Xml file and reutrn the file as bytes array.
        /// </summary>
        /// <param name="obj">Object who will be write in Xml</param>
        public static Byte[] WriteToXmlByteArray(object obj)
        {
            using (MemoryStream writer = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(writer, obj);
                return writer.ToArray();
            }
        }

        /// <summary>
        /// Check if xml file has a valid structure with Xsd
        /// </summary>
        /// <param name="content">Xml file content</param>
        /// <param name="filenamlXsd">Xsd path</param>
        /// <param name="nameSpace">Xsd namespace</param>
        /// <returns></returns>
        public static bool HasValidXmlStructure(byte[] content, string filenamlXsd, string nameSpace = "http://consob.it/priips/feedbackfile/v1")
        {
            bool result = true;

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(nameSpace, filenamlXsd);
            XDocument custOrdDoc = XDocument.Load(new MemoryStream(content));

            custOrdDoc.Validate(schemas, (o, e) =>
            {
                result = false;
            });

            return result;
        }

        /// <summary>
        /// Check if xml file has a valid structure with Xsd
        /// </summary>
        /// <param name="filenameXml">Xml file path</param>
        /// <param name="filenamlXsd">Xsd path</param>
        /// <param name="nameSpace">Xsd namespace</param>
        /// <returns></returns>
        public static bool HasValidXmlStructure(string filenameXml, string filenamlXsd, string nameSpace = "")
        {
            bool result = true;

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(nameSpace, filenamlXsd);
            XDocument custOrdDoc = XDocument.Load(filenameXml);

            custOrdDoc.Validate(schemas, (o, e) =>
            {
                result = false;
            });

            return result;
        }

        /// <summary>
        /// Get messages of validation xml structure
        /// </summary>
        /// <param name="filenameXml">Xml file path</param>
        /// <param name="filenamlXsd">Xsd path</param>
        /// <param name="nameSpace">Xsd namespace</param>
        /// <returns></returns>
        public static string GetValidationMessageXml(string filenameXml, string filenamlXsd, string nameSpace = "")
        {
            StringBuilder sb = new StringBuilder();
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(nameSpace, filenamlXsd);
            XDocument custOrdDoc = XDocument.Load(filenameXml);

            custOrdDoc.Validate(schemas, (o, e) =>
            {
                sb.AppendLine(e.Message);
            });

            return sb.ToString();
        }
    }
}
