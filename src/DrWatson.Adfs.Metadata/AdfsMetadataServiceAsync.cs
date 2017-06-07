using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DrWatson.Adfs.Metadata
{
    public class AdfsMetadataServiceAsync
        : AdfsMetadataService
    {
        private Func<Task<string>> Loader;
        private Task<string> loadTask;
        private AdfsMetadataParser parser;

        public AdfsMetadataServiceAsync(Func<Task<string>> loader)
        {
            Loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public string Identity
        {
            get
            {
                return Ready ? parser.Identity : throw new MetadataServiceException(MetadataServiceException.ErrorCode.NotReady);
            }
        }

        public string SigningCertificateString
        {
            get
            {
                return Ready ? parser.SigningCertificateString : throw new MetadataServiceException(MetadataServiceException.ErrorCode.NotReady);
            }
        }

        public X509Certificate2 SigningCertificate
        {
            get
            {
                return Ready ? parser.SigningCertificate : throw new MetadataServiceException(MetadataServiceException.ErrorCode.NotReady);
            }
        }

        public bool Ready { get; private set; }

        public async Task Load()
        {
            var metadata = await Loader();
            parser = new AdfsMetadataParser(metadata);
            Ready = true;
        }
    }
}