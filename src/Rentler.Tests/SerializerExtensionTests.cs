using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentler;
using System.IO;
using System.Diagnostics;
using ProtoBuf;

namespace Rentler.Tests
{
    [TestClass]
    public class SerializerExtensionsTests
    {
        [TestMethod]
        public void SerializeString()
        {
            string bleh = "hi2u";
            var result = bleh.ToBinaryArray<string>();

            // check the result
            using (var stream = new MemoryStream(result))
            {
                var final = Serializer.Deserialize<string>(stream);
                Assert.AreEqual(bleh, final);
            }
        }

        [TestMethod]
        public void DeserializeString()
        {
            string bleh = "hi2u";
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<string>(ms, bleh);
                var result = ms.ToArray();
                var final = SerializerExtensions.Deserialize<string>(result);
                Assert.AreEqual(final, bleh);
            }
        }

        [TestMethod]
        public void SerializeNull()
        {
            string item = null;
            var result = item.ToBinaryArray();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeSerializeNull()
        {
            var final = SerializerExtensions.Deserialize<string>(null);
            Assert.IsNull(final);
        }

        [TestMethod]
        public void SerializeAndDeserializeList()
        {
            List<string> strings = new List<string>();
            strings.Add("test");
            strings.Add("test2");
            strings.Add("test3");

            var serialized = strings.ToBinaryArray();
            var deserialized = SerializerExtensions.Deserialize<List<string>>(serialized);
            Assert.AreEqual(deserialized[0], "test");
            Assert.AreEqual(deserialized[1], "test2");
            Assert.AreEqual(deserialized[2], "test3");
        }

        [TestMethod]
        public void SerializeAndDeserializeComplexObject()
        {
            var test = new TestObject()
            {
                MyInt = 10,
                MyString = "Test"
            };

            var serialized = test.ToBinaryArray();
            var deserialized = SerializerExtensions.Deserialize<TestObject>(serialized);

            Assert.AreEqual(deserialized.MyInt, test.MyInt);
            Assert.AreEqual(deserialized.MyString, test.MyString);
        }

        public void SerializeAndDeserializeListComplexObject()
        {
            List<TestObject> test = new List<TestObject>();
            for (int x = 0; x < 100; ++x)
            {
                test.Add(new TestObject()
                {
                    MyString = "string" + x.ToString(),
                    MyInt = x
                });
            }

            var serialized = test.ToBinaryArray();
            var deserialized = SerializerExtensions.Deserialize<List<TestObject>>(serialized);

            Assert.AreEqual(test.Count, 100);
            Assert.AreEqual(test[10].MyString, "string10");
            Assert.AreEqual(test[50], 50);
        }
    }

    [ProtoContract]
    public class TestObject
    {
        [ProtoMember(1)]
        public int MyInt { get; set; }

        [ProtoMember(2)]
        public string MyString { get; set; }
    }
}
