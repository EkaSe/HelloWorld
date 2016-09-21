using System;

namespace HelloWorld.Miscellaneous
{
	public class MemoryModel
	{
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
			//Выполнение
			AssignUInt8 (memory, thermosVolumeOffset, 15); //thermosVolume = 1.5l
			AssignUInt8 (memory, cupVolumeOffset, 3); //cupVolume = 0.3
			AssignUInt8 (memory, pieceOfSugarMassOffset, 5); //pieceOfSugarMass = 5g
			AssignUInt8 (memory, pieceOfSugarForCupOffset, 3); //piecesOfSugarForCup = 3
			AssignUInt8 (memory, sugarMassAvailableOffset, 55); //sugarMassAvailable = 55g
			MultiplyUInt8 (memory, pieceOfSugarForCupOffset, pieceOfSugarMassOffset, sugarMassforCupOffset);
			DivideUInt8 (memory, thermosVolumeOffset, cupVolumeOffset, cupsInThermosOffset);
			MultiplyUInt8 (memory, sugarMassforCupOffset, cupsInThermosOffset, sugarMassRequiredOffset);
			LessEqualUInt8 (memory, sugarMassRequiredOffset, sugarMassAvailableOffset, isEnoughSugarOffset);
			if (memory [isEnoughSugarOffset] == 1)
				AssignUInt8 (memory, resultOffset, 1);
			else
				AssignUInt8 (memory, resultOffset, 0);
		}
	}
}

