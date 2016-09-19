using System;

namespace HelloWorld.Algebra
{
	public class QuadraticEquation 
	{
		static public short Solve (double a, double b, double c, out double root1, out double root2) {
			//Дано: коэффициенты уравнения a*x^2+b*x+c=0
			//Найти: вещественные корни уравнения с заданными коэффициентами
			short NumberOfRealRoots = 0;
			root1 = 0;
			root2 = 0;
			//Решение: Определяем, какая формула будет использоваться, 
			//в зависимости от типа уравнения
			if (a != 0) { //Решение: определение формулы для квадратного уравнения
				double d;
				d = b * b - 4 * a * c;
				if (d >= 0) { // Решение: определение формулы в зависимости от числа вещественных корней
					root1 = (-b + Math.Sqrt (d)) / a / 2;
					root2 = (-b - Math.Sqrt (d)) / a / 2;
					if (d == 0)
						NumberOfRealRoots = 1;
					else
						NumberOfRealRoots = 2;
				} 
			} else {//Решение: определение формулы для линейного уравнения
				if (b != 0) {
					root1 = -c / b;
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
			return NumberOfRealRoots;
		}
	}

}

