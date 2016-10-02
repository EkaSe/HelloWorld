using System;

namespace HelloWorld.ComputerModel
{
	public class Memory
	{
		static public byte[] RAM = new byte[256];
		static public ushort stackTopOffset = 8;
		static public ushort stackOffset = (ushort) (stackTopOffset + 1);
		static public ushort instructionsOffset = 24;
	}
}

