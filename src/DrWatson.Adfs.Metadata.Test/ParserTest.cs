﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace DrWatson.Adfs.Metadata.Test
{
    [TestClass]
    public class ParserTest
    {
        private string BasePath { get => GetType().GetTypeInfo().Assembly.Location + "\\.."; }
        private string MetadataPath { get => BasePath + @"\TestData\FederationMetadata.xml"; }
        private string Metadata { get => File.ReadAllText(MetadataPath); }

        [TestMethod]
        public void ParserShouldNotThrowOnValidMetadata()
        {
            Action act = () => new AdfsMetadataParser(Metadata);

            act.ShouldNotThrow();
        }

        [TestMethod]
        public void IdentityShouldHaveValidValue()
        {
            var p = new AdfsMetadataParser(Metadata);

            p.Identity.Should().Be("http://fs.geocyber.ru/adfs/services/trust");
        }

        [TestMethod]
        public void SigningCertificateStringShouldStartsWithValidValue()
        {
            var p = new AdfsMetadataParser(Metadata);

            p.SigningCertificateString.Should().StartWith("MIIC2DCCAcCgAwIBAgIQFDIYq8YLU5BCIiG0yk1");
        }

        [TestMethod]
        public void SigningCertificateShouldNotBeNull()
        {
            var p = new AdfsMetadataParser(Metadata);

            p.SigningCertificate.Should().NotBeNull();
        }
    }
}