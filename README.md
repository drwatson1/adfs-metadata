# ADFS Metadata Parser
[![NuGet Status](https://img.shields.io/nuget/v/DrWatson.Adfs.Metadata.svg?style=flat)](https://www.nuget.org/packages/DrWatson.Adfs.Metadata)

It is a small C# library to load and parse ADFS metadata. Parser was tested on ADFS 3.0.

## Features

* Asynchronous loading a metadata directly from ADFS 3.0 server
* Caching metadata between calls
* Ability to reload if needed
* DI-Container friendly
* Supported metadata
    * Federation server identity
    * Signing certificate

## Installing

```bash
Install-Package DrWatson.Adfs.Metadata
```

## Usage

```csharp
AdfsMetadataService svc = new AdfsMetadataLoader("https://fs.example.com");
// Exception can be thrown
var metadata = await svc.Get();

// Subsequent  calls will return result from cache
metadata = await svc.Get();

// Now we can use metadata as:
string IdP = metadata.Identity;
string stringSigningCert = metadata.SigningCertificateString;

// Or get certificate with the extension method:
X509Certificate2 signingCert = metadata.GetSigningCertificate();

// Start reloading
svc.Invalidate();

// Now we have a new metadata
metadata = await svc.Get();
```

You can get more control over the loading metadata document if you'll use another constructor for this:

```csharp
AdfsMetadataService svc = new AdfsMetadataLoader(() =>
{
    return new HttpClient().GetStringAsync(
        "https://fs.example.com/FederationMetadata/2007-06/FederationMetadata.xml"
    );
});
```

Inside the ASP.Net Core application you can use extension methods to register the loader as a service:

```csharp
services.AddAdfsMetadata("https://fs.example.com");
```

Or:

```csharp
services.AddAdfsMetadata(() =>
{
    return new HttpClient().GetStringAsync(
        "https://fs.example.com/FederationMetadata/2007-06/FederationMetadata.xml"
    );
});
```