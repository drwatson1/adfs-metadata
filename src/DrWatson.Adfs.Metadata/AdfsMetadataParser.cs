using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DrWatson.Adfs.Metadata
{
    public static class AdfsMetadataParser
    {
        public static AdfsMetadata Parse(string metadata)
        {
            return Parse(new StringReader(metadata));
        }

        public static AdfsMetadata Parse(Stream reader)
        {
            return Parse(new StreamReader(reader));
        }

        public static AdfsMetadata Parse(TextReader reader)
        {
            var doc = XDocument.Load(reader);
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("d", "urn:oasis:names:tc:SAML:2.0:metadata");
            namespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

            return new AdfsMetadata
            {
                Identity = LoadIdentity(doc, namespaceManager),
                SigningCertificateString = LoadSigningCertificateString(doc, namespaceManager)
            };
        }

        #region Implementation
        private static string LoadSigningCertificateString(XDocument doc, XmlNamespaceManager namespaceManager)
        {
            return doc.XPathSelectElement("/d:EntityDescriptor/ds:Signature/ds:KeyInfo/ds:X509Data/ds:X509Certificate", namespaceManager)?.Value;
        }

        private static string LoadIdentity(XDocument doc, XmlNamespaceManager namespaceManager)
        {
            return doc.XPathSelectElement("/d:EntityDescriptor", namespaceManager)?.Attribute("entityID")?.Value;
        }
        #endregion Implementation
    }
}