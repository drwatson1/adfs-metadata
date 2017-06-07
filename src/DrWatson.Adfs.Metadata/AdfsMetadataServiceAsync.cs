using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DrWatson.Adfs.Metadata
{
    public class AdfsMetadataLoader
        : AdfsMetadataService
    {
        private Func<Task<string>> Loader;
        volatile private Task<AdfsMetadata> loadTask;

        public AdfsMetadataLoader(Func<Task<string>> loader)
        {
            Loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public Task<AdfsMetadata> Get()
        {
            if (loadTask != null)
                return loadTask;

            lock(this)
            {
                if (loadTask != null)
                    return loadTask;

                InternalInvalidate();

                return loadTask;
            }
        }

        public void Invalidate()
        {
            lock (this)
            {
                InternalInvalidate();
            }
        }
        
        private void InternalInvalidate()
        {
            loadTask = StartLoading();
        }

        private async Task<AdfsMetadata> StartLoading()
        {
            return AdfsMetadataParser.Parse(await Loader());
        }
    }
}