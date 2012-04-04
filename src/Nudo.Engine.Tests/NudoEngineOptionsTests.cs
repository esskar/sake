﻿using Shouldly;
using Xunit;

namespace Nudo.Engine.Tests
{
    public class NudoEngineOptionsTests
    {
        private NudoEngine _engine;

        public NudoEngineOptionsTests()
        {
            _engine = new NudoEngine(new NudoSettings());
        }

        [Fact]
        public void VerboseIsRecognized()
        {
            _engine.Parse("-v").Verbose.ShouldBe(1);
            _engine.Parse("--verbose").Verbose.ShouldBe(1);
        }

        [Fact]
        public void MoreThanOneVerboseMayBePassed()
        {
            _engine.Parse("-vv").Verbose.ShouldBe(2);
            _engine.Parse("-v", "-v").Verbose.ShouldBe(2);
        }

        [Fact]
        public void PlainOldArgsBecomeTargets()
        {
            _engine.Parse("this", "is", "a", "test").Targets.ShouldBe(new[]{"this", "is", "a", "test"});
        }

        [Fact]
        public void MakeFileMayBePassedIn()
        {
            var options = _engine.Parse("--file", "hello.txt", "foo");
            options.Targets.ShouldBe(new[]{"foo"});
            options.Makefile.ShouldBe("hello.txt");
        }
    }
}