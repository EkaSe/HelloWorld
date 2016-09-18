using System;

namespace HelloWorld.Miscellaneous
{
	public class SugarTest
	{
		static public bool TestEnoughSugar (){
			//Собираюсь на пикник, хочу взять с собой термос чая. 
			//Cколько мне в него грамм сахара засыпать? 
			//В кружку я обычно три кубика кладу, но сейчас под рукой нет рафинада, 
			//а хочу, чтобы чай был такой же по сладости.

			double userThermosVolume = 1.5; //Объем термоса (л)
			double userSugarMass = 55; //Масса имеющегося сахара (г)
			return Sugar.EnoughSugar (userThermosVolume, userSugarMass);
		}
	}
}

