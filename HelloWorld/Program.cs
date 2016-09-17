using System;

namespace HelloWorld
{
	public class Miscellaneous
	{
		static public void HiWorld () {
			Console.WriteLine ("Hello World!");
			Console.ReadKey ();
		}

		static public double SugarMassRequired (double thermosVolume) {
			int piecesOfSugarForCup = 3; //Количество кубиков рафинада на кружку
			double cupVolume = 0.3; //Объем кружки (л)
			double pieceOfSugarMass = 5; //Масса кубика рафинада (г)
			return (thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume);
		}

		static public bool EnoughSugar (double thermosVolume, double sugarMass) {
			return (SugarMassRequired(thermosVolume) <= sugarMass);
		}

		static public bool TestSugar (){
				//Собираюсь на пикник, хочу взять с собой термос чая. 
				//Cколько мне в него грамм сахара засыпать? 
				//В кружку я обычно три кубика кладу, но сейчас под рукой нет рафинада, 
				//а хочу, чтобы чай был такой же по сладости.

				double userThermosVolume = 1.5; //Объем термоса (л)
				double userSugarMass = 55; //Масса имеющегося сахара (г)
				return EnoughSugar (userThermosVolume, userSugarMass);
		}
	}

	public class Algebra 
	{
		static public short QuadEq (double a, double b, double c) {
			//Дано: коэффициенты уравнения a*x^2+b*x+c=0
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

		static public void TestQuadEq () {
			double a, b, c; 
			a = 1;
			b = 2;
			c = 1;
			short result = QuadEq (a, b, c);
		}
	}

	public class Point2D
	{
		public double X;
		public double Y;
		static public Point2D Construct (double XCoordinate, double YCoordinate) {
			Point2D point = new Point2D ();
			point.X = XCoordinate;
			point.Y = YCoordinate;
			return point;
		}
		static public double CalculateDistance (Point2D point1, Point2D point2) {
			return Math.Sqrt (Math.Pow((point1.X - point2.X),2) + Math.Pow((point1.Y - point2.Y),2));
		}
	}
	
	public class Rectangle
	{
		public Point2D LeftBottom;
		public Point2D RightTop;

		static public Rectangle Construct (Point2D vertex1, Point2D vertex2){
			Rectangle target = new Rectangle ();
			target.LeftBottom = new Point2D ();
			target.RightTop = new Point2D ();
			target.LeftBottom.X = Math.Min(vertex1.X, vertex2.X);
			target.LeftBottom.Y = Math.Min(vertex1.Y, vertex2.Y);
			target.RightTop.X = Math.Max(vertex1.X, vertex2.X);
			target.RightTop.Y = Math.Max(vertex1.Y, vertex2.Y);
			return target;
		}
	}

	public class Circle
	{
		public Point2D Center;
		public double Radius;

		static public Circle Construct (Point2D center, double radius) {
			Circle target = new Circle ();
			target.Center = center;
			target.Radius = radius;
			return target;
		}
	}

	public class HitTest
	{
		static public bool Between (double value, double min, double max) {
			if (min > max)
				throw new Exception("min не может быть больше max");
			return ((value >= min) && (value <= max));
		}

		static public bool ShotInRectangleTarget (Rectangle target, Point2D shot) {
			return (Between(shot.X, target.LeftBottom.X, target.RightTop.X) && 
				Between(shot.Y, target.LeftBottom.Y, target.RightTop.Y));
		}

		static public bool ShotInRoundTarget (Circle target, Point2D shot) {
			return (Point2D.CalculateDistance(target.Center, shot) <= target.Radius);
		}

