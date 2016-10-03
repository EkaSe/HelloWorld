using System;

namespace HelloWorld.ComputerModel
{
	public class Computer
	{
		static public void Compile () {
			ushort currentOffset = 0;
			Compiler.WriteInstruction (Memory.RAM, ref currentOffset, (ushort)InstructionCode.Jump, (ushort)24, 0, 0);
			Memory.RAM [Memory.stackTopOffset] = 9;

			ushort instructionsSize = 256;
			ushort memorySize = 0;
			Compiler.WriteInstructionsTestFactorial (Memory.RAM, Memory.instructionsOffset, out memorySize, out instructionsSize);

			Memory.RAM [Memory.stackOffset] = (byte)(Memory.instructionsOffset + instructionsSize);
			Memory.RAM [Memory.stackOffset + 1] = (byte)(Memory.RAM [Memory.stackOffset] + memorySize);
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
			Compile ();

			//Выполнение
			Processor.RunProgram ();
		}
	}
}

