using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.IO;
using Neo.IO.Json;
using Neo.Network.P2P.Payloads;
using System.IO;

namespace Neo.UnitTests.Network.P2P.Payloads
{
    [TestClass]
    public class UT_Block
    {
        Block uut;

        [TestInitialize]
        public void TestSetup()
        {
            uut = new Block();
        }

        [TestMethod]
        public void Transactions_Get()
        {
            uut.Transactions.Should().BeNull();
        }

        [TestMethod]
        public void Header_Get()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupBlockWithValues(uut, val256, out var merkRootVal, out var val160, out var timestampVal, out var indexVal, out var scriptVal, out var transactionsVal, 0);

            uut.Header.Should().NotBeNull();
            uut.Header.PrevHash.Should().Be(val256);
            uut.Header.MerkleRoot.Should().Be(merkRootVal);
            uut.Header.Timestamp.Should().Be(timestampVal);
            uut.Header.Index.Should().Be(indexVal);
            uut.Header.Witness.Should().Be(scriptVal);
        }

        [TestMethod]
        public void Size_Get()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupBlockWithValues(uut, val256, out var _, out var _, out var _, out var _, out var _, out var _, 0);
            // header 4 + 32 + 32 + 8 + 4 + 1 + 20 + 4
            // tx 1
            uut.Size.Should().Be(106);
        }

        [TestMethod]
        public void Size_Get_1_Transaction()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupBlockWithValues(uut, val256, out var _, out var _, out var _, out var _, out var _, out var _, 0);

            uut.Transactions = new[]
            {
                TestUtils.GetTransaction(UInt160.Zero)
            };

            uut.Size.Should().Be(159);
        }

        [TestMethod]
        public void Size_Get_3_Transaction()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupBlockWithValues(uut, val256, out var _, out var _, out var _, out var _, out var _, out var _, 0);

            uut.Transactions = new[]
            {
                TestUtils.GetTransaction(UInt160.Zero),
                TestUtils.GetTransaction(UInt160.Zero),
                TestUtils.GetTransaction(UInt160.Zero)
            };

            uut.Size.Should().Be(265);
        }

        [TestMethod]
        public void Serialize()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupBlockWithValues(uut, val256, out var _, out var _, out var _, out var _, out var _, out var _, 1);

            var hex = "0000000000000000000000000000000000000000000000000000000000000000000000003c92d2d37b91caa73ab056d0676d9bd78752a5e2e79e3ae27936e9b3a330e80ee913ff854c0000000000000000000000000000000000000000000000000000000001000111010000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000001000112010000";
            uut.ToArray().ToHexString().Should().Be(hex);
        }

        [TestMethod]
        public void Deserialize()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupBlockWithValues(new Block(), val256, out var merkRoot, out var val160, out var timestampVal, out var indexVal, out var scriptVal, out var transactionsVal, 1);

            var hex = "0000000000000000000000000000000000000000000000000000000000000000000000003c92d2d37b91caa73ab056d0676d9bd78752a5e2e79e3ae27936e9b3a330e80ee913ff854c0000000000000000000000000000000000000000000000000000000001000111010000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000001000112010000";

            using (MemoryStream ms = new MemoryStream(hex.HexToBytes(), false))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                uut.Deserialize(reader);
                merkRoot = uut.MerkleRoot;
            }

            assertStandardBlockTestVals(val256, merkRoot, val160, timestampVal, indexVal, scriptVal, transactionsVal);
        }

        private void assertStandardBlockTestVals(UInt256 val256, UInt256 merkRoot, UInt160 val160, ulong timestampVal, uint indexVal, Witness scriptVal, Transaction[] transactionsVal, bool testTransactions = true)
        {
            uut.PrevHash.Should().Be(val256);
            uut.MerkleRoot.Should().Be(merkRoot);
            uut.Timestamp.Should().Be(timestampVal);
            uut.Index.Should().Be(indexVal);
            uut.NextConsensus.Should().Be(val160);
            uut.Witness.InvocationScript.Length.Should().Be(0);
            uut.Witness.Size.Should().Be(scriptVal.Size);
            uut.Witness.VerificationScript[0].Should().Be(scriptVal.VerificationScript[0]);
            if (testTransactions)
            {
                uut.Transactions.Length.Should().Be(1);
                uut.Transactions[0].Should().Be(transactionsVal[0]);
            }
        }

        [TestMethod]
        public void Equals_SameObj()
        {
            uut.Equals(uut).Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DiffObj()
        {
            Block newBlock = new Block();
            UInt256 val256 = UInt256.Zero;
            UInt256 prevHash = new UInt256(TestUtils.GetByteArray(32, 0x42));
            TestUtils.SetupBlockWithValues(newBlock, val256, out UInt256 merkRoot, out UInt160 val160, out ulong timestampVal, out uint indexVal, out Witness scriptVal, out Transaction[] transactionsVal, 1);
            TestUtils.SetupBlockWithValues(uut, prevHash, out merkRoot, out val160, out timestampVal, out indexVal, out scriptVal, out transactionsVal, 0);

            uut.Equals(newBlock).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_Null()
        {
            uut.Equals(null).Should().BeFalse();
        }

        [TestMethod]
        public void Equals_SameHash()
        {
            Block newBlock = new Block();
            UInt256 prevHash = new UInt256(TestUtils.GetByteArray(32, 0x42));
            TestUtils.SetupBlockWithValues(newBlock, prevHash, out UInt256 merkRoot, out UInt160 val160, out ulong timestampVal, out uint indexVal, out Witness scriptVal, out Transaction[] transactionsVal, 1);
            TestUtils.SetupBlockWithValues(uut, prevHash, out merkRoot, out val160, out timestampVal, out indexVal, out scriptVal, out transactionsVal, 1);

            uut.Equals(newBlock).Should().BeTrue();
        }

        [TestMethod]
        public void ToJson()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupBlockWithValues(uut, val256, out var merkRoot, out var val160, out var timestampVal, out var indexVal, out var scriptVal, out var transactionsVal, 1);

            JObject jObj = uut.ToJson();
            jObj.Should().NotBeNull();
            jObj["hash"].AsString().Should().Be("0x0ffaf8fc7eeb34959e7220d316e3294290e84b6a183b4bdb5c7c14a8f80239ee");
            jObj["size"].AsNumber().Should().Be(159);
            jObj["version"].AsNumber().Should().Be(0);
            jObj["previousblockhash"].AsString().Should().Be("0x0000000000000000000000000000000000000000000000000000000000000000");
            jObj["merkleroot"].AsString().Should().Be("0x0ee830a3b3e93679e23a9ee7e2a55287d79b6d67d056b03aa7ca917bd3d2923c");
            jObj["time"].AsNumber().Should().Be(328665601001);
            jObj["index"].AsNumber().Should().Be(0);
            jObj["nextconsensus"].AsString().Should().Be("NKuyBkoGdZZSLyPbJEetheRhMjeznFZszf");

            JObject scObj = ((JArray)jObj["witnesses"])[0];
            scObj["invocation"].AsString().Should().Be("");
            scObj["verification"].AsString().Should().Be("EQ==");

            jObj["tx"].Should().NotBeNull();
            JArray txObj = (JArray)jObj["tx"];
            txObj[0]["hash"].AsString().Should().Be("0x0ee830a3b3e93679e23a9ee7e2a55287d79b6d67d056b03aa7ca917bd3d2923c");
            txObj[0]["size"].AsNumber().Should().Be(53);
            txObj[0]["version"].AsNumber().Should().Be(0);
            ((JArray)txObj[0]["attributes"]).Count.Should().Be(0);
            txObj[0]["netfee"].AsString().Should().Be("0");
        }
    }
}
