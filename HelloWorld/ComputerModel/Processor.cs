using System;

namespace HelloWorld.ComputerModel
{
	public class Processor
	{
		static public Func<ushort, ushort, ushort, ushort, ushort>[] instructionsList = 
			new Func<ushort, ushort, ushort, ushort, ushort>[9]; 

		static public ushort AssignUInt8Const (ushort instructionOffset, ushort offset, ushort value, ushort unused) {
			Memory.RAM [offset] = BitConverter.GetBytes (value) [0];
			return (ushort) (instructionOffset + 8);
		}

		static public ushort AssignUInt8Var (ushort instructionOffset, ushort destinationOffset, ushort sourceOffset, ushort unused) {
			Memory.RAM [destinationOffset] = Memory.RAM [sourceOffset];
			return (ushort) (instructionOffset + 8);
		}

		static public ushort MultiplyUInt8 (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort aOffset) {
			Memory.RAM [aOffset] = (byte) (Memory.RAM [bOffset] * Memory.RAM [cOffset]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort DivideUInt8 (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort aOffset) { 
			Memory.RAM [aOffset] = (byte) (Memory.RAM [bOffset] / Memory.RAM [cOffset]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort LessEqualUInt8 (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort resultOffset) { 
			if (Memory.RAM [bOffset] <= Memory.RAM [cOffset])
				Memory.RAM [resultOffset] = 1;
			else
				Memory.RAM [resultOffset] = 0;
			return (ushort) (instructionOffset + 8);
		}

		static public ushort SkipIfZero (ushort instructionOffset, ushort valueOffset, ushort unused1, ushort unused2) {
			if (Memory.RAM [valueOffset] == 0)
				return (ushort) (instructionOffset + 16);
			else
				return (ushort) (instructionOffset + 8);
		}

		static public ushort Jump (ushort currentInstructionOffset, ushort instructionOffset, ushort unused1, ushort unused2){
			return (ushort) instructionOffset;
		}

		static public ushort EndOfInstructions (ushort instructionOffset, ushort unused1, ushort unused2, ushort unused3) {
			return 0;
		}

		static public ushort AddUInt8Const (ushort instructionOffset, ushort varOffset, ushort constValue, ushort sumOffset) {
			Memory.RAM [sumOffset] = (byte) (Memory.RAM [varOffset] + BitConverter.GetBytes (constValue) [0]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort ProcessInstruction (ushort currentInstructionOffset){
			instructionsList [(int)InstructionCode.EndOfInstructions] = EndOfInstructions;
			instructionsList [(int)InstructionCode.AssignUInt8Const] = AssignUInt8Const;
			instructionsList [(int)InstructionCode.AssignUInt8Var] = AssignUInt8Var; 
			instructionsList [(int)InstructionCode.MultiplyUInt8] = MultiplyUInt8; 
			instructionsList [(int)InstructionCode.DivideUInt8] = DivideUInt8;
			instructionsList [(int)InstructionCode.LessEqualUInt8] = LessEqualUInt8; 
			instructionsList [(int)InstructionCode.SkipIfZero] = SkipIfZero; 
			instructionsList [(int)InstructionCode.Jump] = Jump;
			instructionsList [(int)InstructionCode.AddUInt8Const] = AddUInt8Const;
			ushort currentInstruction = BitConverter.ToUInt16 (Memory.RAM, currentInstructionOffset);
			ushort arg1 = BitConverter.ToUInt16 (Memory.RAM, (currentInstructionOffset + 2));
			ushort arg2 = BitConverter.ToUInt16 (Memory.RAM, (currentInstructionOffset + 4));
			ushort arg3 = BitConverter.ToUInt16 (Memory.RAM, (currentInstructionOffset + 6));
			if (currentInstruction >= 9) {
				throw new Exception ("Invalid instruction code");
			};
			currentInstructionOffset = instructionsList [currentInstruction] (currentInstructionOffset, arg1, arg2, arg3);
			return currentInstructionOffset;
		}

		static public void RunProgram () {
			ushort currentInstructionOffset = 0;
			do {
				currentInstructionOffset = Processor.ProcessInstruction (currentInstructionOffset);
			} while (currentInstructionOffset > 0);
		}
	}
}

