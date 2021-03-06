﻿using System;

namespace HelloWorld.ComputerModel
{
	public class Processor
	{
		static int stackTopOffsetAddress;

		static public Func<ushort, ushort, ushort, ushort, ushort>[] instructionsList = 
			new Func<ushort, ushort, ushort, ushort, ushort>[24]; 

		static public ushort AssignUInt8Const (ushort instructionOffset, ushort offset, ushort value, ushort unused) {
			Memory.RAM [offset] = BitConverter.GetBytes (value) [0];
			return (ushort) (instructionOffset + 8);
		}

		static public ushort AssignUInt8Var (ushort instructionOffset, ushort destinationOffset, ushort sourceOffset, ushort unused) {
			Memory.RAM [destinationOffset] = Memory.RAM [sourceOffset];
			return (ushort) (instructionOffset + 8);
		}

		static public ushort MultiplyUInt8 (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort aOffset) {
			Memory.RAM [aOffset] = (byte) (Memory.RAM [bOffset ] * Memory.RAM [cOffset]);
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

		static public ushort EndOfInstructions (ushort currentInstructionOffset, ushort unused1, ushort unused2, ushort unused3) {
			return 0;
		}

		static public ushort AddUInt8Const (ushort instructionOffset, ushort varOffset, ushort constValue, ushort sumOffset) {
			Memory.RAM [sumOffset] = (byte) (Memory.RAM [varOffset] + BitConverter.GetBytes (constValue) [0]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort CallMethod (ushort currentOffset, ushort startMethodOffset, ushort inputOffset, ushort localInputOffset){
			return (ushort) startMethodOffset;
		}

		static public ushort AreEqualUInt8 (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort resultOffset) { 
			if (Memory.RAM [bOffset] == Memory.RAM [cOffset])
				Memory.RAM [resultOffset] = 1;
			else
				Memory.RAM [resultOffset] = 0;
			return (ushort) (instructionOffset + 8);
		}

		static public ushort Return (ushort currentOffset, ushort unused1, ushort unused2, ushort unused3) {
			ushort currentStackOffset = (ushort) (stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + 1);
			ushort returnAddress = BitConverter.ToUInt16 (Memory.RAM, currentStackOffset);
			return returnAddress;
		}

		static public ushort InitStack (ushort currentOffset, ushort programStackTop, ushort unused1, ushort unused2){
			stackTopOffsetAddress = programStackTop;
			return (ushort) (currentOffset + 8);
		}

		static public ushort AssignUInt8ConstStack (ushort instructionOffset, ushort offset, ushort value, ushort unused) {
			int address;
			if (offset != 0) {
				address = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + offset + 2;
				Memory.RAM [address] = BitConverter.GetBytes (value) [0];
			} else {
				address = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + 1;
				Memory.RAM [address] = BitConverter.GetBytes (value) [0];
				Memory.RAM [address + 1] = BitConverter.GetBytes (value) [1];
			}
			return (ushort) (instructionOffset + 8);
		}

		static public ushort SubtractUInt8Const (ushort instructionOffset, ushort varOffset, ushort constValue, ushort resultOffset) {
			Memory.RAM [resultOffset] = (byte) (Memory.RAM [varOffset] - BitConverter.GetBytes (constValue) [0]);
			return (ushort) (instructionOffset + 8);
		}

		//==============================================================================================
		static public ushort AssignUInt8VarStack (ushort instructionOffset, ushort destinationOffset, ushort sourceOffset, ushort unused) {
			int destinationAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + destinationOffset + 2;
			int sourceAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + sourceOffset + 2;
			Memory.RAM [destinationAddress] = Memory.RAM [sourceAddress];
			return (ushort) (instructionOffset + 8);
		}

		static public ushort MultiplyUInt8Stack (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort aOffset) {
			int aAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + aOffset + 2;
			int bAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + bOffset + 2;
			int cAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + cOffset + 2;
			Memory.RAM [aAddress] = (byte) (Memory.RAM [bAddress] * Memory.RAM [cAddress]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort DivideUInt8Stack (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort aOffset) { 
			int aAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + aOffset + 2;
			int bAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + bOffset + 2;
			int cAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + cOffset + 2;
			Memory.RAM [aAddress] = (byte) (Memory.RAM [bAddress] / Memory.RAM [cAddress]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort LessEqualUInt8Stack (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort resultOffset) { 
			int resultAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + resultOffset + 2;
			int bAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + bOffset + 2;
			int cAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + cOffset + 2;
			if (Memory.RAM [bAddress] <= Memory.RAM [cAddress])
				Memory.RAM [resultAddress] = 1;
			else
				Memory.RAM [resultAddress] = 0;
			return (ushort) (instructionOffset + 8);
		}

		static public ushort SkipIfZeroStack (ushort instructionOffset, ushort valueOffset, ushort unused1, ushort unused2) {
			int valueAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + valueOffset + 2;
			if (Memory.RAM [valueAddress] == 0)
				return (ushort) (instructionOffset + 16);
			else
				return (ushort) (instructionOffset + 8);
		}

		static public ushort AddUInt8ConstStack (ushort instructionOffset, ushort varOffset, ushort constValue, ushort sumOffset) {
			int sumAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + sumOffset + 2;
			int varAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + varOffset + 2;
			Memory.RAM [sumAddress] = (byte) (Memory.RAM [varAddress] + BitConverter.GetBytes (constValue) [0]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort AreEqualUInt8Stack (ushort instructionOffset, ushort bOffset, ushort cOffset, ushort resultOffset) { 
			int resultAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + resultOffset + 2;
			int bAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + bOffset + 2;
			int cAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + cOffset + 2;
			if (Memory.RAM [bAddress] == Memory.RAM [cAddress])
				Memory.RAM [resultAddress] = 1;
			else
				Memory.RAM [resultAddress] = 0;
			return (ushort) (instructionOffset + 8);
		}

		static public ushort SubtractUInt8ConstStack (ushort instructionOffset, ushort varOffset, ushort constValue, ushort resultOffset) {
			int resultAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + resultOffset + 2;
			int varAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + varOffset + 2;
			Memory.RAM [resultAddress] = (byte) (Memory.RAM [varAddress] - BitConverter.GetBytes (constValue) [0]);
			return (ushort) (instructionOffset + 8);
		}

		static public ushort MoreThanConstUInt8Stack (ushort instructionOffset, ushort varOffset, ushort constValue, ushort resultOffset) { 
			int varAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + varOffset + 2;
			int resultAddress = stackTopOffsetAddress + Memory.RAM [stackTopOffsetAddress] + resultOffset + 2;
			if (Memory.RAM [varAddress] > BitConverter.GetBytes (constValue) [0])
				Memory.RAM [resultAddress] = 1;
			else
				Memory.RAM [resultAddress] = 0;
			return (ushort) (instructionOffset + 8);
		}

		//==================================================================================================

		static public ushort ProcessInstruction (ushort currentInstructionOffset, ref ushort startMethodOffset){
			/* 0*/ instructionsList [(int)InstructionCode.EndOfInstructions] = EndOfInstructions;
			/* 1*/ instructionsList [(int)InstructionCode.AssignUInt8Const] = AssignUInt8Const;
			/* 2*/ instructionsList [(int)InstructionCode.AssignUInt8Var] = AssignUInt8Var; 
			/* 3*/ instructionsList [(int)InstructionCode.MultiplyUInt8] = MultiplyUInt8; 
			/* 4*/ instructionsList [(int)InstructionCode.DivideUInt8] = DivideUInt8;
			/* 5*/ instructionsList [(int)InstructionCode.LessEqualUInt8] = LessEqualUInt8; 
			/* 6*/ instructionsList [(int)InstructionCode.SkipIfZero] = SkipIfZero; 
			/* 7*/ instructionsList [(int)InstructionCode.Jump] = Jump;
			/* 8*/ instructionsList [(int)InstructionCode.AddUInt8Const] = AddUInt8Const;
			/* 9*/ instructionsList [(int)InstructionCode.CallMethod] = CallMethod;
			/*10*/ instructionsList [(int)InstructionCode.AreEqualUInt8] = AreEqualUInt8;
			/*11*/ instructionsList [(int)InstructionCode.Return] = Return;
			/*12*/ instructionsList [(int)InstructionCode.InitStack] = InitStack;
			/*13*/ instructionsList [(int)InstructionCode.AssignUInt8ConstStack] = AssignUInt8ConstStack;
			/*14*/ instructionsList [(int)InstructionCode.SubtractUInt8Const] = SubtractUInt8Const;
			/*15*/ instructionsList [(int)InstructionCode.AssignUInt8VarStack] = AssignUInt8VarStack; 
			/*16*/ instructionsList [(int)InstructionCode.MultiplyUInt8Stack] = MultiplyUInt8Stack; 
			/*17*/ instructionsList [(int)InstructionCode.DivideUInt8Stack] = DivideUInt8Stack;
			/*18*/ instructionsList [(int)InstructionCode.LessEqualUInt8Stack] = LessEqualUInt8Stack; 
			/*19*/ instructionsList [(int)InstructionCode.AddUInt8ConstStack] = AddUInt8ConstStack;
			/*20*/ instructionsList [(int)InstructionCode.AreEqualUInt8Stack] = AreEqualUInt8Stack;
			/*21*/ instructionsList [(int)InstructionCode.SubtractUInt8ConstStack] = SubtractUInt8ConstStack;
			/*22*/ instructionsList [(int)InstructionCode.SkipIfZeroStack] = SkipIfZeroStack;
			/*22*/ instructionsList [(int)InstructionCode.MoreThanConstUInt8Stack] = MoreThanConstUInt8Stack;

			ushort currentInstruction = BitConverter.ToUInt16 (Memory.RAM, currentInstructionOffset);
			ushort arg1 = BitConverter.ToUInt16 (Memory.RAM, (currentInstructionOffset + 2));
			ushort arg2 = BitConverter.ToUInt16 (Memory.RAM, (currentInstructionOffset + 4));
			ushort arg3 = BitConverter.ToUInt16 (Memory.RAM, (currentInstructionOffset + 6));
			if (currentInstruction >= 24) {
				throw new Exception ("Invalid instruction code");
			};

			//Console.WriteLine (currentInstruction + ", line " + currentInstructionOffset + "; stack top = " + Memory.RAM [stackTopOffsetAddress]);
			currentInstructionOffset = instructionsList [currentInstruction] (currentInstructionOffset, arg1, arg2, arg3);
			return currentInstructionOffset;
		}

		static public void RunProgram () {
			ushort startMethodOffset = 0;
			ushort currentInstructionOffset = 0;
			do {
				currentInstructionOffset = Processor.ProcessInstruction (currentInstructionOffset, ref startMethodOffset);
			} while (currentInstructionOffset > 0);
		}
	}
}