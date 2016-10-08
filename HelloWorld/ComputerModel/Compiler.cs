using System;

namespace HelloWorld.ComputerModel
{
	public enum InstructionCode: ushort {
		EndOfInstructions, 
		AssignUInt8Const, 
		AssignUInt8Var, 
		MultiplyUInt8, 
		DivideUInt8, 
		LessEqualUInt8, 
		SkipIfZero, 
		Jump,
		AddUInt8Const,
		CallMethod,
		AreEqualUInt8,
		Return,
		InitStack
	};

	public class Compiler
	{
		static public ushort WriteBytesToArray (byte[] array, ushort value, ushort startOffset) {
			array [startOffset++] = BitConverter.GetBytes (value) [0];
			array [startOffset++] = BitConverter.GetBytes (value) [1];;
			return startOffset;
		}

		static public void WriteInstruction(byte[] instructions, ushort startOffset, InstructionCode instructionCode, 
			ushort argument1, ushort argument2, ushort argument3) {
			ushort offset = startOffset;
			offset = WriteBytesToArray (instructions, (ushort) instructionCode, offset);
			offset = WriteBytesToArray (instructions, argument1, offset);
			offset = WriteBytesToArray (instructions, argument2, offset);
			offset = WriteBytesToArray (instructions, argument3, offset);
		}

		static public byte[] CompileSugar () {

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

			ushort offset = 8;
			ushort thermosVolumeOffset = 9;
			ushort cupVolumeOffset = 10;
			ushort pieceOfSugarMassOffset = 11;
			ushort pieceOfSugarForCupOffset = 12;
			ushort sugarMassAvailableOffset = 13;
			ushort sugarMassRequiredOffset = 14;
			ushort isEnoughSugarOffset = 15;
			ushort sugarMassforCupOffset = 16;
			ushort cupsInThermosOffset = 17;
			ushort resultOffset = 18;

			offset = 19;

			byte[] buffer = new byte[512];
			ushort memorySize = (ushort) (offset - 8);
			offset = 0;
			WriteInstruction (buffer,   0, InstructionCode.Jump, 			 19,  0,  0);
			WriteInstruction (buffer,  19, InstructionCode.AssignUInt8Const,  9, 15,  0);
			WriteInstruction (buffer,  27, InstructionCode.AssignUInt8Const, 10,  3,  0);
			WriteInstruction (buffer,  35, InstructionCode.AssignUInt8Const, 11,  5,  0);
			WriteInstruction (buffer,  43, InstructionCode.AssignUInt8Const, 12,  3,  0);
			WriteInstruction (buffer,  51, InstructionCode.AssignUInt8Const, 13, 55,  0);
			WriteInstruction (buffer,  59, InstructionCode.MultiplyUInt8, 	 12, 11, 16);
			WriteInstruction (buffer,  67, InstructionCode.DivideUInt8, 	  9, 10, 17);
			WriteInstruction (buffer,  75, InstructionCode.MultiplyUInt8, 	 12, 17, 14);
			WriteInstruction (buffer,  83, InstructionCode.LessEqualUInt8, 	 14, 13, 15);
			WriteInstruction (buffer,  91, InstructionCode.SkipIfZero, 		 15,  0,  0);
			WriteInstruction (buffer,  99, InstructionCode.Jump, 			123,  0,  0);
			WriteInstruction (buffer, 107, InstructionCode.AssignUInt8Const, 18,  0,  0);
			WriteInstruction (buffer, 115, InstructionCode.Jump, 			131,  0,  0);
			WriteInstruction (buffer, 123, InstructionCode.AssignUInt8Const, 18,  1,  0);
			WriteInstruction (buffer, 131, InstructionCode.EndOfInstructions, 0,  0,  0);

			byte[] result = new byte[offset];
			Buffer.BlockCopy (buffer, 0, result, 0, result.Length);

			return result;
		}

