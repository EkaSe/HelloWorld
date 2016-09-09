using System;

namespace HelloWorld
{
	class MainClass
	{
		static void Main (string[] args)
		{

			{
				/*Собираюсь на пикник, хочу взять с собой термос чая. 
			Термос 0,5 литра, сколько мне в него грамм сахара засыпать? 
			В кружку я обычно три кубика кладу, но сейчас под рукой нет рафинада, 
			а хочу, чтобы чай был такой же по сладости.*/

				//Дано 
				double VTer = 0.5; //Объем термоса (л)
				int NCup = 3; //Количество кубиков рафинада на кружку

				double VCup = 0.3; //Объем кружки (л)
				double MRaf = 5; //Масса кубика рафинада (г)

				//Найти
				double MSug; //Масса сахара в термосе (г)

				//Решение
				MSug = VTer * NCup * MRaf / VCup;

				//Ответ
				double result = MSug;
			}

			{
				Console.WriteLine ("Hello World!");
				//Console.ReadKey ();
				int result = 0;
			}

			{
				//Дано: коэффициенты уравнения a*x^2+b*x+c=0
				double a, b, c; 
				a = 1;
				b = 2;
				c = 1;
				//Найти: вещественные корни уравнения с заданными коэффициентами
				short NumberOfRealRoots = 0;
				double root1 = 0;
				double root2 = 0;
				//Решение: Определяем, какая формула будет использоваться, 
				//в зависимости от типа уравнения
				if (a != 0) { //Решение: определение формулы для квадратного уравнения
					double d;
					d = b * b - 4 * a * c;
					if (d >= 0) { // Решение: определение формулы в зависимости от числа вещественных корней
						root1 = (-b + Math.Sqrt (d)) / a / 2;//вычисление
						root2 = (-b - Math.Sqrt (d)) / a / 2;//вычисление
						if (d == 0)
							NumberOfRealRoots = 1;
						else
							NumberOfRealRoots = 2;
					} 
				} else {//Решение: определение формулы для линейного уравнения
					if (b != 0) {
						root1 = -c / b;//Вычисление
						NumberOfRealRoots = 1;
					}
				}
				//Ответ
				double result = NumberOfRealRoots;
				double result1, result2;
				if (result == 1)
					result1 = root1;
				if (result == 2) {
					result1 = root1;
					result2 = root2;
				}
				
			}

		}
	}
}
