// Copyright (C) 2015-2025 The Neo Project.
//
// UT_Murmur32.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.Cryptography;
using System.Buffers.Binary;

namespace Neo.UnitTests.Cryptography
{
    [TestClass]
    public class UT_Murmur32
    {
        [TestMethod]
        public void TestGetHashSize()
        {
            Murmur32 murmur3 = new Murmur32(1);
            murmur3.HashSize.Should().Be(32);
        }

        [TestMethod]
        public void TestHashCore()
        {
            byte[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 };
            array.Murmur32(10u).Should().Be(378574820u);
        }

        [TestMethod]
        public void TestTryComputeHash()
        {
            var murmur3 = new Murmur32(10u);
            var buffer = new byte[murmur3.HashSize / 8];
            var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 };

            var ok = murmur3.TryComputeHash(data, buffer, out _);
            ok.Should().BeTrue();

            var hash = BinaryPrimitives.ReadUInt32LittleEndian(buffer);
            hash.Should().Be(378574820u);
        }
    }
}
