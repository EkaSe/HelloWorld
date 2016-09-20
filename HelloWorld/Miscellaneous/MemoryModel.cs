using System;

namespace HelloWorld.MemoryModel
{
	public class MemoryModel
	{

		static public void RunProgram_SugarReqiure () {
			double[] fakeMemory = new double[7];
			fakeMemory [0] = 1.5; //thermosVolume
			fakeMemory [1] = 0.3; //cupVolume
			fakeMemory [2] = 5; //pieceOfSugarMass
			fakeMemory [3] = 3; //piecesOfSugarForCup
			fakeMemory [4] = 55; //sugarMassAvailable
			//SugarMassREquired = thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume
			fakeMemory[5] = fakeMemory [0] * fakeMemory [3] * fakeMemory [2] / fakeMemory [1];
			//if (SugarMassRequired(thermosVolume) <= sugarMass) result = 1;
			if (fakeMemory [5] <= fakeMemory [4])
				fakeMemory [6] = 1;
			else
				fakeMemory [6] = 0;
		}
	}
}

