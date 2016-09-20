using System;

namespace HelloWorld.Miscellaneous
{
	public class MemoryModel
	{

		static public void RunProgram_SugarReqiure () {
			ushort currentOffset = 0;
			ushort thermosVolumeOffset = currentOffset++;
			ushort cupVolumeOffset = currentOffset++;
			ushort pieceOfSugarMassOffset = currentOffset++;
			ushort pieceOfSugarForCupOffset = currentOffset++;
			ushort sugarMassAvailableOffset = currentOffset++;
			ushort sugarMassRequiredOffset = currentOffset++;
			ushort isEnoughSugarOffset = currentOffset++;

			byte[] fakeMemory = new byte[currentOffset];
			fakeMemory [thermosVolumeOffset] = 70; //thermosVolume = 1.5l; max = 5l
			fakeMemory [cupVolumeOffset] = 15; //cupVolume = 0.3l; max = 5l
			fakeMemory [pieceOfSugarMassOffset] = 1; //pieceOfSugarMass = 4g; max = 1000g
			fakeMemory [pieceOfSugarForCupOffset] = 3; //piecesOfSugarForCup = 3; max = 255
			fakeMemory [sugarMassAvailableOffset] = 14; //sugarMassAvailable = 55g; max = 1000g
			fakeMemory[sugarMassRequiredOffset] = (byte) (fakeMemory [thermosVolumeOffset] / 
				fakeMemory [cupVolumeOffset] * fakeMemory [pieceOfSugarForCupOffset] * 
				fakeMemory [pieceOfSugarMassOffset]);
			//if (SugarMassRequired(thermosVolume) <= sugarMass) result = 1;
			if (fakeMemory [sugarMassRequiredOffset] <= fakeMemory [sugarMassAvailableOffset])
				fakeMemory [isEnoughSugarOffset] = 1;
			else
				fakeMemory [isEnoughSugarOffset] = 0;
		}
	}
}