		static public byte[] CompileOrderNumbers () {
			byte[] result = new byte[] {
				/* 0*/ 7, 0, 13, 0, 0, 0, 0, 0, // jump (64)
				/* 8*/ 1, 0, 0, 0, 0, 0,// stackTop, val1, val2, ordered, min, max
				/*14*/ 1, 0, 0, 0, 15, 0, 0, 0, // value1 = 15
				/*22*/ 1, 0, 1, 0, 70, 0, 0, 0, // value2 = 70
				/*30*/ 5, 0, 0, 0, 1, 0, 4, 0, // ordered = (val1 <= val2)
				/*39*/ 6, 0, 4, 0, 0, 0, 0, 0, // skipIfZero (ordered)
				/*46*/ 7, 0, 78, 0, 0, 0, 0, 0, // jump (78)
				/*54*/ 2, 0, 2, 0, 1, 0, 0, 0, // min = value2
				/*62*/ 2, 0, 3, 0, 0, 0, 0, 0, // max = value1
				/*70*/ 7, 0, 94, 0, 0, 0, 0, 0, // jump (94)
				/*78*/ 2, 0, 2, 0, 0, 0, 0, 0, // min = value1
				/*86*/ 2, 0, 3, 0, 1, 0, 0, 0, // max = value2
				/*94*/ 0, 0, 0, 0, 0, 0, 0, 0 // End
			};
			return result;
		}

		static public byte[] CompileTestFactorial() {
			ushort stackTopOffset = 8;
			ushort stack0Offset = 0; //offset relatively to stackTopOffset; offset of return point
			ushort stack1Offset = 1; //offset relatively to stackTopOffset

			ushort nOffset = 16; //relatively to current stack0offset - local memory start
			ushort resultsEqualOffset = 17;
			ushort factorialCycleOffset = 18;
			ushort factorialRecursiveOffset = 19;
			ushort iOffset = 20;
			ushort iLessEqualNOffset = 21;


			byte[] buffer = new byte[512];
			WriteInstruction (buffer,   0, InstructionCode.Jump,  			 24,  0,  0);
			WriteInstruction (buffer,  24, InstructionCode.InitStack, 		  8,  0,  0);
			WriteInstruction (buffer,  32, InstructionCode.AssignUInt8Const,  8,  0,  0);
			WriteInstruction (buffer,  40, InstructionCode.AssignUInt8Const, 16,  5,  0);
			WriteInstruction (buffer,  48, InstructionCode.AddUInt8Const,	  8,  1,  0);
			WriteInstruction (buffer,  56, InstructionCode.AssignUInt8Const, 10, 72,  0);
			WriteInstruction (buffer,  64, InstructionCode.CallMethod, 		 96,  0,  0);
			WriteInstruction (buffer,  72, InstructionCode.AssignUInt8Const,  8,  0,  0);
			WriteInstruction (buffer,  80, InstructionCode.AreEqualUInt8, 	 18, 19, 17);
			WriteInstruction (buffer,  88, InstructionCode.EndOfInstructions, 0,  0,  0);

			WriteInstruction (buffer,  96, InstructionCode.AssignUInt8Const, 16,  5,  0);
			WriteInstruction (buffer, 104, InstructionCode.AssignUInt8Const, 18,  1,  0);
			WriteInstruction (buffer, 112, InstructionCode.AssignUInt8Const, 20,  1,  0);
			WriteInstruction (buffer, 120, InstructionCode.MultiplyUInt8, 	 20, 18, 18);
			WriteInstruction (buffer, 128, InstructionCode.AddUInt8Const, 	 20,  1, 20);
			WriteInstruction (buffer, 136, InstructionCode.LessEqualUInt8, 	 20, 16, 21);
			WriteInstruction (buffer, 144, InstructionCode.SkipIfZero, 		 21,  0,  0);
			WriteInstruction (buffer, 152, InstructionCode.Jump, 			120,  0,  0);
			WriteInstruction (buffer, 160, InstructionCode.Return, 			  0,   0, 0);

			byte[] result = new byte[168];
			Buffer.BlockCopy (buffer, 0, result, 0, result.Length);

			return result;
		}
	}
}

