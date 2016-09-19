using System;
using System.Text;
using HelloWorld.LoopExamples;
using HelloWorld.Geometry;
using HelloWorld.Algebra;
using HelloWorld.HitTest;

namespace HelloWorld
{
	class MainClass
	{
		static void Main (string[] args)
		{
			LoopExamplesTest.TestLoops ();
			QuadraticEquationTest.RunTests ();
		}
	}
}
