using System;
using UnityEngine;

namespace LurkingNinja.Utils.Types
{
	[Serializable]
	public struct IntRange
	{
		public bool IsSet => minValue != int.MinValue || maxValue != int.MaxValue;

		public int MinValue
		{
			get => minValue;
			set
			{
				if (value <= MaxValue) minValue = value;
			}
		}

		public int MaxValue
		{
			get => maxValue;
			set
			{
				if (value >= minValue) maxValue = value;
			}
		}

		[SerializeField]
		private int minValue;
		[SerializeField]
		private int maxValue;

		public IntRange(IntRange other) : this(other.MinValue, other.MaxValue) {}
		public IntRange(int min = int.MinValue, int max = int.MaxValue)
		{
			if (min > max) throw new ArgumentException(
				$"{min.GetType().Name} should not be greater than {max.GetType().Name}.");
			minValue = min;
			maxValue = max;
		}
		
		public static IntRange operator +(IntRange ir, int i) =>
				new (ir.MinValue + i, ir.MaxValue + i);
		public static IntRange operator +(IntRange ir1, IntRange ir2) =>
				new (ir1.MinValue + ir2.MinValue, ir1.MaxValue + ir2.MaxValue);
		public static IntRange operator -(IntRange ir, int i) =>
				new (ir.MinValue - i, ir.MaxValue - i);
		public static IntRange operator -(IntRange ir1, IntRange ir2) =>
				new (ir1.MinValue - ir2.MinValue, ir1.MaxValue - ir2.MaxValue);
		public static IntRange operator *(IntRange ir, int i) =>
				new (ir.MinValue * i, ir.MaxValue * i);
		public static IntRange operator *(IntRange ir1, IntRange ir2) =>
				new (ir1.MinValue * ir2.MinValue, ir1.MaxValue * ir2.MaxValue);
		public static FloatRange operator *(IntRange ir, float i) =>
				new FloatRange(ir.MinValue * i, ir.MaxValue * i);
		public static FloatRange operator *(IntRange ir1, FloatRange ir2) =>
				new (ir1.MinValue * ir2.MinValue, ir1.MaxValue * ir2.MaxValue);
		
		public static implicit operator ValueTuple<int, int>(IntRange intRange) =>
				(intRange.MinValue, intRange.MaxValue);
		public static implicit operator IntRange(ValueTuple<int, int> minMax) =>
				new (minMax.Item1, minMax.Item2);

		public bool IsOverlapping(int min, int max) => minValue >= min || maxValue <= max;
		public bool IsOverlapping(ValueTuple<int, int> minMax) => IsOverlapping(minMax.Item1, minMax.Item2);
		public bool IsOverlapping(IntRange other) => IsOverlapping(other.MinValue, other.MaxValue);

		public bool IsInRange(int value) => minValue <= value && value <= maxValue;
	}
}