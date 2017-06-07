using System;
using System.Security.Cryptography.X509Certificates;

namespace DrWatson.Adfs.Metadata
{
    public class AdfsMetadata
    {
        public string Identity { get; set; }
        public string SigningCertificateString { get; set; }
    }
}