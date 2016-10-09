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

			ushort thermosVolumeOffset = 8;
			ushort cupVolumeOffset = 9;
			ushort pieceOfSugarMassOffset = 10;
			ushort pieceOfSugarForCupOffset = 11;
			ushort sugarMassAvailableOffset = 12;
			ushort sugarMassRequiredOffset = 13;
			ushort isEnoughSugarOffset = 14;
			ushort sugarMassforCupOffset = 15;
			ushort cupsInThermosOffset = 16;
			ushort resultOffset = 17;

			byte[] buffer = new byte[512];

			WriteInstruction (buffer,   0, InstructionCode.Jump, 			 24, 						0,  						0);
			WriteInstruction (buffer,  24, InstructionCode.AssignUInt8Const, thermosVolumeOffset, 		15,  						0);
			WriteInstruction (buffer,  32, InstructionCode.AssignUInt8Const, cupVolumeOffset,  			3,  						0);
			WriteInstruction (buffer,  40, InstructionCode.AssignUInt8Const, pieceOfSugarMassOffset,  	5,  						0);
			WriteInstruction (buffer,  48, InstructionCode.AssignUInt8Const, pieceOfSugarForCupOffset, 	3,  						0);
			WriteInstruction (buffer,  56, InstructionCode.AssignUInt8Const, sugarMassAvailableOffset, 	55,  						0);
			WriteInstruction (buffer,  64, InstructionCode.MultiplyUInt8, 	 pieceOfSugarForCupOffset, 	pieceOfSugarMassOffset, 	sugarMassforCupOffset);
			WriteInstruction (buffer,  72, InstructionCode.DivideUInt8, 	 thermosVolumeOffset, 		cupVolumeOffset, 			cupsInThermosOffset);
			WriteInstruction (buffer,  80, InstructionCode.MultiplyUInt8, 	 sugarMassforCupOffset, 	cupsInThermosOffset, 		sugarMassRequiredOffset);
			WriteInstruction (buffer,  88, InstructionCode.LessEqualUInt8, 	 sugarMassRequiredOffset, 	sugarMassAvailableOffset, 	isEnoughSugarOffset);
			WriteInstruction (buffer,  96, InstructionCode.SkipIfZero, 		 isEnoughSugarOffset, 		0, 							0);
			WriteInstruction (buffer, 104, InstructionCode.Jump, 			 128,  						0,  						0);
			WriteInstruction (buffer, 112, InstructionCode.AssignUInt8Const, resultOffset, 				0, 							0);
			WriteInstruction (buffer, 120, InstructionCode.Jump, 			 136,  						0, 		 					0);
			WriteInstruction (buffer, 128, InstructionCode.AssignUInt8Const, resultOffset, 				1, 							0);
			WriteInstruction (buffer, 136, InstructionCode.EndOfInstructions,0,  						0,  						0);

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

			WriteInstruction (buffer,   0, InstructionCode.Jump,  			 24,  					0,  						0);
			WriteInstruction (buffer,  24, InstructionCode.InitStack, 		 stackTopOffset,  		0,  						0);
			WriteInstruction (buffer,  32, InstructionCode.AssignUInt8Const, stackTopOffset, 		stack0Offset, 				0);
			WriteInstruction (buffer,  40, InstructionCode.AssignUInt8Const, nOffset, 				5, 							0);
			WriteInstruction (buffer,  48, InstructionCode.AddUInt8Const,	 stackTopOffset, 		stack1Offset, 				0);
			WriteInstruction (buffer,  56, InstructionCode.AssignUInt8Const, (ushort) (stackTopOffset + stack1Offset + 1), 72,  0);
			WriteInstruction (buffer,  64, InstructionCode.CallMethod, 		 96,  					0,  						0);
			WriteInstruction (buffer,  72, InstructionCode.AssignUInt8Const, stackTopOffset, 		stack0Offset, 				0);
			WriteInstruction (buffer,  80, InstructionCode.AreEqualUInt8, 	 factorialCycleOffset, 	factorialRecursiveOffset, 	resultsEqualOffset);
			WriteInstruction (buffer,  88, InstructionCode.EndOfInstructions,0,  					0,  						0);

			WriteInstruction (buffer, 96, InstructionCode.AssignUInt8Const,  factorialCycleOffset, 	1, 							0);
			WriteInstruction (buffer, 104, InstructionCode.AssignUInt8Const, iOffset, 				1, 							0);
			WriteInstruction (buffer, 112, InstructionCode.MultiplyUInt8, 	 iOffset, 				factorialCycleOffset, 		factorialCycleOffset);
			WriteInstruction (buffer, 120, InstructionCode.AddUInt8Const, 	 iOffset, 				1, 							iOffset);
			WriteInstruction (buffer, 128, InstructionCode.LessEqualUInt8, 	 iOffset, 				nOffset, 					iLessEqualNOffset);
			WriteInstruction (buffer, 136, InstructionCode.SkipIfZero, 		 iLessEqualNOffset, 	0, 							0);
			WriteInstruction (buffer, 144, InstructionCode.Jump, 			 112,  					0,  						0);
			WriteInstruction (buffer, 152, InstructionCode.Return, 			 0,   					0, 							0);

			byte[] result = new byte[160];
			Buffer.BlockCopy (buffer, 0, result, 0, result.Length);

			return result;
		}
	}
}

