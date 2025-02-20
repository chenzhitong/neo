// Copyright (C) 2015-2025 The Neo Project.
//
// UT_Header.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.Extensions;
using Neo.IO;
using Neo.Network.P2P.Payloads;
using Neo.SmartContract.Native;
using Neo.UnitTests.SmartContract;
using System;

namespace Neo.UnitTests.Network.P2P.Payloads
{
    [TestClass]
    public class UT_Header
    {
        Header uut;

        [TestInitialize]
        public void TestSetup()
        {
            uut = new Header();
        }

        [TestMethod]
        public void Size_Get()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupHeaderWithValues(null, uut, val256, out _, out _, out _, out _, out _, out _);
            // blockbase 4 + 64 + 1 + 32 + 4 + 4 + 20 + 4
            // header 1
            uut.Size.Should().Be(113); // 105 + nonce
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupHeaderWithValues(null, uut, val256, out _, out _, out _, out _, out _, out _);
            uut.GetHashCode().Should().Be(uut.Hash.GetHashCode());
        }

        [TestMethod]
        public void TrimTest()
        {
            UInt256 val256 = UInt256.Zero;
            var snapshotCache = TestBlockchain.GetTestSnapshotCache().CreateSnapshot();
            TestUtils.SetupHeaderWithValues(null, uut, val256, out _, out _, out _, out _, out _, out _);
            uut.Witness = new Witness() { InvocationScript = Array.Empty<byte>(), VerificationScript = Array.Empty<byte>() };

            TestUtils.BlocksAdd(snapshotCache, uut.Hash, new TrimmedBlock()
            {
                Header = new Header
                {
                    Timestamp = uut.Timestamp,
                    PrevHash = uut.PrevHash,
                    MerkleRoot = uut.MerkleRoot,
                    NextConsensus = uut.NextConsensus,
                    Witness = uut.Witness
                },
                Hashes = Array.Empty<UInt256>()
            });

            var trim = NativeContract.Ledger.GetTrimmedBlock(snapshotCache, uut.Hash);
            var header = trim.Header;

            header.Version.Should().Be(uut.Version);
            header.PrevHash.Should().Be(uut.PrevHash);
            header.MerkleRoot.Should().Be(uut.MerkleRoot);
            header.Timestamp.Should().Be(uut.Timestamp);
            header.Index.Should().Be(uut.Index);
            header.NextConsensus.Should().Be(uut.NextConsensus);
            header.Witness.InvocationScript.Span.SequenceEqual(uut.Witness.InvocationScript.Span).Should().BeTrue();
            header.Witness.VerificationScript.Span.SequenceEqual(uut.Witness.VerificationScript.Span).Should().BeTrue();
            trim.Hashes.Length.Should().Be(0);
        }

        [TestMethod]
        public void Deserialize()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupHeaderWithValues(null, new Header(), val256, out UInt256 merkRoot, out UInt160 val160, out ulong timestampVal, out ulong nonceVal, out uint indexVal, out Witness scriptVal);

            uut.MerkleRoot = merkRoot; // need to set for deserialise to be valid

            var hex = "0000000000000000000000000000000000000000000000000000000000000000000000007227ba7b747f1a98f68679d4a98b68927646ab195a6f56b542ca5a0e6a412662493ed0e58f01000000000000000000000000000000000000000000000000000000000000000000000001000111";

            MemoryReader reader = new(hex.HexToBytes());
            uut.Deserialize(ref reader);

            AssertStandardHeaderTestVals(val256, merkRoot, val160, timestampVal, nonceVal, indexVal, scriptVal);
        }

        private void AssertStandardHeaderTestVals(UInt256 val256, UInt256 merkRoot, UInt160 val160, ulong timestampVal, ulong nonceVal, uint indexVal, Witness scriptVal)
        {
            uut.PrevHash.Should().Be(val256);
            uut.MerkleRoot.Should().Be(merkRoot);
            uut.Timestamp.Should().Be(timestampVal);
            uut.Index.Should().Be(indexVal);
            uut.Nonce.Should().Be(nonceVal);
            uut.NextConsensus.Should().Be(val160);
            uut.Witness.InvocationScript.Length.Should().Be(0);
            uut.Witness.Size.Should().Be(scriptVal.Size);
            uut.Witness.VerificationScript.Span[0].Should().Be(scriptVal.VerificationScript.Span[0]);
        }

        [TestMethod]
        public void Equals_Null()
        {
            uut.Equals(null).Should().BeFalse();
        }


        [TestMethod]
        public void Equals_SameHeader()
        {
            uut.Equals(uut).Should().BeTrue();
        }

        [TestMethod]
        public void Equals_SameHash()
        {
            Header newHeader = new();
            UInt256 prevHash = new(TestUtils.GetByteArray(32, 0x42));
            TestUtils.SetupHeaderWithValues(null, newHeader, prevHash, out _, out _, out _, out _, out _, out _);
            TestUtils.SetupHeaderWithValues(null, uut, prevHash, out _, out _, out _, out _, out _, out _);

            uut.Equals(newHeader).Should().BeTrue();
        }

        [TestMethod]
        public void Equals_SameObject()
        {
            uut.Equals((object)uut).Should().BeTrue();
        }

        [TestMethod]
        public void Serialize()
        {
            UInt256 val256 = UInt256.Zero;
            TestUtils.SetupHeaderWithValues(null, uut, val256, out _, out _, out _, out _, out _, out _);

            var hex = "0000000000000000000000000000000000000000000000000000000000000000000000007227ba7b747f1a98f68679d4a98b68927646ab195a6f56b542ca5a0e6a412662493ed0e58f01000000000000000000000000000000000000000000000000000000000000000000000001000111";
            uut.ToArray().ToHexString().Should().Be(hex);
        }
    }
}
