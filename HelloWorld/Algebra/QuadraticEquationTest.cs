using System;

namespace HelloWorld.Algebra
{
	public class QuadraticEquationTest
	{
		static public void SolveTest () {
			double a, b, c; 
			a = 1;
			b = 2;
			c = 1;
			short result = QuadraticEquation.Solve (a, b, c);
		}
	}
}

