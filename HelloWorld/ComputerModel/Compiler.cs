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
		InitStack,
		AssignUInt8ConstStack
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

			ushort thermosVolumeAddress = 8;
			ushort cupVolumeAddress = 9;
			ushort pieceOfSugarMassAddress = 10;
			ushort pieceOfSugarForCupAddress = 11;
			ushort sugarMassAvailableAddress = 12;
			ushort sugarMassRequiredAddress = 13;
			ushort isEnoughSugarAddress = 14;
			ushort sugarMassforCupAddress = 15;
			ushort cupsInThermosAddress = 16;
			ushort resultAddress = 17;

			byte[] buffer = new byte[512];

			WriteInstruction (buffer,   0, InstructionCode.Jump, 24, 0, 0);
			WriteInstruction (buffer,  24, InstructionCode.AssignUInt8Const, thermosVolumeAddress, 15, 0);
			WriteInstruction (buffer,  32, InstructionCode.AssignUInt8Const, cupVolumeAddress, 3, 0);
			WriteInstruction (buffer,  40, InstructionCode.AssignUInt8Const, pieceOfSugarMassAddress, 5, 0);
			WriteInstruction (buffer,  48, InstructionCode.AssignUInt8Const, pieceOfSugarForCupAddress, 3, 0);
			WriteInstruction (buffer,  56, InstructionCode.AssignUInt8Const, sugarMassAvailableAddress, 55, 0);
			WriteInstruction (buffer,  64, InstructionCode.MultiplyUInt8, pieceOfSugarForCupAddress, pieceOfSugarMassAddress, sugarMassforCupAddress);
			WriteInstruction (buffer,  72, InstructionCode.DivideUInt8, thermosVolumeAddress, cupVolumeAddress, cupsInThermosAddress);
			WriteInstruction (buffer,  80, InstructionCode.MultiplyUInt8, sugarMassforCupAddress, cupsInThermosAddress, sugarMassRequiredAddress);
			WriteInstruction (buffer,  88, InstructionCode.LessEqualUInt8, sugarMassRequiredAddress, sugarMassAvailableAddress, isEnoughSugarAddress);
			WriteInstruction (buffer,  96, InstructionCode.SkipIfZero, isEnoughSugarAddress, 0, 0);
			WriteInstruction (buffer, 104, InstructionCode.Jump, 128, 0, 0);
			WriteInstruction (buffer, 112, InstructionCode.AssignUInt8Const, resultAddress, 0, 0);
			WriteInstruction (buffer, 120, InstructionCode.Jump, 136, 0, 0);
			WriteInstruction (buffer, 128, InstructionCode.AssignUInt8Const, resultAddress, 1, 0);
			WriteInstruction (buffer, 136, InstructionCode.EndOfInstructions, 0, 0, 0);

			byte[] result = new byte[144];
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
			ushort stackTopAddress = 8;
			ushort stack0Offset = 0; //offset relatively to stackTopOffset; offset of return point
			ushort stack1Offset = 1; //offset relatively to stackTopOffset

			ushort nAddress = 16; //relatively to current stack0offset - local memory start
			ushort resultsEqualAddress = 17;
			ushort factorialCycleAddress = 18;
			ushort factorialRecursiveAddress = 19;
			ushort iAddress = 20;
			ushort iLessEqualNAddress = 21;

			byte[] buffer = new byte[512];

			WriteInstruction (buffer,   0, InstructionCode.Jump, 24, 0, 0);
			WriteInstruction (buffer,  24, InstructionCode.InitStack, stackTopAddress, 0, 0);
			WriteInstruction (buffer,  32, InstructionCode.AssignUInt8Const, stackTopAddress, stack0Offset, 0);
			WriteInstruction (buffer,  40, InstructionCode.AssignUInt8Const, nAddress, 5, 0);
			WriteInstruction (buffer,  48, InstructionCode.AddUInt8Const, stackTopAddress, stack1Offset, 0);
			WriteInstruction (buffer,  56, InstructionCode.AssignUInt8Const, (ushort) (stackTopAddress + stack1Offset + 1), 72, 0);
			WriteInstruction (buffer,  64, InstructionCode.CallMethod, 96, 0, 0);
			WriteInstruction (buffer,  72, InstructionCode.AssignUInt8Const, stackTopAddress, stack0Offset, 0);
			WriteInstruction (buffer,  80, InstructionCode.AreEqualUInt8, factorialCycleAddress, factorialRecursiveAddress, resultsEqualAddress);
			WriteInstruction (buffer,  88, InstructionCode.EndOfInstructions, 0, 0, 0);

			WriteInstruction (buffer,  96, InstructionCode.AssignUInt8Const, factorialCycleAddress, 1, 0);
			WriteInstruction (buffer, 104, InstructionCode.AssignUInt8Const, iAddress, 1, 0);
			WriteInstruction (buffer, 112, InstructionCode.MultiplyUInt8, iAddress, factorialCycleAddress, factorialCycleAddress);
			WriteInstruction (buffer, 120, InstructionCode.AddUInt8Const, iAddress, 1, iAddress);
			WriteInstruction (buffer, 128, InstructionCode.LessEqualUInt8, iAddress, nAddress, iLessEqualNAddress);
			WriteInstruction (buffer, 136, InstructionCode.SkipIfZero, iLessEqualNAddress, 0, 0);
			WriteInstruction (buffer, 144, InstructionCode.Jump, 112, 0, 0);
			WriteInstruction (buffer, 152, InstructionCode.Return, 0, 0, 0);

			byte[] result = new byte[160];
			Buffer.BlockCopy (buffer, 0, result, 0, result.Length);

			return result;
		}
	}
}

