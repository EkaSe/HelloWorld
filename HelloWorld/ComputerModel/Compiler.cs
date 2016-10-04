using System;

namespace HelloWorld.ComputerModel
{
	public enum InstructionCode: ushort {
		EndOfInstructions, 
		AssignUInt8Const, 
		AssignUInt8Var, 
		MultiplyUInt8, 
		DivideUInt8, 
		LessEqualUInt8, 
		SkipIfZero, 
		Jump,
		AddUInt8Const,
		CallMethod,
		AreEqualUInt8
	};

	public class Compiler
	{
		static public ushort WriteBytesToArray (byte[] array, ushort value, ushort startOffset) {
			array [startOffset++] = BitConverter.GetBytes (value) [0];
			array [startOffset++] = BitConverter.GetBytes (value) [1];;
			return startOffset;
		}

		static public void WriteInstruction(byte[] instructions, ref ushort offset, ushort instructionCode, 
			ushort argument1, ushort argument2, ushort argument3) {
			offset = WriteBytesToArray (instructions, instructionCode, offset);
			offset = WriteBytesToArray (instructions, argument1, offset);
			offset = WriteBytesToArray (instructions, argument2, offset);
			offset = WriteBytesToArray (instructions, argument3, offset);
		}

		static public void CompileSugar (byte[] instructions, ushort startInstructionOffset, 
			out ushort memorySize, out ushort instructionsSize) {

			/*
			 static public void EnoughSugar () {
				double ThermosVolume = 1.5; 
				double cupVolume = 0.3;
				double pieceOfSugarMass = 5;
				int piecesOfSugarForCup = 3;
				double sugarMass = 55;
				double sugarMassRequired;
				sugarMassRequired = thermosVolume * piecesOfSugarForCup * pieceOfSugarMass / cupVolume;
				if (sugarMassRequired <= sugarMass)
					result = 1;
				else result = 0;
			}*/

			ushort currentOffset = 0;
			ushort thermosVolumeOffset = currentOffset++;
			ushort cupVolumeOffset = currentOffset++;
			ushort pieceOfSugarMassOffset = currentOffset++;
			ushort pieceOfSugarForCupOffset = currentOffset++;
			ushort sugarMassAvailableOffset = currentOffset++;
			ushort sugarMassRequiredOffset = currentOffset++;
			ushort isEnoughSugarOffset = currentOffset++;
			ushort sugarMassforCupOffset = currentOffset++;
			ushort cupsInThermosOffset = currentOffset++;
			ushort resultOffset = currentOffset++;

			memorySize = currentOffset;

			ushort currentInstructionOffset = startInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, thermosVolumeOffset, 15, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, cupVolumeOffset, 3, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, pieceOfSugarMassOffset, 5, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, pieceOfSugarForCupOffset, 3, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, sugarMassAvailableOffset, 55, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.MultiplyUInt8, pieceOfSugarForCupOffset, pieceOfSugarMassOffset, sugarMassforCupOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.DivideUInt8, thermosVolumeOffset, cupVolumeOffset, cupsInThermosOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.MultiplyUInt8, sugarMassforCupOffset, cupsInThermosOffset, sugarMassRequiredOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.LessEqualUInt8, sugarMassRequiredOffset, sugarMassAvailableOffset, isEnoughSugarOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.SkipIfZero, isEnoughSugarOffset, 0, 0);
			ushort ifNotZeroOffset = currentInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.Jump, 0, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, resultOffset, 0, 0);
			ushort ifZeroOffset = currentInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.Jump, 0, 0, 0);
			ifNotZeroOffset = WriteBytesToArray (instructions, (ushort) currentInstructionOffset, (ushort) (ifNotZeroOffset + 2));
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, resultOffset, 1, 0);
			ifZeroOffset = WriteBytesToArray (instructions, (ushort) currentInstructionOffset, (ushort) (ifZeroOffset + 2));
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.EndOfInstructions, 0, 0, 0);

			instructionsSize = (ushort) (currentInstructionOffset - startInstructionOffset);
		}

		static public void CompileOrderNumbers (byte[] instructions, ushort startInstructionOffset, 
			out ushort memorySize, out ushort instructionsSize) {
			byte[] composeInstructions = new byte[] {
				/* 0*/ 1, 0, 0, 0, 15, 0, 0, 0, // value1 = 15
				/* 8*/ 1, 0, 1, 0, 70, 0, 0, 0, // value2 = 70
				/*16*/ 5, 0, 0, 0, 1, 0, 4, 0, // ordered = (val1 <= val2)
				/*24*/ 6, 0, 4, 0, 0, 0, 0, 0, // skipIfZero (ordered)
				/*32*/ 7, 0, 64, 0, 0, 0, 0, 0, // jump (64)
				/*40*/ 2, 0, 2, 0, 1, 0, 0, 0, // min = value2
				/*48*/ 2, 0, 3, 0, 0, 0, 0, 0, // max = value1
				/*56*/ 7, 0, 80, 0, 0, 0, 0, 0, // jump (80)
				/*64*/ 2, 0, 2, 0, 0, 0, 0, 0, // min = value1
				/*72*/ 2, 0, 3, 0, 1, 0, 0, 0, // max = value2
				/*80*/ 0, 0, 0, 0, 0, 0, 0, 0 // End
			};
			for (int i = 0; i < 88; i++) {
				instructions [i + startInstructionOffset] = composeInstructions [i];
			};
			memorySize = 5;
			instructionsSize = 88;
		}

		static public void CompileFactorialCycle(byte[] instructions, ushort startInstructionOffset,
			out ushort memorySize, out ushort instructionsSize) {
			ushort currentOffset = 0;
			ushort nOffset = currentOffset++;
			ushort factorialOffset = currentOffset++;
			ushort iOffset = currentOffset++;
			ushort iLessEqualNOffset = currentOffset++;
			memorySize = currentOffset;

			ushort currentInstructionOffset = startInstructionOffset;

			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, nOffset, 5, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, factorialOffset, 1, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, iOffset, 1, 0);
			ushort cycleStart = currentInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.MultiplyUInt8, iOffset, factorialOffset, factorialOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AddUInt8Const, iOffset, 1, iOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.LessEqualUInt8, iOffset, nOffset, iLessEqualNOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.SkipIfZero, iLessEqualNOffset, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.Jump, cycleStart, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.EndOfInstructions, 0, 0, 0);

			instructionsSize = (ushort) (currentInstructionOffset - startInstructionOffset);
		}

		static public void CompileTestFactorial(byte[] instructions, ushort startInstructionOffset,
			out ushort memorySize, out ushort instructionsSize) {
			ushort currentOffset = 0;
			ushort nOffset = currentOffset++;
			ushort resultsEqualOffset = currentOffset++;
			ushort factorialCycleOffset = currentOffset++;
			ushort factorialRecursiveOffset = currentOffset++;
			ushort iOffset = currentOffset++;
			ushort iLessEqualNOffset = currentOffset++;
			memorySize = currentOffset;

			ushort currentInstructionOffset = startInstructionOffset;

			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, nOffset, 5, 0);
			
			ushort factorialCycleCall = currentInstructionOffset;
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.CallMethod, (ushort) (currentInstructionOffset + 8), nOffset, factorialCycleOffset);

			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AreEqualUInt8, factorialCycleOffset, factorialRecursiveOffset, resultsEqualOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.EndOfInstructions, 0, 0, 0);

			ushort unused = WriteBytesToArray (instructions, (ushort) (currentInstructionOffset - startInstructionOffset), (ushort) (factorialCycleCall + 2));
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, nOffset, 5, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, factorialCycleOffset, 1, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AssignUInt8Const, iOffset, 1, 0);
			ushort cycleStart = (ushort) (currentInstructionOffset - startInstructionOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.MultiplyUInt8, iOffset, factorialCycleOffset, factorialCycleOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.AddUInt8Const, iOffset, 1, iOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.LessEqualUInt8, iOffset, nOffset, iLessEqualNOffset);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.SkipIfZero, iLessEqualNOffset, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.Jump, cycleStart, 0, 0);
			WriteInstruction (instructions, ref currentInstructionOffset, 
				(ushort)InstructionCode.EndOfInstructions, (ushort) (factorialCycleCall - startInstructionOffset), 0, 0);
			
			instructionsSize = (ushort) (currentInstructionOffset - startInstructionOffset);
		}

		static public void Compile () {
			ushort currentOffset = 0;
			ushort instructionsSize = 256;
			ushort memorySize = 0;

			ushort TestFactorialOffset = currentOffset;
			currentOffset += 2;
			Compiler.CompileTestFactorial (Memory.HDD, currentOffset, out memorySize, out instructionsSize);
			currentOffset = WriteBytesToArray (Memory.HDD, instructionsSize, TestFactorialOffset);
			currentOffset += instructionsSize;

			ushort FactorialCycleOffset = currentOffset;
			currentOffset += 2;
			Compiler.CompileFactorialCycle (Memory.HDD, currentOffset, out memorySize, out instructionsSize);
			currentOffset = WriteBytesToArray (Memory.HDD, instructionsSize, FactorialCycleOffset);
			currentOffset += instructionsSize;

			ushort OrderNumbersOffset = currentOffset;
			currentOffset += 2;
			Compiler.CompileOrderNumbers (Memory.HDD, currentOffset, out memorySize, out instructionsSize);
			currentOffset = WriteBytesToArray (Memory.HDD, instructionsSize, OrderNumbersOffset);
			currentOffset += instructionsSize;

			ushort SugarOffset = currentOffset;
			currentOffset += 2;
			Compiler.CompileSugar (Memory.HDD, currentOffset, out memorySize, out instructionsSize);
			currentOffset = WriteBytesToArray (Memory.HDD, instructionsSize, SugarOffset);
			currentOffset += instructionsSize;


			ushort currentMethodOffset = TestFactorialOffset;
			instructionsSize = BitConverter.ToUInt16 (Memory.HDD, currentMethodOffset);
			currentMethodOffset += 2;
			for (int i = 0; i < instructionsSize; i++) {
				Memory.RAM [i] = Memory.HDD [i + currentMethodOffset];
			};

			ushort memoryStart = instructionsSize;

			currentOffset = 0;
			while (currentOffset < instructionsSize) {
				ushort currentInstruction = BitConverter.ToUInt16 (Memory.RAM, currentOffset);
				ushort arg1 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 2));
				ushort arg2 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 4));
				ushort arg3 = BitConverter.ToUInt16 (Memory.RAM, (currentOffset + 6));
				if ((currentInstruction != (ushort) InstructionCode.Jump) &&
					(currentInstruction != (ushort) InstructionCode.CallMethod) &&
					(currentInstruction != (ushort) InstructionCode.EndOfInstructions))
				Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg1 + memoryStart), (ushort) (currentOffset + 2));
				if ((currentInstruction != (ushort) InstructionCode.AssignUInt8Const) &&
					(currentInstruction != (ushort) InstructionCode.AddUInt8Const))
					Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg2 + memoryStart), (ushort) (currentOffset + 4));
				Compiler.WriteBytesToArray (Memory.RAM, (ushort) (arg3 + memoryStart), (ushort) (currentOffset + 6));
				currentOffset += 8;
			};
		}
	}
}

