using System;

namespace HelloWorld.Miscellaneous
{
	public class MemoryModel
	{
		enum InstructionCode: byte {AssignUInt8Code, MultiplyUInt8Code, DivideUInt8Code, LessEqualUInt8Code};
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

		static public ushort ProcessInstruction (byte[] instructions, byte[] memory, ushort currentInstruction){
			ushort arg1, arg2, arg3;
			switch (instructions [currentInstruction]) {
			case (byte) InstructionCode.AssignUInt8Code:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				AssignUInt8 (memory, arg1, instructions [currentInstruction++]);
				break;
			case (byte) InstructionCode.MultiplyUInt8Code:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				MultiplyUInt8 (memory, arg1, arg2, arg3);
				break;
			case (byte) InstructionCode.DivideUInt8Code:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				DivideUInt8 (memory, arg1, arg2, arg3);
				break;
			case (byte) InstructionCode.LessEqualUInt8Code:
				currentInstruction++;
				arg1 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg2 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				arg3 = BitConverter.ToUInt16 (instructions, currentInstruction);
				currentInstruction += 2;
				LessEqualUInt8 (memory, arg1, arg2, arg3);
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
			ushort instructionsLength = 56;
			byte[] instructions = new byte[instructionsLength];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [1];
			instructions [currentInstructionOffset++] = 15;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [1];
			instructions [currentInstructionOffset++] = 3;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [1];
			instructions [currentInstructionOffset++] = 5;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [1];
			instructions [currentInstructionOffset++] = 3;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [1];
			instructions [currentInstructionOffset++] = 55;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.MultiplyUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarForCupOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (pieceOfSugarMassOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.DivideUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (thermosVolumeOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupVolumeOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.MultiplyUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassforCupOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (cupsInThermosOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.LessEqualUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassRequiredOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (sugarMassAvailableOffset) [1];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (isEnoughSugarOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (isEnoughSugarOffset) [1];
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [1];
			instructions [currentInstructionOffset++] = 0;
			instructions [currentInstructionOffset++] = (byte) InstructionCode.AssignUInt8Code;
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [0];
			instructions [currentInstructionOffset++] = BitConverter.GetBytes (resultOffset) [1];
			instructions [currentInstructionOffset++] = 1;
			//Выполнение
			currentInstructionOffset = 0;
			while (currentInstructionOffset < (instructionsLength - 8))
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
			if (memory [isEnoughSugarOffset] == 1)
				currentInstructionOffset = ProcessInstruction (instructions, memory, currentInstructionOffset);
			else {
				currentInstructionOffset = ProcessInstruction (instructions, memory, currentInstructionOffset);
				currentInstructionOffset = ProcessInstruction (instructions, memory, currentInstructionOffset);
			}
		}
	}
}