		static public void TestHitTests (){
			//В двухмерной системе координат есть прямоугольная мишень, 
			//расположение и размеры которой заданы координатами противоположных углов.
			//И есть координаты места попадания выстрела. 
			//Нужно определить, пришлось ли попадание на мишень или нет.

			Point2D shotPoint = Point2D.Construct (-1,3);
			Point2D targetVertex1 = Point2D.Construct (-3, 3);
			Point2D targetVertex2 = Point2D.Construct (1, 5);
			Rectangle rectangleTarget = Rectangle.Construct (targetVertex1, targetVertex2);
			bool resultRectangleTarget = ShotInRectangleTarget (rectangleTarget,shotPoint);

			//Круглая мишень

			Circle roundTarget = Circle.Construct (Point2D.Construct (0, 2), 3);
			bool resultRoundTarget = ShotInRoundTarget (roundTarget, shotPoint);

			//T-мишень

			Rectangle TTargetPart1 = rectangleTarget;
			Rectangle TTargetPart2 = Rectangle.Construct (Point2D.Construct (0, 0), Point2D.Construct (-2, 3));
			bool resultTTarget = ShotInRectangleTarget (TTargetPart1, shotPoint) ||
				HitTest.ShotInRectangleTarget (TTargetPart2, shotPoint);

			//Мишень с отверстием

			Circle targetHole = Circle.Construct (Point2D.Construct (2, 1), 2);
			bool resultTargetWithHole = ShotInRoundTarget (roundTarget, shotPoint) &&
				!ShotInRoundTarget (targetHole, shotPoint);
		}
			
	}

	public class LoopExamples 
	{
		static double doubleEpsilon = 1E-7;
		static public int Multiplication (int multiplier1, int multiplier2 ) {
			int result = 0;
			if (multiplier2 > 0) {
				for (int i = 1; i <= multiplier2; i++)
					result = result + multiplier1;
			} else {
				for (int i = 1; i <= (-multiplier2); i++)
					result = result - multiplier1;
			};
			return result;			
		}
		static public int Factorial (int argument) {
			int result = 1;
			for (int i = 1; i <= argument; i++)
				result = result * i;
			return result;
		}
		//partial sum of 1/n^2 series
		static public double HyperharmonicSeries (int numberOfElements){
			double result = 0;
			for (int i = 1; i <= numberOfElements; i++)
				result = result + 1 / (i * i);
			return result;
		}

		static public void TestMultiplication (int multiplier1, int multiplier2, int expectedResult){
			if (Multiplication (multiplier1, multiplier2) == expectedResult)
				Console.WriteLine ("Multiplication test: " + multiplier1 + " * " + multiplier2 + " = " +
					expectedResult + " passed");
			else Console.WriteLine ("Multiplication test: " + multiplier1 + " * " + multiplier2 + " = " +
				expectedResult + " failed");
		}
		static public void TestFactorial (int argument, int expectedResult){
			if (Factorial (argument) == expectedResult)
				Console.WriteLine ("Factorial test: " + argument + "! = " + expectedResult + " passed");
			else 
				Console.WriteLine ("Factorial test: " + argument + "! = " + expectedResult + " failed");
		}
		static public void TestHyperharmonicSeries (int numberOfElements, double expectedResult){
			if ((HyperharmonicSeries (numberOfElements) - expectedResult) < doubleEpsilon)
				Console.WriteLine ("Hyperharmonic series test: partial sum of the first " + 
					numberOfElements + " elements is " + expectedResult + " passed");
			else 
				Console.WriteLine ("Hypeharmonic series test: partial sum of the first " + 
					numberOfElements + " elements is " + expectedResult + " failed");
		}

		static public void TestLoops (){
			TestMultiplication (3, 5, 15);
			TestMultiplication (-3, -5, 15);
			TestMultiplication (-3, 5, -15);
			TestMultiplication (3, -5, -15);
			TestMultiplication (0, 5, 0);
			TestMultiplication (3, 0, 0);
			Console.WriteLine ();
			TestFactorial (0, 1);
			TestFactorial (1, 1);
			TestFactorial (5, 120);
			Console.WriteLine ();
			TestHyperharmonicSeries (1, 1);
			TestHyperharmonicSeries (6, 1.4913889);
		}
	}

	class MainClass
	{
		static void Main (string[] args)
		{
			LoopExamples.TestLoops ();

		}
	}
}
