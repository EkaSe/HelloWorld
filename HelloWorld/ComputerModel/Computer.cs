using System;

namespace HelloWorld.ComputerModel
{
	public class Computer
	{
		static public int SaveToFile (byte[] content) {
			int offset = Memory.HDDContentLength;
			int handler = offset;
			offset = Compiler.WriteBytesToArray (Memory.HDD, (ushort) content.Length, (ushort) offset);
			Buffer.BlockCopy (content, 0, Memory.HDD, offset, content.Length);
			Memory.HDDContentLength = offset + content.Length;
			return handler;
		}

		static public byte[] ReadFromFile (int handler) {
			ushort instructionsSize = BitConverter.ToUInt16 (Memory.HDD, handler);
			byte[] buffer = new byte[instructionsSize];
			Buffer.BlockCopy (Memory.HDD, handler + 2, buffer, 0, instructionsSize);
			Memory.localMemoryOffset = instructionsSize;
			return buffer;
		}

		static public void LoadToRAM (byte[] content) {
			Memory.localMemoryOffset = (ushort) content.Length;
			Buffer.BlockCopy (content, 0, Memory.RAM, 0, content.Length);
		}

		static public void Start () {

			//Компиляция
			byte[] buffer = Compiler.CompileTestFactorial ();
			int TestFactorialOffset = SaveToFile (buffer);

			buffer = Compiler.CompileOrderNumbers ();
			int OrderNumbersOffset = SaveToFile (buffer);

			buffer = Compiler.CompileSugar ();
			int SugarOffset = SaveToFile (buffer);

			buffer = ReadFromFile (TestFactorialOffset);
			LoadToRAM (buffer);

			/*
			ushort memoryStart = instructionsSize;

			ushort currentOffset = 0;
			while (currentOffset < instructionsSize) {
				ushort currentInstruction = BitConverter.ToUInt16 (Memory.RAM, currentOffset);
				ushort arg1 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 2));
				ushort arg2 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 4));
				ushort arg3 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 6));
				if ((currentInstruction != (ushort) InstructionCode.Jump) &&
					(currentInstruction != (ushort) InstructionCode.CallMethod) &&
					(currentInstruction != (ushort) InstructionCode.EndOfInstructions))
					Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg1 + memoryStart), (ushort) (currentOffset + 2));
				if ((currentInstruction != (ushort) InstructionCode.AssignUInt8Const) &&
					(currentInstruction != (ushort) InstructionCode.AddUInt8Const))
					Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg2 + memoryStart), (ushort) (currentOffset + 4));
				Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg3 + memoryStart), (ushort) (currentOffset + 6));
				currentOffset += 8;
			};
			*/

			//Выполнение
			Processor.RunProgram ();
		}
	}
}

