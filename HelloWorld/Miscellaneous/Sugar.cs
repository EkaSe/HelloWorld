using System;

namespace HelloWorld.Miscellaneous
{
	public class Sugar
	{
		static public double SugarMassRequired (double thermosVolume) {
			int piecesOfSugarForCup = 3; //Количество кубиков рафинада на кружку
			double cupVolume = 0.3; //Объем кружки (л)
			double pieceOfSugarMass = 5; //Масса кубика рафинада (г)
			return (thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume);
		}

		static public bool EnoughSugar (double thermosVolume, double sugarMass) {
			return (SugarMassRequired(thermosVolume) <= sugarMass);
		}
	}
}

