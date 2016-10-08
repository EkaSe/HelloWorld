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
			return buffer;
		}

		static public void LoadToRAM (byte[] content) {
			ushort offset = 0;
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

			//Выполнение
			Processor.RunProgram ();
		}
	}
}

