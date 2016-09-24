using System;

namespace HelloWorld.Miscellaneous
{
	public class MemoryModel
	{
		enum InstructionCode: byte {AssignUInt8, MultiplyUInt8, DivideUInt8, LessEqualUInt8, JumpIfNotZero, Jump};

		static public void AssignUInt8 (byte[] memory, ushort offset, byte value) {
			memory [offset] = value;
		}//0 (4)

		static public void MultiplyUInt8 (byte[] memory, ushort bOffset, ushort cOffset, ushort aOffset) {
			memory [aOffset] = (byte) (memory [bOffset] * memory [cOffset]);
		}//1 (7)

		static public void DivideUInt8 (byte[] memory, ushort bOffset, ushort cOffset, ushort aOffset) { 
			memory [aOffset] = (byte) (memory [bOffset] / memory [cOffset]);
		}//2 (7)

		static public void LessEqualUInt8 (byte[] memory, ushort bOffset, ushort cOffset, ushort resultOffset) { 
			if (memory [bOffset] <= memory [cOffset])
				memory [resultOffset] = 1;
			else
				memory [resultOffset] = 0;
		}//3 (7)

		static public ushort JumpIfNotZero (byte[] memory, ushort valueOffset, ushort ifNullOffset, 
			ushort ifNotNullOffset) {
			if (memory [valueOffset] == 0)
				return ifNullOffset;
			else
				return ifNotNullOffset;
		}

		static public ushort Jump (ushort offset){
			return offset;
		}

		static public ushort ProcessInstruction (byte[] instructions, byte[] memory, ushort currentInstruction){
			ushort arg1, arg2, arg3;
			switch (instructions [currentInstruction]) {
			case (byte) InstructionCode.AssignUInt8:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				AssignUInt8 (memory, arg1, instructions [currentInstruction++]);
				break;
			case (byte) InstructionCode.MultiplyUInt8:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				MultiplyUInt8 (memory, arg1, arg2, arg3);
				break;
			case (byte) InstructionCode.DivideUInt8:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				DivideUInt8 (memory, arg1, arg2, arg3);
				break;
			case (byte) InstructionCode.LessEqualUInt8:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				LessEqualUInt8 (memory, arg1, arg2, arg3);
				break;
			case (byte) InstructionCode.JumpIfNotZero:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction); 
				currentInstruction += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction = JumpIfNotZero (memory, arg1, arg2, arg3);
				break;
			case (byte) InstructionCode.Jump:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction = Jump (arg1);
				break;
			default:
				throw new Exception("Invalid instruction code");
				break;
			}
			return currentInstruction;
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
			//Загрузка
			byte[] memory = new byte[currentOffset];
			ushort currentInstructionOffset = 0;
			ushort instructionsLength = 255;
			byte[] instructions = new byte[instructionsLength];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [1];
			instructions [currentInstructionOffset++] = 15;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [1];
			instructions [currentInstructionOffset++] = 3;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [1];
			instructions [currentInstructionOffset++] = 5;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [1];
			instructions [currentInstructionOffset++] = 3;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [1];
			instructions [currentInstructionOffset++] = 55;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.MultiplyUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.DivideUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.MultiplyUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.LessEqualUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (isEnoughSugarOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (isEnoughSugarOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.JumpIfNotZero;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (isEnoughSugarOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (isEnoughSugarOffset) [1];
			ushort ifNotZeroOffset = currentInstructionOffset;
			instructions [ifNotZeroOffset] = BitConverter.GetBytes (ifNotZeroOffset + 4) [0];
			instructions [ifNotZeroOffset + 1] = BitConverter.GetBytes (ifNotZeroOffset + 4) [1];
			currentInstructionOffset += 4;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [1];
			instructions [currentInstructionOffset++] = 1;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.Jump;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (instructionsLength) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (instructionsLength) [1];
			instructions [ifNotZeroOffset + 2] = BitConverter.GetBytes (currentInstructionOffset) [0];
			instructions [ifNotZeroOffset + 3] = BitConverter.GetBytes (currentInstructionOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [1];
			instructions [currentInstructionOffset++] = 1;
			instructionsLength = currentInstructionOffset;

			//Выполнение
			currentInstructionOffset = 0;
			while (currentInstructionOffset < instructionsLength)
				currentInstructionOffset = ProcessInstruction (instructions, memory, currentInstructionOffset);
			/*
			AssignUInt8 (memory, thermosVolumeOffset, 15); //thermosVolume = 1.5l
			AssignUInt8 (memory, cupVolumeOffset, 3); //cupVolume = 0.3
			AssignUInt8 (memory, pieceOfSugarMassOffset, 5); //pieceOfSugarMass = 5g
			AssignUInt8 (memory, pieceOfSugarForCupOffset, 3); //piecesOfSugarForCup = 3
			AssignUInt8 (memory, sugarMassAvailableOffset, 55); //sugarMassAvailable = 55g
			MultiplyUInt8 (memory, pieceOfSugarForCupOffset, pieceOfSugarMassOffset, sugarMassforCupOffset);
			DivideUInt8 (memory, thermosVolumeOffset, cupVolumeOffset, cupsInThermosOffset);
			MultiplyUInt8 (memory, sugarMassforCupOffset, cupsInThermosOffset, sugarMassRequiredOffset);
			LessEqualUInt8 (memory, sugarMassRequiredOffset, sugarMassAvailableOffset, isEnoughSugarOffset);
			*/
		}
	}
}

