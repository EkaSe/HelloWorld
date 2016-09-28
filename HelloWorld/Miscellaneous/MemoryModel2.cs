using System;

namespace HelloWorld.Miscellaneous
{
	public class MemoryModel2
	{
		enum InstructionCode: ushort {
			EndOfInstructions, 
			AssignUInt8Const, 
			AssignUInt8Var, 
			MultiplyUInt8, 
			DivideUInt8, 
			LessEqualUInt8, 
			SkipIfZero, 
			Jump
		};

		static public Func<ushort, byte[], ushort, ushort, ushort, ushort>[] instructionsList = 
			new Func<ushort, byte[], ushort, ushort, ushort, ushort>[8]; 

		static public ushort WriteBytesToArray (byte[] array, ushort value, ushort startOffset) {
			array [startOffset++] = BitConverter.GetBytes (value) [0];
			array [startOffset++] = BitConverter.GetBytes (value) [1];;
			return startOffset;
		}

		static public ushort AssignUInt8Const (ushort instructionOffset, byte[] memory, ushort offset, ushort value, ushort unused) {
			memory [offset] = BitConverter.GetBytes (value) [0];
			return (ushort) (instructionOffset + 8);
		}

		static public ushort AssignUInt8Var (ushort instructionOffset, byte[] memory, ushort destinationOffset, ushort sourceOffset, ushort unused) {
			memory [destinationOffset] = memory [sourceOffset];
			return (ushort) (instructionOffset + 8);
		}

		static public ushort MultiplyUInt8 (ushort instructionOffset, byte[] memory, ushort bOffset, ushort cOffset, ushort aOffset) {
			memory [aOffset] = (byte) (memory [bOffset] * memory [cOffset]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort DivideUInt8 (ushort instructionOffset, byte[] memory, ushort bOffset, ushort cOffset, ushort aOffset) { 
			memory [aOffset] = (byte) (memory [bOffset] / memory [cOffset]);
			return (ushort) (instructionOffset + 8);
		}
	
		static public ushort LessEqualUInt8 (ushort instructionOffset, byte[] memory, ushort bOffset, ushort cOffset, ushort resultOffset) { 
			if (memory [bOffset] <= memory [cOffset])
				memory [resultOffset] = 1;
			else
				memory [resultOffset] = 0;
			return (ushort) (instructionOffset + 8);
		}

		static public ushort SkipIfZero (ushort instructionOffset, byte[] memory, ushort valueOffset, ushort unused1, ushort unused2) {
			if (memory [valueOffset] == 0)
				return (ushort) (instructionOffset + 16);
			else
				return (ushort) (instructionOffset + 8);
		}

		static public ushort Jump (ushort currentInstructionOffset, byte[] memory, ushort instructionOffset, ushort unused1, ushort unused2){
			return (ushort) instructionOffset;
		}

		static public ushort EndOfInstructions (ushort instructionOffset, byte[] unused, ushort unused1, ushort unused2, ushort unused3) {
			return 0;
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

		static public ushort ProcessInstruction (byte[] instructions, byte[] memory, ushort currentInstructionOffset){
			instructionsList [(int)InstructionCode.EndOfInstructions] = EndOfInstructions;
			instructionsList [(int)InstructionCode.AssignUInt8Const] = AssignUInt8Const;
			instructionsList [(int)InstructionCode.AssignUInt8Var] = AssignUInt8Var; 
			instructionsList [(int)InstructionCode.MultiplyUInt8] = MultiplyUInt8; 
			instructionsList [(int)InstructionCode.DivideUInt8] = DivideUInt8;
			instructionsList [(int)InstructionCode.LessEqualUInt8] = LessEqualUInt8; 
			instructionsList [(int)InstructionCode.SkipIfZero] = SkipIfZero; 
			instructionsList [(int)InstructionCode.Jump] = Jump;
			ushort currentInstruction = BitConverter.ToUInt16 (instructions, currentInstructionOffset);
			ushort arg1 = BitConverter.ToUInt16 (instructions, (currentInstructionOffset + 2));
			ushort arg2 = BitConverter.ToUInt16 (instructions, (currentInstructionOffset + 4));
			ushort arg3 = BitConverter.ToUInt16 (instructions, (currentInstructionOffset + 6));
			if (currentInstruction >= 8) {
				throw new Exception ("Invalid instruction code");
			};
			currentInstructionOffset = instructionsList [currentInstruction] (currentInstructionOffset, memory, arg1, arg2, arg3);
			return currentInstructionOffset;
		}

		static public void RunProgram () {
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
						
			ushort currentInstructionOffset = 0;
			ushort instructionsLength = 255;
			byte[] instructions = new byte[instructionsLength];
			ushort memorySize = 0;
			WriteInstructionsSugar(instructions, out memorySize);
			//WriteInstructionsOrderNumbers (instructions, out memorySize);

			//Загрузка
			byte[] memory = new byte[memorySize];
			//Выполнение
			currentInstructionOffset = 0;
			do {
				currentInstructionOffset = ProcessInstruction (instructions, memory, currentInstructionOffset);
			} while (currentInstructionOffset > 0);
		}
	}
}

