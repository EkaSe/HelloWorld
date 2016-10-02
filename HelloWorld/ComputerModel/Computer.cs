using System;

namespace HelloWorld.ComputerModel
{
	public class Computer
	{
		static public void Compile (ushort memorySize, byte[] instructions, ushort instructionsSize) {
			ushort currentOffset = 0;
			Compiler.WriteInstruction (Memory.RAM, ref currentOffset, (ushort) InstructionCode.Jump, (ushort) 24, 0, 0);
			Memory.RAM [Memory.stackTopOffset] = 9;
			Memory.RAM [Memory.stackOffset] = (byte) (Memory.instructionsOffset + instructionsSize);
			Memory.RAM [Memory.stackOffset + 1] = (byte) (Memory.RAM [Memory.stackOffset] + memorySize);
			for (int i = 0; i < instructionsSize; i++) {
				Memory.RAM [Memory.instructionsOffset + i] = instructions [i];
			};

			/*
			while (currentOffset < 250) {
				ushort currentInstruction = BitConverter.ToUInt16 (Memory.RAM, currentOffset);
				ushort arg1 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 2));
				ushort arg2 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 4));
				ushort arg3 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 6));
				Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg1 + 8), (ushort) (currentOffset + 2));
				if ((currentInstruction != (ushort) InstructionCode.AssignUInt8Const) &&
					(currentInstruction != (ushort) InstructionCode.AddUInt8Const))
					Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg2 + 8), (ushort) (currentOffset + 4));
				Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg3 + 8), (ushort) (currentOffset + 6));
				if (currentInstruction == (ushort) InstructionCode.Jump)
					Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg1 + 8 + memorySize), (ushort) (currentOffset + 2));
				currentOffset += 8;
			};*/
		}

		static public void Start () {
			//Написание программы
			/*
			 static public void EnoughSugar () {
				double ThermosVolume = 1.5; 
				double cupVolume = 0.3;
				double pieceOfSugarMass = 5;
				int piecesOfSugarForCup = 3;
				double sugarMass = 55;
				double sugarMassRequired;
				sugarMassRequired = thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume;
				if (sugarMassRequired <= sugarMass)
					result = 1;
				else result = 0;
			}*/

			//Компиляция

			ushort instructionsLength = 256;
			byte[] instructions = new byte[instructionsLength];
			ushort memorySize = 0;
			//Compiler.WriteInstructionsSugar(instructions, out memorySize);
			//Compiler.WriteInstructionsOrderNumbers (instructions, out memorySize);
			Compiler.WriteInstructionsFactorialCycle (instructions, out memorySize, out instructionsLength);
			Compile (memorySize, instructions, instructionsLength);

			//Выполнение
			Processor.RunProgram ();
		}
	}
}

