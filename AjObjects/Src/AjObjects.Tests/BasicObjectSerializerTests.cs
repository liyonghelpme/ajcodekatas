using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AjObjects.Tests
{
    [TestClass]
    public class BasicObjectSerializerTests
    {
        [TestMethod]
        public void SerializeAndDeserializeSimpleBasicObject()
        {
            BasicObject obj = BasicObject.CreateObject("Name", "Adam", "Age", 800);

            BasicObjectSerializer serializer = new BasicObjectSerializer();
            Stream stream = new MemoryStream();
            serializer.Serialize(obj, stream);
            stream.Seek(0, SeekOrigin.Begin);

            BasicObject clone = serializer.Deserialize(stream);

            Assert.IsNotNull(clone);
            Assert.AreEqual(obj, clone);
        }

        [TestMethod]
        public void SerializeAndDeserializeCompositeObjectObject()
        {
            BasicObject adam = BasicObject.CreateObject("Name", "Adam", "Age", 800);
            BasicObject eve = BasicObject.CreateObject("Name", "Eve", "Age", 700);
            BasicObject abel = BasicObject.CreateObject("Name", "Abel", "Age", 600, "Father", adam, "Mother", eve);

            BasicObjectSerializer serializer = new BasicObjectSerializer();
            Stream stream = new MemoryStream();
            serializer.Serialize(abel, stream);
            stream.Seek(0, SeekOrigin.Begin);

            BasicObject clone = serializer.Deserialize(stream);

            Assert.IsNotNull(clone);
            Assert.AreEqual(abel, clone);
        }

        [TestMethod]
        public void SerializeAndDeserializeBasicObjectWithId()
        {
            BasicObject obj = new BasicObject();
            obj.Id = Guid.NewGuid();
            obj["Name"] = "Adam";
            obj["Age"] = 800;

            BasicObjectSerializer serializer = new BasicObjectSerializer();
            Stream stream = new MemoryStream();
            serializer.Serialize(obj, stream);
            stream.Seek(0, SeekOrigin.Begin);

            BasicObject clone = serializer.Deserialize(stream);

            Assert.IsNotNull(clone);
            Assert.AreEqual(obj, clone);
        }

        [TestMethod]
        public void SerializeAndDeserializeBasicObjectWithDateTime()
        {
            BasicObject obj = new BasicObject();
            obj["Name"] = "Adam";
            obj["Born"] = new DateTime(1, 1, 1);
            obj["Age"] = 800;

            BasicObjectSerializer serializer = new BasicObjectSerializer();
            Stream stream = new MemoryStream();
            serializer.Serialize(obj, stream);
            stream.Seek(0, SeekOrigin.Begin);

            BasicObject clone = serializer.Deserialize(stream);

            Assert.IsNotNull(clone);
            Assert.AreEqual(obj, clone);
        }
    }
}
