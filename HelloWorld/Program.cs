using System;

namespace HelloWorld
{
	class MainClass
	{
		static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			Console.ReadKey();

			//Дано: коэффициенты уравнения a*x^2+b*x+c=0
			double a, b, c; 
			a = 1.0;
			b = 2;
			c = 1;
			//Найти: вещественные корни уравнения с заданными коэффициентами
			short NumberOfRealRoots = 0;
			double root1, root2;
			/*Решение: Определяем, какая формула будет использоваться, 
			 * в зависимости от типа уравнения*/
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
			//Ответ: где-то здесь он мог бы быть - найденные корни уравнения или их отсутствие
		}
	}
}
