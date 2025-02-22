// Copyright (C) 2015-2025 The Neo Project.
//
// UT_JArray.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using System.Collections;

namespace Neo.Json.UnitTests
{
    enum Foo
    {
        male,
        female
    }

    [TestClass]
    public class UT_JArray
    {
        private JObject alice;
        private JObject bob;

        [TestInitialize]
        public void SetUp()
        {
            alice = new JObject();
            alice["name"] = "alice";
            alice["age"] = 30;
            alice["score"] = 100.001;
            alice["gender"] = Foo.female;
            alice["isMarried"] = true;
            var pet1 = new JObject();
            pet1["name"] = "Tom";
            pet1["type"] = "cat";
            alice["pet"] = pet1;

            bob = new JObject();
            bob["name"] = "bob";
            bob["age"] = 100000;
            bob["score"] = 0.001;
            bob["gender"] = Foo.male;
            bob["isMarried"] = false;
            var pet2 = new JObject();
            pet2["name"] = "Paul";
            pet2["type"] = "dog";
            bob["pet"] = pet2;
        }

        [TestMethod]
        public void TestAdd()
        {
            var jArray = new JArray
            {
                alice,
                bob
            };
            var jAlice = jArray[0];
            var jBob = jArray[1];
            jAlice["name"].ToString().Should().Be(alice["name"].ToString());
            jAlice["age"].ToString().Should().Be(alice["age"].ToString());
            jAlice["score"].ToString().Should().Be(alice["score"].ToString());
            jAlice["gender"].ToString().Should().Be(alice["gender"].ToString());
            jAlice["isMarried"].ToString().Should().Be(alice["isMarried"].ToString());
            jAlice["pet"].ToString().Should().Be(alice["pet"].ToString());
            jBob["name"].ToString().Should().Be(bob["name"].ToString());
            jBob["age"].ToString().Should().Be(bob["age"].ToString());
            jBob["score"].ToString().Should().Be(bob["score"].ToString());
            jBob["gender"].ToString().Should().Be(bob["gender"].ToString());
            jBob["isMarried"].ToString().Should().Be(bob["isMarried"].ToString());
            jBob["pet"].ToString().Should().Be(bob["pet"].ToString());
        }

