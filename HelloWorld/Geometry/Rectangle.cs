using System;

namespace HelloWorld.Geometry
{
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
}

