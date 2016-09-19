using System;
using System.Text;
using HelloWorld.Geometry;
using HelloWorld.HitTest;

namespace HelloWorld.LoopExamples
{
	public class LoopExamplesTest
	{
		static double doubleEpsilon = 1E-7;

		static public void TestMultiplication (int multiplier1, int multiplier2, int expectedResult){
			if (LoopExamples.Multiplication (multiplier1, multiplier2) == expectedResult)
				Console.WriteLine ("Multiplication test: " + multiplier1 + " * " + multiplier2 + " = " +
					expectedResult + " passed");
			else Console.WriteLine ("Multiplication test: " + multiplier1 + " * " + multiplier2 + " = " +
				expectedResult + " failed");
		}
		static public void TestFactorial (int argument, int expectedResult){
			if (argument < 0) {
				try {
					LoopExamples.Factorial (argument);
				} catch {
					Console.WriteLine ("Factorial test: Invalid argument passed");
				}
			} else {
				if (expectedResult == LoopExamples.Factorial (argument))
					Console.WriteLine ("Factorial test: " + argument + "! = " + expectedResult + " passed");
				else 
					Console.WriteLine ("Factorial test: " + argument + "! = " + expectedResult + " failed");
			}
		}
		static public void TestHyperharmonicSeries (int numberOfElements, double expectedResult){
			if ((Math.Abs(LoopExamples.HyperharmonicSeries (numberOfElements) - expectedResult)) < doubleEpsilon)
				Console.WriteLine ("Hyperharmonic series test: partial sum of the first " + 
					numberOfElements + " elements is " + expectedResult + " passed");
			else 
				Console.WriteLine ("Hypeharmonic series test: partial sum of the first " + 
					numberOfElements + " elements is " + expectedResult + " failed");
		}
		static public void TestSinusTaylorSeries (double angle, double expectedResult){
			if (Math.Abs (LoopExamples.SinusTaylorSeries (angle) - expectedResult) < doubleEpsilon)
				Console.WriteLine ("Taylor series test: sin(" + angle + ") = " + expectedResult + " passed");
			else 
				Console.WriteLine ("Taylor series test: sin(" + angle + ") = " + expectedResult + " failed");
		} 
		static public void TestAsterisks (int numberOfAsterisks, string expectedResult) {
			if (LoopExamples.AsteriskLine (numberOfAsterisks) == expectedResult) {
				Console.WriteLine ("Asterisks line test: " + numberOfAsterisks + " asterisks passed");
			} else 
				Console.WriteLine ("Asterisks line test: " + numberOfAsterisks + " asterisks failed");;
		}  
		static public void TestSlashes (int numberOfSlashes, string expectedResult) {
			if (LoopExamples.SlashLine (numberOfSlashes) == expectedResult) {
				Console.WriteLine ("Slash line test: " + numberOfSlashes + " slashes passed");
			} else 
				Console.WriteLine ("Slash line test: " + numberOfSlashes + " slashes failed");;
		}  
		static public string DrawRoundTarget (Circle target){
			StringBuilder outString = new StringBuilder();
			for (int i = 25; i > 0; i--) {
				for (int j = 0; j < 80; j++) {
					if (HitTarget.ShotInRoundTarget (target, Point2D.Construct (j, i)))
						outString.Append ("X");
					else
						outString.Append (" ");
				}
				outString.Append ("\n");
			}
			return outString.ToString();
		}
		static public string DrawRectangleTarget (Rectangle target){
			StringBuilder outString = new StringBuilder();
			for (int i = 25; i > 0; i--) {
				for (int j = 0; j < 80; j++) {
					if (HitTarget.ShotInRectangleTarget (target, Point2D.Construct (j, i)))
						outString.Append ("X");
					else
						outString.Append (" ");
				}
				outString.Append ("\n");
			}
			return outString.ToString();
		}
		static public string DrawRectangleTargetWithRoundHole (Rectangle target, Circle hole){
			StringBuilder outString = new StringBuilder();
			for (int i = 25; i > 0; i--) {
				for (int j = 0; j < 80; j++) {
					if (HitTarget.ShotInRectangleTarget (target, Point2D.Construct (j, i)) &&
						!HitTarget.ShotInRoundTarget (hole, Point2D.Construct (j, i)))
						outString.Append ("X");
					else
						outString.Append (" ");
				}
				outString.Append ("\n");
			}
			return outString.ToString();
		}
		static public string DrawTTarget (Rectangle target1, Rectangle target2){
			StringBuilder outString = new StringBuilder();
			for (int i = 25; i > 0; i--) {
				for (int j = 0; j < 80; j++) {
					if (HitTarget.ShotInRectangleTarget (target1, Point2D.Construct (j, i)) ||
						HitTarget.ShotInRectangleTarget (target2, Point2D.Construct (j, i)))
						outString.Append ("X");
					else
						outString.Append (" ");
				}
				outString.Append ("\n");
			}
			return outString.ToString();
		}
		static public void TestDrawTargets (){
			Rectangle rectangleTarget = Rectangle.Construct (Point2D.Construct (20, 5), Point2D.Construct (45, 20));
			Console.WriteLine (DrawRectangleTarget (rectangleTarget));

			Circle roundTarget = Circle.Construct (Point2D.Construct (40, 10), 10);
			Console.WriteLine (DrawRoundTarget (roundTarget));

			Rectangle TTargetPart1 = Rectangle.Construct (Point2D.Construct (50, 0), Point2D.Construct (60, 20));
			Rectangle TTargetPart2 = Rectangle.Construct (Point2D.Construct (30, 15), Point2D.Construct (70, 22));
			Console.WriteLine (DrawTTarget (TTargetPart1, TTargetPart2));

			Circle targetHole = Circle.Construct (Point2D.Construct (40, 10), 10);
			Console.WriteLine (DrawRectangleTargetWithRoundHole (rectangleTarget, targetHole));
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
			TestFactorial (-5, 0);
			Console.WriteLine ();
			TestHyperharmonicSeries (1, 1);
			TestHyperharmonicSeries (6, 1.49138888889);
			Console.WriteLine ();
			TestSinusTaylorSeries (0, 0);
			TestSinusTaylorSeries (Math.PI / 2, 1);
			TestSinusTaylorSeries (-Math.PI / 6, -0.5);
			TestSinusTaylorSeries (10 * Math.PI, 0);
			Console.WriteLine ();
			TestAsterisks (0, "");
			TestAsterisks (10, "**********");
			Console.WriteLine ();
			TestSlashes (0, "");
			TestSlashes (10, "/\\\\/\\\\/\\\\/");
			Console.WriteLine (LoopExamples.Labyrinth (800));
			TestDrawTargets ();
		}
	}
}

