using System;
using HelloWorld.Geometry;

namespace HelloWorld.HitTest
{
	public class HitTarget
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
	}
}