        [TestMethod]
        public void TestSetItem()
        {
            var jArray = new JArray
            {
                alice
            };
            jArray[0] = bob;
            Assert.AreEqual(jArray[0], bob);

            Action action = () => jArray[1] = alice;
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void TestClear()
        {
            var jArray = new JArray
            {
                alice
            };
            var jAlice = jArray[0];
            jAlice["name"].ToString().Should().Be(alice["name"].ToString());
            jAlice["age"].ToString().Should().Be(alice["age"].ToString());
            jAlice["score"].ToString().Should().Be(alice["score"].ToString());
            jAlice["gender"].ToString().Should().Be(alice["gender"].ToString());
            jAlice["isMarried"].ToString().Should().Be(alice["isMarried"].ToString());
            jAlice["pet"].ToString().Should().Be(alice["pet"].ToString());

            jArray.Clear();
            Action action = () => jArray[0].ToString();
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void TestContains()
        {
            var jArray = new JArray
            {
                alice
            };
            jArray.Contains(alice).Should().BeTrue();
            jArray.Contains(bob).Should().BeFalse();
        }

        [TestMethod]
        public void TestCopyTo()
        {
            var jArray = new JArray
            {
                alice,
                bob
            };

            JObject[] jObjects1 = new JObject[2];
            jArray.CopyTo(jObjects1, 0);
            var jAlice1 = jObjects1[0];
            var jBob1 = jObjects1[1];
            Assert.AreEqual(alice, jAlice1);
            Assert.AreEqual(bob, jBob1);

            JObject[] jObjects2 = new JObject[4];
            jArray.CopyTo(jObjects2, 2);
            var jAlice2 = jObjects2[2];
            var jBob2 = jObjects2[3];
            jObjects2[0].Should().BeNull();
            jObjects2[1].Should().BeNull();
            Assert.AreEqual(alice, jAlice2);
            Assert.AreEqual(bob, jBob2);
        }

        [TestMethod]
        public void TestInsert()
        {
            var jArray = new JArray
            {
                alice,
                alice,
                alice,
                alice
            };

            jArray.Insert(1, bob);
            jArray.Count().Should().Be(5);
            jArray[0].Should().Be(alice);
            jArray[1].Should().Be(bob);
            jArray[2].Should().Be(alice);

            jArray.Insert(5, bob);
            jArray.Count().Should().Be(6);
            jArray[5].Should().Be(bob);
        }

        [TestMethod]
        public void TestIndexOf()
        {
            var jArray = new JArray();
            jArray.IndexOf(alice).Should().Be(-1);

            jArray.Add(alice);
            jArray.Add(alice);
            jArray.Add(alice);
            jArray.Add(alice);
            jArray.IndexOf(alice).Should().Be(0);

            jArray.Insert(1, bob);
            jArray.IndexOf(bob).Should().Be(1);
        }

        [TestMethod]
        public void TestIsReadOnly()
        {
            var jArray = new JArray();
            jArray.IsReadOnly.Should().BeFalse();
        }

        [TestMethod]
        public void TestRemove()
        {
            var jArray = new JArray
            {
                alice
            };
            jArray.Count().Should().Be(1);
            jArray.Remove(alice);
            jArray.Count().Should().Be(0);

            jArray.Add(alice);
            jArray.Add(alice);
            jArray.Count().Should().Be(2);
            jArray.Remove(alice);
            jArray.Count().Should().Be(1);
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            var jArray = new JArray
            {
                alice,
                bob,
                alice
            };
            jArray.RemoveAt(1);
            jArray.Count().Should().Be(2);
            jArray.Contains(bob).Should().BeFalse();
        }

        [TestMethod]
        public void TestGetEnumerator()
        {
            var jArray = new JArray
            {
                alice,
                bob,
                alice,
                bob
            };
            int i = 0;
            foreach (var item in jArray)
            {
                if (i % 2 == 0) item.Should().Be(alice);
                if (i % 2 != 0) item.Should().Be(bob);
                i++;
            }
            Assert.IsNotNull(((IEnumerable)jArray).GetEnumerator());
        }

        [TestMethod]
        public void TestAsString()
        {
            var jArray = new JArray
            {
                alice,
                bob,
            };
            var s = jArray.AsString();
            Assert.AreEqual(s, "[{\"name\":\"alice\",\"age\":30,\"score\":100.001,\"gender\":\"female\",\"isMarried\":true,\"pet\":{\"name\":\"Tom\",\"type\":\"cat\"}},{\"name\":\"bob\",\"age\":100000,\"score\":0.001,\"gender\":\"male\",\"isMarried\":false,\"pet\":{\"name\":\"Paul\",\"type\":\"dog\"}}]");
        }

        [TestMethod]
        public void TestCount()
        {
            var jArray = new JArray { alice, bob };
            jArray.Count.Should().Be(2);
        }

        [TestMethod]
        public void TestInvalidIndexAccess()
        {
            var jArray = new JArray { alice };
            Action action = () => { var item = jArray[1]; };
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void TestEmptyEnumeration()
        {
            var jArray = new JArray();
            foreach (var item in jArray)
            {
                Assert.Fail("Enumeration should not occur on an empty JArray");
            }
        }

        [TestMethod]
        public void TestImplicitConversionFromJTokenArray()
        {
            JToken[] jTokens = { alice, bob };
            JArray jArray = jTokens;

            jArray.Count.Should().Be(2);
            jArray[0].Should().Be(alice);
            jArray[1].Should().Be(bob);
        }

        [TestMethod]
        public void TestAddNullValues()
        {
            var jArray = new JArray();
            jArray.Add(null);
            jArray.Count.Should().Be(1);
            jArray[0].Should().BeNull();
        }

        [TestMethod]
        public void TestClone()
        {
            var jArray = new JArray { alice, bob };
            var clone = (JArray)jArray.Clone();

            clone.Should().NotBeSameAs(jArray);
            clone.Count.Should().Be(jArray.Count);

            for (int i = 0; i < jArray.Count; i++)
            {
                clone[i]?.AsString().Should().Be(jArray[i]?.AsString());
            }

            var a = jArray.AsString();
            var b = jArray.Clone().AsString();
            a.Should().Be(b);
        }

        [TestMethod]
        public void TestReadOnlyBehavior()
        {
            var jArray = new JArray();
            jArray.IsReadOnly.Should().BeFalse();
        }

        [TestMethod]
        public void TestAddNull()
        {
            var jArray = new JArray { null };

            jArray.Count.Should().Be(1);
            jArray[0].Should().BeNull();
        }

        [TestMethod]
        public void TestSetNull()
        {
            var jArray = new JArray { alice };
            jArray[0] = null;

            jArray.Count.Should().Be(1);
            jArray[0].Should().BeNull();
        }

        [TestMethod]
        public void TestInsertNull()
        {
            var jArray = new JArray { alice };
            jArray.Insert(0, null);

            jArray.Count.Should().Be(2);
            jArray[0].Should().BeNull();
            jArray[1].Should().Be(alice);
        }

        [TestMethod]
        public void TestRemoveNull()
        {
            var jArray = new JArray { null, alice };
            jArray.Remove(null);

            jArray.Count.Should().Be(1);
            jArray[0].Should().Be(alice);
        }

        [TestMethod]
        public void TestContainsNull()
        {
            var jArray = new JArray { null, alice };
            jArray.Contains(null).Should().BeTrue();
            jArray.Contains(bob).Should().BeFalse();
        }

        [TestMethod]
        public void TestIndexOfNull()
        {
            var jArray = new JArray { null, alice };
            jArray.IndexOf(null).Should().Be(0);
            jArray.IndexOf(alice).Should().Be(1);
        }

        [TestMethod]
        public void TestCopyToWithNull()
        {
            var jArray = new JArray { null, alice };
            JObject[] jObjects = new JObject[2];
            jArray.CopyTo(jObjects, 0);

            jObjects[0].Should().BeNull();
            jObjects[1].Should().Be(alice);
        }

        [TestMethod]
        public void TestToStringWithNull()
        {
            var jArray = new JArray { null, alice, bob };
            var jsonString = jArray.ToString();
            var asString = jArray.AsString();
            // JSON string should properly represent the null value
            jsonString.Should().Be("[null,{\"name\":\"alice\",\"age\":30,\"score\":100.001,\"gender\":\"female\",\"isMarried\":true,\"pet\":{\"name\":\"Tom\",\"type\":\"cat\"}},{\"name\":\"bob\",\"age\":100000,\"score\":0.001,\"gender\":\"male\",\"isMarried\":false,\"pet\":{\"name\":\"Paul\",\"type\":\"dog\"}}]");
            asString.Should().Be("[null,{\"name\":\"alice\",\"age\":30,\"score\":100.001,\"gender\":\"female\",\"isMarried\":true,\"pet\":{\"name\":\"Tom\",\"type\":\"cat\"}},{\"name\":\"bob\",\"age\":100000,\"score\":0.001,\"gender\":\"male\",\"isMarried\":false,\"pet\":{\"name\":\"Paul\",\"type\":\"dog\"}}]");
        }

        [TestMethod]
        public void TestFromStringWithNull()
        {
            var jsonString = "[null,{\"name\":\"alice\",\"age\":30,\"score\":100.001,\"gender\":\"female\",\"isMarried\":true,\"pet\":{\"name\":\"Tom\",\"type\":\"cat\"}},{\"name\":\"bob\",\"age\":100000,\"score\":0.001,\"gender\":\"male\",\"isMarried\":false,\"pet\":{\"name\":\"Paul\",\"type\":\"dog\"}}]";
            var jArray = (JArray)JArray.Parse(jsonString);

            jArray.Count.Should().Be(3);
            jArray[0].Should().BeNull();

            // Checking the second and third elements
            jArray[1]["name"].AsString().Should().Be("alice");
            jArray[2]["name"].AsString().Should().Be("bob");
        }
    }
}
