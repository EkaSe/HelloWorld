﻿using System;

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
		AreEqualUInt8
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

			byte[] buffer = new byte[512];
			ushort offset = 0;

			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, thermosVolumeOffset, 15, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, cupVolumeOffset, 3, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, pieceOfSugarMassOffset, 5, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, pieceOfSugarForCupOffset, 3, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, sugarMassAvailableOffset, 55, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.MultiplyUInt8, pieceOfSugarForCupOffset, pieceOfSugarMassOffset, sugarMassforCupOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.DivideUInt8, thermosVolumeOffset, cupVolumeOffset, cupsInThermosOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.MultiplyUInt8, sugarMassforCupOffset, cupsInThermosOffset, sugarMassRequiredOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.LessEqualUInt8, sugarMassRequiredOffset, sugarMassAvailableOffset, isEnoughSugarOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.SkipIfZero, isEnoughSugarOffset, 0, 0);
			ushort ifNotZeroOffset = offset;
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.Jump, 0, 0, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, resultOffset, 0, 0);
			ushort ifZeroOffset = offset;
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.Jump, 0, 0, 0);
			ifNotZeroOffset = WriteBytesToArray (buffer, (ushort) offset, (ushort) (ifNotZeroOffset + 2));
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, resultOffset, 1, 0);
			ifZeroOffset = WriteBytesToArray (buffer, (ushort) offset, (ushort) (ifZeroOffset + 2));
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.EndOfInstructions, 0, 0, 0);

			byte[] result = new byte[offset];
			Buffer.BlockCopy (buffer, 0, result, 0, result.Length);

			return result;
		}

		static public byte[] CompileOrderNumbers () {
			byte[] result = new byte[] {
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
				/*80*/ 0, 0, 0, 0, 0, 0, 0, 0 // End
			};
			return result;
		}

		static public byte[] CompileTestFactorial() {
			ushort currentOffset = 0;
			ushort nOffset = currentOffset++;
			ushort resultsEqualOffset = currentOffset++;
			ushort factorialCycleOffset = currentOffset++;
			ushort factorialRecursiveOffset = currentOffset++;
			ushort iOffset = currentOffset++;
			ushort iLessEqualNOffset = currentOffset++;

			byte[] buffer = new byte[512];
			ushort offset = 0;

			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, nOffset, 5, 0);
			ushort factorialCycleCall = offset;
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.CallMethod, (ushort) (offset + 8), nOffset, factorialCycleOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AreEqualUInt8, factorialCycleOffset, factorialRecursiveOffset, resultsEqualOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.EndOfInstructions, 0, 0, 0);
			ushort unused = WriteBytesToArray (buffer, offset, (ushort) (factorialCycleCall + 2));
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, nOffset, 5, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, factorialCycleOffset, 1, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AssignUInt8Const, iOffset, 1, 0);
			ushort cycleStart = offset;
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.MultiplyUInt8, iOffset, factorialCycleOffset, factorialCycleOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.AddUInt8Const, iOffset, 1, iOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.LessEqualUInt8, iOffset, nOffset, iLessEqualNOffset);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.SkipIfZero, iLessEqualNOffset, 0, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.Jump, cycleStart, 0, 0);
			WriteInstruction (buffer, ref offset, (ushort)InstructionCode.EndOfInstructions, factorialCycleCall, 0, 0);

			byte[] result = new byte[offset];
			Buffer.BlockCopy (buffer, 0, result, 0, result.Length);

			return result;
		}
	}
}

