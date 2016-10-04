using System;

namespace HelloWorld.ComputerModel
{
	public class Computer
	{
		static public void Start () {

			//Компиляция
			Compiler.Compile ();

			//Выполнение
			Processor.RunProgram ();
		}
	}
}

