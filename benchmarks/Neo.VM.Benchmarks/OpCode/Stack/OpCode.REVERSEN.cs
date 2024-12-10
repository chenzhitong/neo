// Copyright (C) 2015-2024 The Neo Project.
//
// OpCode.REVERSEN.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

namespace Neo.VM.Benchmark.OpCode
{
    public class OpCode_REVERSEN : OpCodeBase
    {
        protected override VM.OpCode Opcode => VM.OpCode.REVERSEN;

        protected override byte[] CreateOneOpCodeScript()
        {
            var builder = new InstructionBuilder();
            var initBegin = new JumpTarget();
            builder.AddInstruction(new Instruction { _opCode = VM.OpCode.INITSLOT, _operand = [1, 0] });
            builder.Push(ItemCount);
            builder.AddInstruction(VM.OpCode.STLOC0);
            initBegin._instruction = builder.AddInstruction(VM.OpCode.NOP);
            builder.Push(ushort.MaxValue * 2);
            builder.AddInstruction(VM.OpCode.NEWBUFFER);
            builder.AddInstruction(VM.OpCode.LDLOC0);
            builder.AddInstruction(VM.OpCode.DEC);
            builder.AddInstruction(VM.OpCode.STLOC0);
            builder.AddInstruction(VM.OpCode.LDLOC0);
            builder.Jump(VM.OpCode.JMPIF, initBegin);
            // var loopBegin = new JumpTarget { _instruction = builder.AddInstruction(VM.OpCode.NOP) };
            builder.Push(ItemCount);
            builder.AddInstruction(VM.OpCode.REVERSEN);
            // builder.Jump(VM.OpCode.JMP, loopBegin);
            return builder.ToArray();
        }

        protected override byte[] CreateOneGASScript()
        {
            var builder = new InstructionBuilder();
            var initBegin = new JumpTarget();
            builder.AddInstruction(new Instruction { _opCode = VM.OpCode.INITSLOT, _operand = [1, 0] });
            builder.Push(ItemCount);
            builder.AddInstruction(VM.OpCode.STLOC0);
            initBegin._instruction = builder.AddInstruction(VM.OpCode.NOP);
            builder.Push(ushort.MaxValue * 2);
            builder.AddInstruction(VM.OpCode.NEWBUFFER);
            builder.AddInstruction(VM.OpCode.LDLOC0);
            builder.AddInstruction(VM.OpCode.DEC);
            builder.AddInstruction(VM.OpCode.STLOC0);
            builder.AddInstruction(VM.OpCode.LDLOC0);
            builder.Jump(VM.OpCode.JMPIF, initBegin);
            var loopBegin = new JumpTarget { _instruction = builder.AddInstruction(VM.OpCode.NOP) };
            builder.Push(ItemCount);
            builder.AddInstruction(VM.OpCode.REVERSEN);
            builder.Jump(VM.OpCode.JMP, loopBegin);
            return builder.ToArray();
        }
    }
}

// | Method          | ItemCount | Mean     | Error     | StdDev    | Median   |
//     |---------------- |---------- |---------:|----------:|----------:|---------:|
//     | Bench_OneOpCode | 1         | 1.089 us | 0.0546 us | 0.1457 us | 1.100 us |
//     | Bench_OneOpCode | 32        | 1.344 us | 0.0446 us | 0.1214 us | 1.300 us |
//     | Bench_OneOpCode | 128       | 1.540 us | 0.0532 us | 0.1393 us | 1.500 us |
//     | Bench_OneOpCode | 1024      | 3.968 us | 0.1582 us | 0.4614 us | 3.800 us |
//     | Bench_OneOpCode | 2040      | 6.327 us | 0.1916 us | 0.5620 us | 6.200 us |


// | Method          | ItemCount | Mean     | Error     | StdDev    |
//     |---------------- |---------- |---------:|----------:|----------:|
//     | Bench_OneOpCode | 2         | 1.285 us | 0.0383 us | 0.1061 us |
//     | Bench_OneOpCode | 32        | 2.029 us | 0.1094 us | 0.3208 us |
//     | Bench_OneOpCode | 128       | 2.709 us | 0.1647 us | 0.4779 us |
//     | Bench_OneOpCode | 1024      | 4.553 us | 0.6520 us | 1.9121 us |
//     | Bench_OneOpCode | 2040      | 6.112 us | 0.6347 us | 1.8715 us |


// | Method       | ItemCount | Mean       | Error    | StdDev   |
// |------------- |---------- |-----------:|---------:|---------:|
// | Bench_OneGAS | 4         |   174.9 ms |  1.24 ms |  1.10 ms |
// | Bench_OneGAS | 8         |   182.5 ms |  3.28 ms |  2.91 ms |
// | Bench_OneGAS | 16        |   202.4 ms |  2.36 ms |  2.09 ms |
// | Bench_OneGAS | 32        |   244.1 ms |  3.49 ms |  2.92 ms |
// | Bench_OneGAS | 64        |   302.2 ms |  5.39 ms |  5.05 ms |
// | Bench_OneGAS | 128       |   417.8 ms |  4.31 ms |  3.82 ms |
// | Bench_OneGAS | 256       |   644.1 ms |  8.12 ms |  7.59 ms |
// | Bench_OneGAS | 512       | 1,119.7 ms |  8.87 ms |  8.29 ms |
// | Bench_OneGAS | 1024      | 2,057.8 ms | 11.39 ms | 10.10 ms |
// | Bench_OneGAS | 2040      | 4,009.0 ms | 12.07 ms | 10.08 ms |


// | Method       | ItemCount | Mean     | Error    | StdDev   |
// |------------- |---------- |---------:|---------:|---------:|
// | Bench_OneGAS | 4         | 447.6 ms |  7.13 ms |  6.67 ms |
// | Bench_OneGAS | 8         | 459.8 ms |  9.08 ms |  8.49 ms |
// | Bench_OneGAS | 16        | 521.0 ms |  7.90 ms |  7.39 ms |
// | Bench_OneGAS | 32        | 628.7 ms | 12.46 ms | 13.85 ms |
// | Bench_OneGAS | 64        | 511.1 ms |  9.64 ms |  9.02 ms |
// | Bench_OneGAS | 128       | 523.2 ms |  6.63 ms |  6.21 ms |
// | Bench_OneGAS | 256       | 469.7 ms |  6.49 ms |  6.08 ms |
// | Bench_OneGAS | 512       | 473.5 ms |  3.18 ms |  2.98 ms |
// | Bench_OneGAS | 1024      | 469.4 ms |  8.33 ms |  7.80 ms |
// | Bench_OneGAS | 2040      | 465.1 ms |  6.58 ms |  6.15 ms |