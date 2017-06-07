using FluentAssertions;
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
        private string GroomedValidMetadataPath { get => BasePath + @"\TestData\GroomedValidFederationMetadata.xml"; }
        private string GroomedValidMetadata { get => File.ReadAllText(GroomedValidMetadataPath); }

        [TestMethod]
        public void ParserShouldNotThrowOnValidMetadata()
        {
            Action act = () => new Parser(GroomedValidMetadata);

            act.ShouldNotThrow();
        }

        [TestMethod]
        public void IdentityShouldHaveValidValue()
        {
            var p = new Parser(GroomedValidMetadata);

            p.Identity.Should().Be("http://fs.geocyber.ru/adfs/services/trust");
        }
    }
}