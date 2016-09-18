using System;

namespace HelloWorld.Geometry
{
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
}

