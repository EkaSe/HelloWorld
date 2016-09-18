using System;
using HelloWorld.Geometry;

namespace HelloWorld.HitTest
{
	public class HitTargetTest
	{
		static public void TestHitTarget (){
			//В двухмерной системе координат есть прямоугольная мишень, 
			//расположение и размеры которой заданы координатами противоположных углов.
			//И есть координаты места попадания выстрела. 
			//Нужно определить, пришлось ли попадание на мишень или нет.

			Point2D shotPoint = Point2D.Construct (-1,3);
			Point2D targetVertex1 = Point2D.Construct (-3, 3);
			Point2D targetVertex2 = Point2D.Construct (1, 5);
			Rectangle rectangleTarget = Rectangle.Construct (targetVertex1, targetVertex2);
			bool resultRectangleTarget = HitTarget.ShotInRectangleTarget (rectangleTarget,shotPoint);

			//Круглая мишень

			Circle roundTarget = Circle.Construct (Point2D.Construct (0, 2), 3);
			bool resultRoundTarget = HitTarget.ShotInRoundTarget (roundTarget, shotPoint);

			//T-мишень

			Rectangle TTargetPart1 = rectangleTarget;
			Rectangle TTargetPart2 = Rectangle.Construct (Point2D.Construct (0, 0), Point2D.Construct (-2, 3));
			bool resultTTarget = HitTarget.ShotInRectangleTarget (TTargetPart1, shotPoint) ||
				HitTarget.ShotInRectangleTarget (TTargetPart2, shotPoint);

			//Мишень с отверстием

			Circle targetHole = Circle.Construct (Point2D.Construct (2, 1), 2);
			bool resultTargetWithHole = HitTarget.ShotInRoundTarget (roundTarget, shotPoint) &&
				!HitTarget.ShotInRoundTarget (targetHole, shotPoint);
		}
	}
}

