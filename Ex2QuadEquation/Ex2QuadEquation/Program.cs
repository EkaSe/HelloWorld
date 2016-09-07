using System;

namespace Ex2QuadEquation
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			double a, b, c;
			a = 1.0;
			b = 2;
			c = 1;
			short NumberOfRealRoots = 0;
			double root1, root2;
			if (a != 0) {
				double d;
				d = b * b - 4 * a * c;
				if (d >= 0) {
					root1 = (-b + Math.Sqrt (d)) / a / 2;
					root2 = (-b - Math.Sqrt (d)) / a / 2;
					if (d == 0)
						NumberOfRealRoots = 1;
					else
						NumberOfRealRoots = 2;
				} 
			} else {//a==0
				if (b != 0) {
					root1 = -c / b;
					NumberOfRealRoots = 1;
				}
			}
		}
	}
}
