// Copyright (C) 2015-2024 The Neo Project.
//
// Benchmark.Opcode.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using System.Numerics;

namespace Neo.VM.Benchmark.OpCode
{
    public class Benchmark_Opcode
    {
        internal static readonly long OneGasDatoshi = 1_0000_0000;

        internal static readonly BigInteger MAX_INT = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819967");
        internal static readonly BigInteger MIN_INT = BigInteger.Parse("-57896044618658097711785492504343953926634992332820282019728792003956564819968");

        public static readonly IReadOnlyDictionary<VM.OpCode, long> OpCodePrices = new Dictionary<VM.OpCode, long>
        {
            [VM.OpCode.PUSHINT8] = 1 << 0,
            [VM.OpCode.PUSHINT16] = 1 << 0,
            [VM.OpCode.PUSHINT32] = 1 << 0,
            [VM.OpCode.PUSHINT64] = 1 << 0,
            [VM.OpCode.PUSHINT128] = 1 << 2,
            [VM.OpCode.PUSHINT256] = 1 << 2,
            [VM.OpCode.PUSHT] = 1 << 0,
            [VM.OpCode.PUSHF] = 1 << 0,
            [VM.OpCode.PUSHA] = 1 << 2,
            [VM.OpCode.PUSHNULL] = 1 << 0,
            [VM.OpCode.PUSHDATA1] = 1 << 3,
            [VM.OpCode.PUSHDATA2] = 1 << 9,
            [VM.OpCode.PUSHDATA4] = 1 << 12,
            [VM.OpCode.PUSHM1] = 1 << 0,
            [VM.OpCode.PUSH0] = 1 << 0,
            [VM.OpCode.PUSH1] = 1 << 0,
            [VM.OpCode.PUSH2] = 1 << 0,
            [VM.OpCode.PUSH3] = 1 << 0,
            [VM.OpCode.PUSH4] = 1 << 0,
            [VM.OpCode.PUSH5] = 1 << 0,
            [VM.OpCode.PUSH6] = 1 << 0,
            [VM.OpCode.PUSH7] = 1 << 0,
            [VM.OpCode.PUSH8] = 1 << 0,
            [VM.OpCode.PUSH9] = 1 << 0,
            [VM.OpCode.PUSH10] = 1 << 0,
            [VM.OpCode.PUSH11] = 1 << 0,
            [VM.OpCode.PUSH12] = 1 << 0,
            [VM.OpCode.PUSH13] = 1 << 0,
            [VM.OpCode.PUSH14] = 1 << 0,
            [VM.OpCode.PUSH15] = 1 << 0,
            [VM.OpCode.PUSH16] = 1 << 0,
            [VM.OpCode.NOP] = 1 << 0,
            [VM.OpCode.JMP] = 1 << 1,
            [VM.OpCode.JMP_L] = 1 << 1,
            [VM.OpCode.JMPIF] = 1 << 1,
            [VM.OpCode.JMPIF_L] = 1 << 1,
            [VM.OpCode.JMPIFNOT] = 1 << 1,
            [VM.OpCode.JMPIFNOT_L] = 1 << 1,
            [VM.OpCode.JMPEQ] = 1 << 1,
            [VM.OpCode.JMPEQ_L] = 1 << 1,
            [VM.OpCode.JMPNE] = 1 << 1,
            [VM.OpCode.JMPNE_L] = 1 << 1,
            [VM.OpCode.JMPGT] = 1 << 1,
            [VM.OpCode.JMPGT_L] = 1 << 1,
            [VM.OpCode.JMPGE] = 1 << 1,
            [VM.OpCode.JMPGE_L] = 1 << 1,
            [VM.OpCode.JMPLT] = 1 << 1,
            [VM.OpCode.JMPLT_L] = 1 << 1,
            [VM.OpCode.JMPLE] = 1 << 1,
            [VM.OpCode.JMPLE_L] = 1 << 1,
            [VM.OpCode.CALL] = 1 << 9,
            [VM.OpCode.CALL_L] = 1 << 9,
            [VM.OpCode.CALLA] = 1 << 9,
            [VM.OpCode.CALLT] = 1 << 15,
            [VM.OpCode.ABORT] = 0,
            [VM.OpCode.ABORTMSG] = 0,
            [VM.OpCode.ASSERT] = 1 << 0,
            [VM.OpCode.ASSERTMSG] = 1 << 0,
            [VM.OpCode.THROW] = 1 << 9,
            [VM.OpCode.TRY] = 1 << 2,
            [VM.OpCode.TRY_L] = 1 << 2,
            [VM.OpCode.ENDTRY] = 1 << 2,
            [VM.OpCode.ENDTRY_L] = 1 << 2,
            [VM.OpCode.ENDFINALLY] = 1 << 2,
            [VM.OpCode.RET] = 0,
            [VM.OpCode.SYSCALL] = 0,
            [VM.OpCode.DEPTH] = 1 << 1,
            [VM.OpCode.DROP] = 1 << 1,
            [VM.OpCode.NIP] = 1 << 1,
            [VM.OpCode.XDROP] = 1 << 4,
            [VM.OpCode.CLEAR] = 1 << 4,
            [VM.OpCode.DUP] = 1 << 1,
            [VM.OpCode.OVER] = 1 << 1,
            [VM.OpCode.PICK] = 1 << 1,
            [VM.OpCode.TUCK] = 1 << 1,
            [VM.OpCode.SWAP] = 1 << 1,
            [VM.OpCode.ROT] = 1 << 1,
            [VM.OpCode.ROLL] = 1 << 4,
            [VM.OpCode.REVERSE3] = 1 << 1,
            [VM.OpCode.REVERSE4] = 1 << 1,
            [VM.OpCode.REVERSEN] = 1 << 2,
            [VM.OpCode.INITSSLOT] = 1 << 4,
            [VM.OpCode.INITSLOT] = 1 << 6,
            [VM.OpCode.LDSFLD0] = 1 << 1,
            [VM.OpCode.LDSFLD1] = 1 << 1,
            [VM.OpCode.LDSFLD2] = 1 << 1,
            [VM.OpCode.LDSFLD3] = 1 << 1,
            [VM.OpCode.LDSFLD4] = 1 << 1,
            [VM.OpCode.LDSFLD5] = 1 << 1,
            [VM.OpCode.LDSFLD6] = 1 << 1,
            [VM.OpCode.LDSFLD] = 1 << 1,
            [VM.OpCode.STSFLD0] = 1 << 1,
            [VM.OpCode.STSFLD1] = 1 << 1,
            [VM.OpCode.STSFLD2] = 1 << 1,
            [VM.OpCode.STSFLD3] = 1 << 1,
            [VM.OpCode.STSFLD4] = 1 << 1,
            [VM.OpCode.STSFLD5] = 1 << 1,
            [VM.OpCode.STSFLD6] = 1 << 1,
            [VM.OpCode.STSFLD] = 1 << 1,
            [VM.OpCode.LDLOC0] = 1 << 1,
            [VM.OpCode.LDLOC1] = 1 << 1,
            [VM.OpCode.LDLOC2] = 1 << 1,
            [VM.OpCode.LDLOC3] = 1 << 1,
            [VM.OpCode.LDLOC4] = 1 << 1,
            [VM.OpCode.LDLOC5] = 1 << 1,
            [VM.OpCode.LDLOC6] = 1 << 1,
            [VM.OpCode.LDLOC] = 1 << 1,
            [VM.OpCode.STLOC0] = 1 << 1,
            [VM.OpCode.STLOC1] = 1 << 1,
            [VM.OpCode.STLOC2] = 1 << 1,
            [VM.OpCode.STLOC3] = 1 << 1,
            [VM.OpCode.STLOC4] = 1 << 1,
            [VM.OpCode.STLOC5] = 1 << 1,
            [VM.OpCode.STLOC6] = 1 << 1,
            [VM.OpCode.STLOC] = 1 << 1,
            [VM.OpCode.LDARG0] = 1 << 1,
            [VM.OpCode.LDARG1] = 1 << 1,
            [VM.OpCode.LDARG2] = 1 << 1,
            [VM.OpCode.LDARG3] = 1 << 1,
            [VM.OpCode.LDARG4] = 1 << 1,
            [VM.OpCode.LDARG5] = 1 << 1,
            [VM.OpCode.LDARG6] = 1 << 1,
            [VM.OpCode.LDARG] = 1 << 1,
            [VM.OpCode.STARG0] = 1 << 1,
            [VM.OpCode.STARG1] = 1 << 1,
            [VM.OpCode.STARG2] = 1 << 1,
            [VM.OpCode.STARG3] = 1 << 1,
            [VM.OpCode.STARG4] = 1 << 1,
            [VM.OpCode.STARG5] = 1 << 1,
            [VM.OpCode.STARG6] = 1 << 1,
            [VM.OpCode.STARG] = 1 << 1,
            [VM.OpCode.NEWBUFFER] = 1 << 8,
            [VM.OpCode.MEMCPY] = 1 << 11,
            [VM.OpCode.CAT] = 1 << 11,
            [VM.OpCode.SUBSTR] = 1 << 11,
            [VM.OpCode.LEFT] = 1 << 11,
            [VM.OpCode.RIGHT] = 1 << 11,
            [VM.OpCode.INVERT] = 1 << 2,
            [VM.OpCode.AND] = 1 << 3,
            [VM.OpCode.OR] = 1 << 3,
            [VM.OpCode.XOR] = 1 << 3,
            [VM.OpCode.EQUAL] = 1 << 5,
            [VM.OpCode.NOTEQUAL] = 1 << 5,
            [VM.OpCode.SIGN] = 1 << 2,
            [VM.OpCode.ABS] = 1 << 2,
            [VM.OpCode.NEGATE] = 1 << 2,
            [VM.OpCode.INC] = 1 << 2,
            [VM.OpCode.DEC] = 1 << 2,
            [VM.OpCode.ADD] = 1 << 3,
            [VM.OpCode.SUB] = 1 << 3,
            [VM.OpCode.MUL] = 1 << 3,
            [VM.OpCode.DIV] = 1 << 3,
            [VM.OpCode.MOD] = 1 << 3,
            [VM.OpCode.POW] = 1 << 6,
            [VM.OpCode.SQRT] = 1 << 6,
            [VM.OpCode.MODMUL] = 1 << 5,
            [VM.OpCode.MODPOW] = 1 << 11,
            [VM.OpCode.SHL] = 1 << 3,
            [VM.OpCode.SHR] = 1 << 3,
            [VM.OpCode.NOT] = 1 << 2,
            [VM.OpCode.BOOLAND] = 1 << 3,
            [VM.OpCode.BOOLOR] = 1 << 3,
            [VM.OpCode.NZ] = 1 << 2,
            [VM.OpCode.NUMEQUAL] = 1 << 3,
            [VM.OpCode.NUMNOTEQUAL] = 1 << 3,
            [VM.OpCode.LT] = 1 << 3,
            [VM.OpCode.LE] = 1 << 3,
            [VM.OpCode.GT] = 1 << 3,
            [VM.OpCode.GE] = 1 << 3,
            [VM.OpCode.MIN] = 1 << 3,
            [VM.OpCode.MAX] = 1 << 3,
            [VM.OpCode.WITHIN] = 1 << 3,
            [VM.OpCode.PACKMAP] = 1 << 11,
            [VM.OpCode.PACKSTRUCT] = 1 << 11,
            [VM.OpCode.PACK] = 1 << 11,
            [VM.OpCode.UNPACK] = 1 << 8,
            [VM.OpCode.NEWARRAY0] = 1 << 4,
            [VM.OpCode.NEWARRAY] = 1 << 9,
            [VM.OpCode.NEWARRAY_T] = 1 << 9,
            [VM.OpCode.NEWSTRUCT0] = 1 << 4,
            [VM.OpCode.NEWSTRUCT] = 1 << 9,
            [VM.OpCode.NEWMAP] = 1 << 3,
            [VM.OpCode.SIZE] = 1 << 2,
            [VM.OpCode.HASKEY] = 1 << 6,
            [VM.OpCode.KEYS] = 1 << 4,
            [VM.OpCode.VALUES] = 1 << 13,
            [VM.OpCode.PICKITEM] = 1 << 6,
            [VM.OpCode.APPEND] = 1 << 13,
            [VM.OpCode.SETITEM] = 1 << 13,
            [VM.OpCode.REVERSEITEMS] = 1 << 13,
            [VM.OpCode.REMOVE] = 1 << 4,
            [VM.OpCode.CLEARITEMS] = 1 << 4,
            [VM.OpCode.POPITEM] = 1 << 4,
            [VM.OpCode.ISNULL] = 1 << 1,
            [VM.OpCode.ISTYPE] = 1 << 1,
            [VM.OpCode.CONVERT] = 1 << 13,
        };

        internal static void RunScript(byte[] script)
        {
            LoadScript(script).ExecuteBenchmark();
        }

        internal static BenchmarkEngine RunScriptUntil(byte[] script, VM.OpCode opCode)
        {
            return LoadScript(script).ExecuteUntil(opCode);
        }

        internal static BenchmarkEngine LoadScript(byte[] script)
        {
            var engine = new BenchmarkEngine();
            engine.LoadScript(script);
            return engine;
        }

        internal static void FillStack(ref InstructionBuilder builder)
        {
            var initBegin = new JumpTarget();
            builder.AddInstruction(new Instruction { _opCode = VM.OpCode.INITSLOT, _operand = [1, 0] });
            builder.Push(2048);
            builder.AddInstruction(VM.OpCode.STLOC0);
            initBegin._instruction = builder.AddInstruction(VM.OpCode.NOP);
            builder.Push(0);
            builder.AddInstruction(VM.OpCode.LDLOC0);
            builder.AddInstruction(VM.OpCode.DEC);
            builder.AddInstruction(VM.OpCode.STLOC0);
            builder.AddInstruction(VM.OpCode.LDLOC0);
            builder.Jump(VM.OpCode.JMPIF, initBegin);
        }
    }
}
