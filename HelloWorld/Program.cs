using System;

namespace HelloWorld
{
	public class Point2D
	{
		public double X;
		public double Y;
	}

	public class Rectangle
	{
		public Point2D LeftBottom;
		public Point2D RightTop;
	}

	public class Circle
	{
		public Point2D Center;
		public double Radius;
	}


	class MainClass
	{
//___________________________________________________________________________
		static Func <double,double> sugarMassRequired = (thermosVolume) => {
			int piecesOfSugarForCup = 3; //Количество кубиков рафинада на кружку
			double cupVolume = 0.3; //Объем кружки (л)
			double pieceOfSugarMass = 5; //Масса кубика рафинада (г)

			return thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume;
		};

		static Func <double,double,bool> enoughSugar = (thermosVolume, sugarMass) => {
			if (sugarMassRequired(thermosVolume) > sugarMass)
				return false;
			else
				return true;
		};

//___________________________________________________________________________ 

		static Func <double,double,double,bool> between = (value, min, max) => {
			if (min > max)
				throw new Exception("min не может быть больше max");
			return ((value >= min) && (value <= max));
		};

		static Func <double, double, Point2D> constructPoint = (XCoordinate, YCoordinate) => {
			Point2D point = new Point2D ();
			point.X = XCoordinate;
			point.Y = YCoordinate;
			return point;
		};

		static Func <Point2D, Point2D, Rectangle> constructRectangle = (vertex1, vertex2) => {
			Rectangle target = new Rectangle ();
			target.LeftBottom = new Point2D ();
			target.RightTop = new Point2D ();
			target.LeftBottom.X = Math.Min(vertex1.X, vertex2.X);
			target.LeftBottom.Y = Math.Min(vertex1.Y, vertex2.Y);
			target.RightTop.X = Math.Max(vertex1.X, vertex2.X);
			target.RightTop.Y = Math.Max(vertex1.Y, vertex2.Y);
			return target;
		};

		static Func <Point2D, double, Circle> constructCircle = (center, radius) => {
			Circle target = new Circle ();
			target.Center = center;
			target.Radius = radius;
		};

		static Func <Rectangle, Point2D, bool> shotInRectangleTarget = 
			(target,shot) => {
			return (between(shot.X, target.LeftBottom.X, target.RightTop.X) && 
				between(shot.Y, target.LeftBottom.Y, target.RightTop.Y));
		};

		static Func <Point2D, Point2D, double> distanceBetweenPoints = (point1, point2) => {
			return Math.Sqrt (Math.Pow((point1.X - point2.X),2) + Math.Pow((point1.Y - point2.Y),2));
		};

		static Func <Circle, Point2D, bool> shotInRoundTarget = 
			(target, shot) => {
			return (distanceBetweenPoints(target.Center, shot) <= target.Radius);
		};

//___________________________________________________________________________ 

		static Action HiWorld = () => {
			Console.WriteLine ("Hello World!");
			Console.ReadKey ();
			int result = 0;
		};

		static Action QuadEq = () => {
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
	
		};
//___________________________________________________________________________ 

		static void Main (string[] args)
		{
			/*{
				//Собираюсь на пикник, хочу взять с собой термос чая. 
				//Cколько мне в него грамм сахара засыпать? 
				//В кружку я обычно три кубика кладу, но сейчас под рукой нет рафинада, 
				//а хочу, чтобы чай был такой же по сладости.

				double userThermosVolume = 1.5; //Объем термоса (л)
				double userSugarMass = 55; //Масса имеющегося сахара (г)
				bool result = enoughSugar (userThermosVolume, userSugarMass);
			}*/

			{
				//В двухмерной системе координат есть прямоугольная мишень, 
				//расположение и размеры которой заданы координатами противоположных углов.
				//И есть координаты места попадания выстрела. 
				//Нужно определить, пришлось ли попадание на мишень или нет.

				Point2D shotPoint = constructPoint (-1, 3);
				Point2D targetVertex1 = constructPoint (-3, 3);
				Point2D targetVertex2 = constructPoint (1, 5);
				Rectangle rectangleTarget = constructRectangle (targetVertex1, targetVertex2);
				bool resultRectangleTarget = shotInRectangleTarget (rectangleTarget,shotPoint);

				//Круглая мишень

				Circle roundTarget = constructCircle (constructPoint (0, 2), 3);
				bool resultRoundTarget = shotInRoundTarget (roundTarget, shotPoint);

				//T-мишень

				Rectangle TTargetPart1 = rectangleTarget;
				Rectangle TTargetPart2 = constructRectangle (constructPoint (0, 0),constructPoint (-2, 3));
				bool resultTTarget = shotInRectangleTarget (TTargetPart1, shotPoint) ||
				                     shotInRectangleTarget (TTargetPart2, shotPoint);
				
				//Мишень с отверстием

				Circle targetHole = constructCircle (constructPoint (2, 1), 2);
				bool resultTargetWithHole = shotInRoundTarget (roundTarget, shotPoint) &&
				                            !shotInRoundTarget (targetHole, shotPoint);
				
			}


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
