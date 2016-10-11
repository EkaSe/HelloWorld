using System;

namespace HelloWorld.ComputerModel
{
	public enum InstructionCode: ushort {
		/* 0*/ EndOfInstructions, 
		/* 1*/ AssignUInt8Const, 
		/* 2*/ AssignUInt8Var, 
		/* 3*/ MultiplyUInt8, 
		/* 4*/ DivideUInt8, 
		/* 5*/ LessEqualUInt8, 
		/* 6*/ SkipIfZero, 
		/* 7*/ Jump,
		/* 8*/ AddUInt8Const,
		/* 9*/ CallMethod,
		/*10*/ AreEqualUInt8,
		/*11*/ Return,
		/*12*/ InitStack,
		/*13*/ AssignUInt8ConstStack,
		/*14*/ SubtractUInt8Const,
		/*15*/ AssignUInt8VarStack, 
		/*16*/ MultiplyUInt8Stack, 
		/*17*/ DivideUInt8Stack, 
		/*18*/ LessEqualUInt8Stack, 
		/*19*/ AddUInt8ConstStack,
		/*20*/ AreEqualUInt8Stack,
		/*21*/ SubtractUInt8ConstStack,
		/*22*/ SkipIfZeroStack
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

			ushort nOffset = 1; 
			ushort resultsEqualOffset = 2;
			ushort factorialCycleOffset = 3;
			ushort factorialRecursiveOffset = 4;

			ushort factorialCycleNOffset = 1; 
			ushort factorialCycleResultOffset = 2;
			ushort factorialCycleIOffset = 3;
			ushort factorialCycleILessEqualNOffset = 4;

			byte[] buffer = new byte[256];

			WriteInstruction (buffer,   0, InstructionCode.Jump, 64, 0, 0);
			WriteInstruction (buffer,  64, InstructionCode.InitStack, stackTopAddress, 0, 0);
			WriteInstruction (buffer,  72, InstructionCode.AssignUInt8Const, stackTopAddress, 0, 0);
			WriteInstruction (buffer,  80, InstructionCode.AssignUInt8ConstStack, nOffset, 5, 0);
			WriteInstruction (buffer,  88, InstructionCode.AssignUInt8VarStack, (ushort) (factorialCycleNOffset + 5), nOffset, 0);
			WriteInstruction (buffer,  96, InstructionCode.AddUInt8Const, stackTopAddress, 5, stackTopAddress);
			WriteInstruction (buffer, 104, InstructionCode.AssignUInt8ConstStack, 0, 120, 0);
			WriteInstruction (buffer, 112, InstructionCode.CallMethod, 152, 0, 0);
			WriteInstruction (buffer, 120, InstructionCode.SubtractUInt8Const, stackTopAddress, 5, stackTopAddress);
			WriteInstruction (buffer, 128, InstructionCode.AssignUInt8VarStack, factorialCycleOffset, (ushort) (factorialCycleResultOffset + 5), 0);
			WriteInstruction (buffer, 136, InstructionCode.AreEqualUInt8Stack, factorialCycleOffset, factorialRecursiveOffset, resultsEqualOffset);
			WriteInstruction (buffer, 144, InstructionCode.EndOfInstructions, 0, 0, 0);

			WriteInstruction (buffer, 152, InstructionCode.AssignUInt8ConstStack, factorialCycleResultOffset, 1, 0);
			WriteInstruction (buffer, 160, InstructionCode.AssignUInt8ConstStack, factorialCycleIOffset, 1, 0);
			WriteInstruction (buffer, 168, InstructionCode.MultiplyUInt8Stack, factorialCycleIOffset, factorialCycleResultOffset, factorialCycleResultOffset);
			WriteInstruction (buffer, 176, InstructionCode.AddUInt8ConstStack, factorialCycleIOffset, 1, factorialCycleIOffset);
			WriteInstruction (buffer, 184, InstructionCode.LessEqualUInt8Stack, factorialCycleIOffset, factorialCycleNOffset, factorialCycleILessEqualNOffset);
			WriteInstruction (buffer, 192, InstructionCode.SkipIfZeroStack, factorialCycleILessEqualNOffset, 0, 0);
			WriteInstruction (buffer, 200, InstructionCode.Jump, 168, 0, 0);
			WriteInstruction (buffer, 208, InstructionCode.Return, 0, 0, 0);

			byte[] result = new byte[216];
			Buffer.BlockCopy (buffer, 0, result, 0, result.Length);

			return result;
		}
	}
}