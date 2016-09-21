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
			 static public double SugarMassRequired (double thermosVolume) {
				int piecesOfSugarForCup = 3; //Количество кубиков рафинада на кружку
				double cupVolume = 0.3; //Объем кружки (л)
				double pieceOfSugarMass = 5; //Масса кубика рафинада (г)
				return (thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume);
			}

			static public bool EnoughSugar (double thermosVolume, double sugarMass) {
				return (SugarMassRequired(thermosVolume) <= sugarMass);
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

			byte[] memory = new byte[currentOffset];

			//Запуск
			AssignUInt8 (memory, thermosVolumeOffset, 70); //thermosVolume = 1.5l; max = 5l
			AssignUInt8 (memory, cupVolumeOffset, 15); //cupVolume = 0.3l; max = 5l
			AssignUInt8 (memory, pieceOfSugarMassOffset, 1); //pieceOfSugarMass = 4g; max = 1000g
			AssignUInt8 (memory, pieceOfSugarForCupOffset, 3); //piecesOfSugarForCup = 3; max = 255
			AssignUInt8 (memory, sugarMassAvailableOffset, 14); //sugarMassAvailable = 55g; max = 1000g
			MultiplyUInt8 (memory, pieceOfSugarForCupOffset, pieceOfSugarMassOffset, sugarMassforCupOffset);
			DivideUInt8 (memory, thermosVolumeOffset, cupVolumeOffset, cupsInThermosOffset);
			MultiplyUInt8 (memory, sugarMassforCupOffset, cupsInThermosOffset, sugarMassRequiredOffset);
			LessEqualUInt8 (memory, sugarMassRequiredOffset, sugarMassAvailableOffset, isEnoughSugarOffset);
		}
	}
}

