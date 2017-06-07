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
        private string GroomedValidMetadataPath { get => @"D:\GitHub\adfs-metadata\src\DrWatson.Adfs.Metadata.Test\TestData\GroomedValidFederationMetadata.xml"; }
        private string GroomedValidMetadata { get => File.ReadAllText(GroomedValidMetadataPath); }

        [TestMethod]
        public void ParserShouldNotThrowOnValidMetadata()
        {
            Action act = () => new Parser(GroomedValidMetadata);

            act.ShouldNotThrow();
        }
    }
}