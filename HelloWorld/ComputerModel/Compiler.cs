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
		AddUInt8Const
	};

	public class Compiler
	{
		static public ushort WriteBytesToArray (byte[] array, ushort value, ushort startOffset) {
			array [startOffset++] = BitConverter.GetBytes (value) [0];
			array [startOffset++] = BitConverter.GetBytes (value) [1];;
			return startOffset;
		}

		static public void WriteInstruction(byte[] instructions, ref ushort offset, ushort instructionCode, 
			ushort argument1, ushort argument2, ushort argument3) {
			offset = WriteBytesToArray (instructions, instructionCode, offset);
			offset = WriteBytesToArray (instructions, argument1, offset);
			offset = WriteBytesToArray (instructions, argument2, offset);
			offset = WriteBytesToArray (instructions, argument3, offset);
		}

		static public void WriteInstructionsSugar (byte[] instructions, out ushort memorySize) {
			ushort currentOffset = 0;
			ushort thermosVolumeOffset = currentOffset++;
			ushort cupVolumeOffset = currentOffset++;
			ushort pieceOfSugarMassOffset = currentOffset++;
			ushort pieceOfSugarForCupOffset = currentOffset++;
			ushort sugarMassAvailableOffset = currentOffset++;
			ushort sugarMassRequiredOffset = currentOffset++;
			ushort isEnoughSugarOffset = currentOffset++;
			ushort sugarMassforCupOffset = currentOffset++;
			ushort cupsInThermosOffset = currentOffset++;
			ushort resultOffset = currentOffset++;
			ushort currentInstructionOffset = 0;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, thermosVolumeOffset, 15, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, cupVolumeOffset, 3, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, pieceOfSugarMassOffset, 5, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, pieceOfSugarForCupOffset, 3, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, sugarMassAvailableOffset, 55, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.MultiplyUInt8, pieceOfSugarForCupOffset, pieceOfSugarMassOffset, sugarMassforCupOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.DivideUInt8, thermosVolumeOffset, cupVolumeOffset, cupsInThermosOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.MultiplyUInt8, sugarMassforCupOffset, cupsInThermosOffset, sugarMassRequiredOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.LessEqualUInt8, sugarMassRequiredOffset, sugarMassAvailableOffset, isEnoughSugarOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.SkipIfZero, isEnoughSugarOffset, 0, 0);
			ushort ifNotZeroOffset = currentInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.Jump, 0, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, resultOffset, 0, 0);
			ushort ifZeroOffset = currentInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.Jump, 0, 0, 0);
			ifNotZeroOffset = WriteBytesToArray (instructions, (ushort) currentInstructionOffset, (ushort) (ifNotZeroOffset + 2));
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, resultOffset, 1, 0);
			ifZeroOffset = WriteBytesToArray (instructions, (ushort) currentInstructionOffset, (ushort) (ifZeroOffset + 2));
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.EndOfInstructions, 0, 0, 0);

			memorySize = currentOffset;
		}

		static public void WriteInstructionsOrderNumbers (byte[] instructions, out ushort memorySize) {
			byte[] composeInstructions = new byte[] {
				/* 0*/ 1, 0, 0, 0, 15, 0, 0, 0, // value1 = 15
				/* 8*/ 1, 0, 1, 0, 70, 0, 0, 0, // value2 = 70
				/*16*/ 5, 0, 0, 0, 1, 0, 4, 0, // ordered = (val1 <= val2)
				/*24*/ 6, 0, 4, 0, 0, 0, 0, 0, // skipIfZero (ordered)
				/*32*/ 7, 0, 64, 0, 0, 0, 0, 0, // jump (64)
				/*40*/ 2, 0, 2, 0, 1, 0, 0, 0, // min = value2
				/*48*/ 2, 0, 3, 0, 0, 0, 0, 0, // max = value1
				/*56*/ 7, 0, 80, 0, 0, 0, 0, 0, // jump (80)
				/*64*/ 2, 0, 2, 0, 0, 0, 0, 0, // min = value1
				/*72*/ 2, 0, 3, 0, 1, 0, 0, 0, // max = value2
				/*80*/ 0, 0 // End
			};
			for (int i = 0; i < 82; i++) {
				instructions [i] = composeInstructions [i];
			};
			memorySize = 5;
		}

		static public void WriteInstructionsFactorialCycle(byte[] instructions, out ushort memorySize) {
			ushort currentOffset = 0;
			ushort nOffset = currentOffset++;
			ushort factorialOffset = currentOffset++;
			ushort iOffset = currentOffset++;
			ushort iLessEqualNOffset = currentOffset++;
			ushort currentInstructionOffset = 0;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, nOffset, 5, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, factorialOffset, 1, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, iOffset, 1, 0);
			ushort cycleStart = currentInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.MultiplyUInt8, iOffset, factorialOffset, factorialOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AddUInt8Const, iOffset, 1, iOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.LessEqualUInt8, iOffset, nOffset, iLessEqualNOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.SkipIfZero, iLessEqualNOffset, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.Jump, cycleStart, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.EndOfInstructions, 0, 0, 0);

			memorySize = currentOffset;
		}
	}
}

