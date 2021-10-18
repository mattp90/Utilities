using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Utilities
{
    public static class Xml
    {
        public static void main()
        {
            
        }

        /// <summary>
        ///   Parse an object to Xml and write it to file system.
        /// </summary>
        /// <param name="obj">Object who will be write in Xml</param>
        /// <param name="filename">Path where will be write Xml file</param>
        public static void WriteToXml(object obj, string filename)
        {
            try
            {
                using (var writer = new StreamWriter(filename))
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(writer, obj);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///   Parse an object to Xml file and reutrn the file as bytes array.
        /// </summary>
        /// <param name="obj">Object who will be write in Xml</param>
        public static Byte[] WriteToXmlByteArray(object obj)
        {
            try
            {
                using (var writer = new MemoryStream())
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(writer, obj);
                    return writer.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///   Check if xml file has a valid structure with Xsd
        /// </summary>
        /// <param name="content">Xml file content</param>
        /// <param name="filenamlXsd">Xsd path</param>
        /// <param name="nameSpace">Xsd namespace</param>
        /// <returns></returns>
        public static bool HasValidXmlStructure(byte[] content, string filenamlXsd, string nameSpace = "http://consob.it/priips/feedbackfile/v1")
        {
            try
            {
                bool result = true;

                var schemas = new XmlSchemaSet();
                schemas.Add(nameSpace, filenamlXsd);
                var custOrdDoc = XDocument.Load(new MemoryStream(content));

                custOrdDoc.Validate(schemas, (o, e) =>
                {
                    // e.Message;
                    result = false;
                });

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        ///   Check if xml file has a valid structure with Xsd
        /// </summary>
        /// <param name="filenameXml">Xml file path</param>
        /// <param name="filenamlXsd">Xsd path</param>
        /// <param name="nameSpace">Xsd namespace</param>
        /// <returns></returns>
        public static bool HasValidXmlStructure(string filenameXml, string filenamlXsd, string nameSpace = "http://consob.it/priips/feedbackfile/v1")
        {
            try
            {
                bool result = true;

                var schemas = new XmlSchemaSet();
                schemas.Add(nameSpace, filenamlXsd);
                var custOrdDoc = XDocument.Load(filenameXml);

                custOrdDoc.Validate(schemas, (o, e) =>
                {
                    // result = e.Message;
                    result = false;
                });

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
