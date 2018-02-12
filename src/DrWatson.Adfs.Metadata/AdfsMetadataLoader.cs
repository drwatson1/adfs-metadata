using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrWatson.Adfs.Metadata
{
    public class AdfsMetadataLoader
        : AdfsMetadataService
    {
        private Func<Task<string>> Loader;
        volatile private Task<AdfsMetadata> loadTask;

        /// <summary>
        /// Load metadata from a server specified by the base URL. The metadata Url will be constructed automatically.
        /// </summary>
        /// <param name="adfsBaseUrl">A base URL of ADFS, for example "https://fs.example.com" </param>
        public AdfsMetadataLoader(string adfsBaseUrl)
        {
            Loader = () =>
                new HttpClient().GetStringAsync(GetMetadataUrl(adfsBaseUrl));
        }

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

        private string GetMetadataUrl(string adfsBaseUrl)
        {
            return adfsBaseUrl.TrimEnd(new char[] { '/', '\\', ' ' }) + "/FederationMetadata/2007-06/FederationMetadata.xml";
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