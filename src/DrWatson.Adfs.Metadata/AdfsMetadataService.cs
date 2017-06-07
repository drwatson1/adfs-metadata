using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DrWatson.Adfs.Metadata
{
    public interface AdfsMetadataService
    {
        string Identity { get; }
        string SigningCertificateString { get; }

        X509Certificate2 SigningCertificate { get; }

        bool Ready { get; }

        Task Load();
    }
}