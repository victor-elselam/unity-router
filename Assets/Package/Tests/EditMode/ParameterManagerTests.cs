using Elselam.UnityRouter.Extensions;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class ParameterManagerTests : ZenjectUnitTestFixture
    {
        private IParameterManager parameterManager;

        [SetUp]
        public void Binding()
        {
            Container.Bind<IParameterManager>()
                .To<ParameterManager>()
                .AsSingle();

            Container.Inject(this);
        }

        [Inject]
        public void Construct(IParameterManager parameterManager)
        {
            this.parameterManager = parameterManager;
        }

        [Test]
        public void Create_StringParameter_ReturnSameValueInParameter()
        {
            var param = parameterManager.Create("key", "value");

            param.Key.Should().Be("key");
            param.Value.Should().Be("value");
        }

        [Test]
        public void Create_IntParameter_ReturnSerializedValueInParameter()
        {
            var param = parameterManager.Create("key", 2);

            param.Key.Should().Be("key");
            param.Value.Should().Be("2");
        }

        [Test]
        public void Create_ObjectParameter_ReturnSerializedValueInParameter()
        {
            var obj = new { Test = "test", Count = 2, Valid = true };
            var param = parameterManager.Create("key", obj);

            param.Key.Should().Be("key");
            param.Value.Should().Be("{\"Test\":\"test\",\"Count\":2,\"Valid\":true}");
        }

        [Test]
        public void CreateDictionary_ReturnDictionaryWithSpecifiedParameters()
        {
            var obj = parameterManager.Create("obj", new { Test = "test", Count = 2, Valid = true });
            var str = parameterManager.Create("str", "value");
            var count = parameterManager.Create("count", 2);

            var parameters = parameterManager.CreateDictionary(obj, str, count);

            parameters.Count.Should().Be(3);
            parameters["obj"].Should().Be("{\"Test\":\"test\",\"Count\":2,\"Valid\":true}");
            parameters["str"].Should().Be("value");
            parameters["count"].Should().Be("2");
        }

        [Test]
        public void GetParameterOfType_Valid_ReturnDeserializedParameter()
        {
            var obj = parameterManager.Create("obj", new TestClass("test", 2, true));
            var str = parameterManager.Create("str", "value");
            var count = parameterManager.Create("count", 2);
            var parameters = parameterManager.CreateDictionary(obj, str, count);

            var result = parameterManager.GetParamOfType<TestClass>(parameters, "obj");

            result.Test.Should().Be("test");
            result.Count.Should().Be(2);
            result.Valid.Should().Be(true);
        }

        [Test]
        public void GetParameterOfType_ObjectInvalidWithoutDefault_ReturnNull()
        {
            var str = parameterManager.Create("str", "value");
            var count = parameterManager.Create("count", 2);
            var parameters = parameterManager.CreateDictionary(str, count);

            var result = parameterManager.GetParamOfType<TestClass>(parameters, "obj");

            result.Should().BeNull();
        }

        [Test]
        public void GetParameterOfType_ObjectInvalidWithDefault_ReturnDefault()
        {
            var str = parameterManager.Create("str", "value");
            var count = parameterManager.Create("count", 2);
            var parameters = parameterManager.CreateDictionary(str, count);
            var defaultValue = new TestClass("str", 1, true);

            var result = parameterManager.GetParamOfType<TestClass>(parameters, "obj", defaultValue);

            result.Test.Should().Be("str");
            result.Count.Should().Be(1);
            result.Valid.Should().Be(true);
        }

        [Test]
        public void GetParameterOfType_IntInvalidWithoutDefault_Return0()
        {
            var str = parameterManager.Create("str", "value");
            var parameters = parameterManager.CreateDictionary(str);

            var result = parameterManager.GetParamOfType<int>(parameters, "count");

            result.Should().Be(0);
        }

        [Test]
        public void GetParameterOfType_IntInvalidWithDefault_ReturnDefault()
        {
            var str = parameterManager.Create("str", "value");
            var parameters = parameterManager.CreateDictionary(str);
            var defaultValue = 3;

            var result = parameterManager.GetParamOfType<int>(parameters, "count", defaultValue);

            result.Should().Be(3);
        }

        [Test]
        public void GetParameterOfType_NullParameters_ReturnDefault()
        {
            var defaultValue = 3;

            var result = parameterManager.GetParamOfType<int>(null, "count", defaultValue);

            result.Should().Be(3);
        }

        [Test]
        public void GetParameterOfType_EmptyParameters_ReturnDefault()
        {
            var defaultValue = 3;

            var result = parameterManager.GetParamOfType<int>(new Dictionary<string, string>(), "count", defaultValue);

            result.Should().Be(3);
        }

        private class TestClass
        {
            public string Test = "test";
            public int Count = 2;
            public bool Valid = true;

            public TestClass(string test, int count, bool valid)
            {
                Test = test;
                Count = count;
                Valid = valid;
            }
        }
    }
}