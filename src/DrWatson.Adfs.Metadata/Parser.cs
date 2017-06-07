using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DrWatson.Adfs.Metadata
{
    public class Parser
    {
        public Parser(string metadata)
            : this(new StringReader(metadata))
        {
        }

        public Parser(Stream reader)
            : this(new StreamReader(reader))
        {
        }

        public Parser(TextReader reader)
        {
            Parse(reader);
        }

        public string Identity { get; private set; }
        public string SigningCertificateString { get; private set; }
        public X509Certificate2 SigningCertificate { get => new X509Certificate2(Convert.FromBase64String(SigningCertificateString)); }

        #region Implementation

        private void Parse(TextReader reader)
        {
            var doc = XDocument.Load(reader);
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("d", "urn:oasis:names:tc:SAML:2.0:metadata");
            namespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

            Identity = LoadIdentity(doc, namespaceManager);
            SigningCertificateString = LoadSigningCertificateString(doc, namespaceManager);
        }

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