using System;

namespace HelloWorld
{
	public class Point2D
	{
		public double Xpoint;
		public double Ypoint;
	}

	public class Rectangle
	{
		public double X1;
		public double X2;
		public double Y1;
		public double Y2;
	}



	class MainClass
	{
		static void Main (string[] args)
		{
			/*{

				//Собираюсь на пикник, хочу взять с собой термос чая. 
				//Cколько мне в него грамм сахара засыпать? 
				//В кружку я обычно три кубика кладу, но сейчас под рукой нет рафинада, 
				//а хочу, чтобы чай был такой же по сладости.
				Func<double,double> sugarMassRequired = (thermosVolume) => {
					int piecesOfSugarForCup = 3; //Количество кубиков рафинада на кружку
					double cupVolume = 0.3; //Объем кружки (л)
					double pieceOfSugarMass = 5; //Масса кубика рафинада (г)

					return thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume;
				};

				Func<double,double,bool> enoughSugar = (thermosVolume, sugarMass) => {
					if (sugarMassRequired(thermosVolume) > sugarMass)
						return false;
					else
						return true;
				};

				double userThermosVolume = 1.5; //Объем термоса (л)
				double userSugarMass = 55; //Масса имеющегося сахара (г)
				bool result = enoughSugar (userThermosVolume, userSugarMass);
			}*/

			{
				//В двухмерной системе координат есть прямоугольная мишень, 
				//расположение и размеры которой заданы координатами противоположных углов.
				//И есть координаты места попадания выстрела. 
				//Нужно определить, пришлось ли попадание на мишень или нет.
				Func<double,double,double,bool> between = (value, min, max) => {
					if (min > max) {
						double temp = min;
						min = max;
						max = temp;
					}
					if ((value > min) && (value < max))
						return true;
					else return false;
				};

				Func<Rectangle, Point2D, bool> shotInTarget = 
					(target,shot) => {
					if (between(shot.Xpoint, target.X1, target.X2) && 
						between(shot.Ypoint, target.Y1, target.Y2) ) 
						return true;
					else return false;
				};

				Rectangle userTarget = new Rectangle();
				userTarget.X1 = 0;
				userTarget.Y1 = 2;
				userTarget.X2 = -2;
				userTarget.Y2 = 4;
				Point2D shotPoint = new Point2D ();
				shotPoint.Xpoint = -1;
				shotPoint.Ypoint = 3;
				bool result = shotInTarget (userTarget,shotPoint);
			}

			/*Action HiWorld = () => {
				Console.WriteLine ("Hello World!");
				//Console.ReadKey ();
				int result = 0;
			};

			Action QuadEq = () => {
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
				
			};*/
			/*int iAct = 1;
			Action ComboAction = () => Console.WriteLine("Empty Action");
			ComboAction = () => {
				QuadEq ();
				sugar ();
				if (iAct == 1) {
					iAct = 0;
					ComboAction ();
				} else {
					HiWorld ();
				}
			};
			ComboAction ();
			ComboAction ();*/
		}
	}
}
