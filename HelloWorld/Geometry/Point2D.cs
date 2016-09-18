using System;

namespace HelloWorld.Geometry
{
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
}

