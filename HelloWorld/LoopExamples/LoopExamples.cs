using System;
using System.Text;

namespace HelloWorld.LoopExamples
{
	public class LoopExamples
	{
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
			if (argument < 0) throw new Exception("Factorial of negative number is undefined");
			for (int i = 1; i <= argument; i++)
				result = result * i;
			return result;
		}
		//partial sum of 1/n^2 series
		static public double HyperharmonicSeries (int numberOfElements){
			double result = 0;
			for (int i = 1; i <= numberOfElements; i++)
				result += 1.0 / (i * i);
			return result;
		}
		static public double SinusTaylorSeries (double angle) { //angle in radians
			double epsilon = 1E-7;
			angle = angle - 2 * Math.PI * Math.Truncate (angle / 2 / Math.PI);
			int n = 0;
			double currentTerm = angle;
			double nextTerm = angle;
			double partialSum = currentTerm;
			do {
				currentTerm = nextTerm;
				nextTerm = -currentTerm * angle * angle / (2 * n + 2) / (2 * n + 3);
				partialSum += nextTerm;
				n++;
			} while (Math.Abs (currentTerm - nextTerm) > epsilon);
			return partialSum;
		}
		static public string AsteriskLine (int numberOfSymbols){
			StringBuilder asterisks = new StringBuilder ();
			for (int i = 0; i < numberOfSymbols; i++)
				asterisks.Append ("*");
			return asterisks.ToString ();
		}
		static public string SlashLine (int numberOfSymbols){
			StringBuilder slashes = new StringBuilder ();
			for (int i = 0; i < numberOfSymbols; i++) {
				if (i % 3 == 0)
					slashes.Append ("/");
				else slashes.Append ("\\");
			};
			return slashes.ToString ();
		}
	}
}

