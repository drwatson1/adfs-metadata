# ADFS Metadata Parser
It is a pack of simple utilities to load and parse ADFS metadata. Parser was tested on ADFS 3.0.

## Installing

```
Install-Package DrWatson.Adfs.Metadata
```

## Usage

```
AdfsMetadataServiceAsync svc = new AdfsMetadataServiceAsync(() =>
{
    return new HttpClient().GetStringAsync(
        "https://fs.example.com/FederationMetadata/2007-06/FederationMetadata.xml"
    );
});
// Exception can be thrown
await svc.Load();

if(svc.Ready)
{
    X509Certificate2 signingCert = svc.SigningCertificate();
    string identity = svc.Identity;
}
```