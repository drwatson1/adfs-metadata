using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DrWatson.Adfs.Metadata
{
    public interface AdfsMetadataService
    {
        Task<AdfsMetadata> Get();
        void Invalidate();
    }
}