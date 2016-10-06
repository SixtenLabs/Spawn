﻿using Xunit;
using FluentAssertions;
using NSubstitute;

namespace SixtenLabs.Spawn.Tests
{
    public class SpawnSpecTests
    {
        private TestSpec SubjectUnderTest()
        {
            MockWebClientFactory = Substitute.For<WebClientFactory>();
            MockGeneratorSettings = Substitute.For<IGeneratorSettings>();
            MockFileLoader = Substitute.For<XmlFileLoader>(MockGeneratorSettings, MockWebClientFactory);
            MockSpecMapper = Substitute.For<ISpecMapper>();

            return new TestSpec(MockFileLoader, MockSpecMapper);
        }

        [Fact]
        public void ProcessRegistry_FileLoader_LoadRegistryCalledOnce()
        {
            var subject = SubjectUnderTest();

            subject.ProcessRegistry();

            MockFileLoader.Received(1).LoadRegistry();
        }

        [Fact]
        public void SpecTree_ShouldBe_Empty()
        {
            var subject = SubjectUnderTest();

            var actual = subject.SpecTree;

            actual.Should().BeNull();
        }

        //[Fact]
        //public void CreateSpecTree_ShouldBe_Empty()
        //{
        //  var subject = SubjectUnderTest();

        //  var actual = subject.CreateSpecTree(;

        //  actual.Should().BeNull();
        //}

        private IGeneratorSettings MockGeneratorSettings { get; set; }

        private XmlFileLoader MockFileLoader { get; set; }

        private IWebClientFactory MockWebClientFactory { get; set; }

        private ISpecMapper MockSpecMapper { get; set; }
    }
}
