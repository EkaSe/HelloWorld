using System;

namespace HelloWorld.Algebra
{
	public class QuadraticEquationTest
	{
		static public void SolveTest (double a, double b, double c, 
			short expectedResult, double expectedRoot1, double expectedRoot2) {
			double root1 = 0;
			double root2 = 0;
			short result = QuadraticEquation.Solve (a, b, c, out root1, out root2);
			if (result == expectedResult) {
				if ((result == 0) || ((expectedRoot1 == root1) && (expectedRoot2 == root2) ||
				    (expectedRoot1 == root2) && (expectedRoot2 == root1)))
					Console.WriteLine ("Quadratic equation test: " + a + "x^2 + " + b + "x + " + c +
					" = 0 passed");
			} else Console.WriteLine ("Quadratic equation test: " + a + "x^2 + " + b + "x + " + c +
				" = 0 failed");
		}

		static public void RunTests (){
			SolveTest (1, 2, 1, 1, -1, -1);
			SolveTest (0, 1, -5, 1, 5, 0);
		}
	}
}

