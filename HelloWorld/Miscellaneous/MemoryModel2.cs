﻿using System;

namespace HelloWorld.Miscellaneous
{
	public class MemoryModel2
	{
		enum InstructionCode: ushort {EndOfInstructions, AssignUInt8, MultiplyUInt8, DivideUInt8, LessEqualUInt8, SkipIfZero, Jump};

		static public ushort WriteBytesToArray (byte[] array, ushort value, ushort startOffset) {
			array [startOffset++] = BitConverter.GetBytes (value) [0];
			array [startOffset++] = BitConverter.GetBytes (value) [1];;
			return startOffset;
		}

		static public void AssignUInt8 (byte[] memory, ushort offset, byte value) {
			memory [offset] = value;
		}

		static public void MultiplyUInt8 (byte[] memory, ushort bOffset, ushort cOffset, ushort aOffset) {
			memory [aOffset] = (byte) (memory [bOffset] * memory [cOffset]);
		}

		static public void DivideUInt8 (byte[] memory, ushort bOffset, ushort cOffset, ushort aOffset) { 
			memory [aOffset] = (byte) (memory [bOffset] / memory [cOffset]);
		}

		static public void LessEqualUInt8 (byte[] memory, ushort bOffset, ushort cOffset, ushort resultOffset) { 
			if (memory [bOffset] <= memory [cOffset])
				memory [resultOffset] = 1;
			else
				memory [resultOffset] = 0;
		}

		static public ushort SkipIfZero (byte[] memory, ushort valueOffset, ushort currentInstructionOffset) {
			if (memory [valueOffset] == 0)
				return (ushort) (currentInstructionOffset + 16);
			else
				return (ushort) (currentInstructionOffset + 8);
		}

		static public ushort Jump (ushort offset){
			return offset;
		}

		static public void EndOfInstructions () {}

		static public ushort ProcessInstruction (byte[] instructions, byte[] memory, ushort currentInstructionOffset){
			ushort arg1, arg2, arg3;
			ushort currentInstruction = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
			currentInstructionOffset += 2;
			switch (currentInstruction) {
			case (ushort) InstructionCode.AssignUInt8:
				arg1 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				AssignUInt8 (memory, arg1, instructions [currentInstructionOffset++]);
				currentInstructionOffset += 3;
				break;
			case (ushort) InstructionCode.MultiplyUInt8:
				arg1 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				MultiplyUInt8 (memory, arg1, arg2, arg3);
				break;
			case (ushort) InstructionCode.DivideUInt8:
				arg1 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				DivideUInt8 (memory, arg1, arg2, arg3);
				break;
			case (ushort) InstructionCode.LessEqualUInt8:
				arg1 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset += 2;
				LessEqualUInt8 (memory, arg1, arg2, arg3);
				break;
			case (ushort) InstructionCode.SkipIfZero:
				arg1 = BitConverter.ToUInt16 (instructions, currentInstructionOffset); 
				currentInstructionOffset = SkipIfZero (memory, arg1, (ushort) (currentInstructionOffset - 2));
				break;
			case (ushort) InstructionCode.Jump:
				arg1 = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
				currentInstructionOffset = Jump (arg1);
				break;
			case (ushort) InstructionCode.EndOfInstructions:
				currentInstructionOffset = 0;
				break;
			default:
				throw new Exception("Invalid instruction code");
				break;
			}
			return currentInstructionOffset;
		}

		static public void RunProgram_SugarReqiure () {
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
			ushort instructionsLength = 255;
			byte[] instructions = new byte[instructionsLength];
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.AssignUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, thermosVolumeOffset, currentInstructionOffset);
			instructions [currentInstructionOffset++] = 15;
			currentInstructionOffset += 3;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.AssignUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, cupVolumeOffset, currentInstructionOffset);
			instructions [currentInstructionOffset++] = 3;
			currentInstructionOffset += 3;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.AssignUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, pieceOfSugarMassOffset, currentInstructionOffset);
			instructions [currentInstructionOffset++] = 5;
			currentInstructionOffset += 3;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.AssignUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, pieceOfSugarForCupOffset, currentInstructionOffset);
			instructions [currentInstructionOffset++] = 3;
			currentInstructionOffset += 3;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.AssignUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, sugarMassAvailableOffset, currentInstructionOffset);
			instructions [currentInstructionOffset++] = 55;
			currentInstructionOffset += 3;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.MultiplyUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, pieceOfSugarForCupOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, pieceOfSugarMassOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, sugarMassforCupOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.DivideUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, thermosVolumeOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, cupVolumeOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, cupsInThermosOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.MultiplyUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, sugarMassforCupOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, cupsInThermosOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, sugarMassRequiredOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.LessEqualUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, sugarMassRequiredOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, sugarMassAvailableOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, isEnoughSugarOffset, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.SkipIfZero, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, isEnoughSugarOffset, currentInstructionOffset);
			currentInstructionOffset += 4;
			ushort ifNotZeroOffset = currentInstructionOffset;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.Jump, currentInstructionOffset);
			currentInstructionOffset += 6;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.AssignUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, resultOffset, currentInstructionOffset);
			instructions [currentInstructionOffset++] = 0;
			currentInstructionOffset += 3;
			ushort ifZeroOffset = currentInstructionOffset;
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.Jump, currentInstructionOffset);
			currentInstructionOffset += 6;
			ifNotZeroOffset = WriteBytesToArray (instructions, (ushort) currentInstructionOffset, (ushort) (ifNotZeroOffset + 2));
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.AssignUInt8, currentInstructionOffset);
			currentInstructionOffset = WriteBytesToArray (instructions, resultOffset, currentInstructionOffset);
			instructions [currentInstructionOffset++] = 1;
			currentInstructionOffset += 3;
			ifZeroOffset = WriteBytesToArray (instructions, (ushort) currentInstructionOffset, (ushort) (ifZeroOffset + 2));
			currentInstructionOffset = WriteBytesToArray (instructions, (ushort) InstructionCode.EndOfInstructions, currentInstructionOffset);

			//Загрузка
			byte[] memory = new byte[currentOffset];
			//Выполнение
			currentInstructionOffset = 0;
			do
				currentInstructionOffset = ProcessInstruction (instructions, memory, currentInstructionOffset);
			while (currentInstructionOffset > 0);
		}
	}
}
