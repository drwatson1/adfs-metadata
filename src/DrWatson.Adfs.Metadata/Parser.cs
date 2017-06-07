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
            namespaceManager.AddNamespace(String.Empty, "urn:oasis:names:tc:SAML:2.0:metadata");

            Identity = LoadIdentity(doc, namespaceManager);
        }

        private string LoadIdentity(XDocument doc, XmlNamespaceManager namespaceManager)
        {
            return doc.XPathSelectElement("/EntityDescriptor/@entityID")?.Value;
        }

        #endregion
    }
}
