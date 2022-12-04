using System;
using UnityEngine;

namespace LurkingNinja.Utils.Types
{
	[Serializable]
	public struct ValueRange
	{
		public Value MinValue
		{
			get => minValue;
			set
			{
				if (value <= MaxValue) minValue = value;
			}
		}

		public Value MaxValue
		{
			get => maxValue;
			set
			{
				if (value >= minValue) maxValue = value;
			}
		}

		public Value Midpoint => MinValue + MaxValue / 2;

		[SerializeField]
		private Value minValue;
		[SerializeField]
		private Value maxValue;

		public ValueRange(ValueRange other) : this(other.MinValue.ulongValue, other.MaxValue.ulongValue) {}

		public ValueRange(ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
		{
			if (min > max) throw new ArgumentException(
				$"{min.GetType().Name} should not be greater than {max.GetType().Name}.");
			minValue = min;
			maxValue = max;
		}
		public ValueRange(long min = long.MinValue, long max = long.MaxValue)
		{
			if (min > max) throw new ArgumentException(
				$"{min.GetType().Name} should not be greater than {max.GetType().Name}.");
			minValue = min;
			maxValue = max;
		}
		public ValueRange(double min = double.MinValue, double max = double.MaxValue)
		{
			if (min > max) throw new ArgumentException(
				$"{min.GetType().Name} should not be greater than {max.GetType().Name}.");
			minValue = min;
			maxValue = max;
		}

		
		public static ValueRange operator +(ValueRange ir, int i) =>
				new (ir.MinValue + i, ir.MaxValue + i);
		public static ValueRange operator +(ValueRange ir1, ValueRange ir2) =>
				new (ir1.MinValue + ir2.MinValue, ir1.MaxValue + ir2.MaxValue);
		public static ValueRange operator -(ValueRange ir, int i) =>
				new (ir.MinValue - i, ir.MaxValue - i);
		public static ValueRange operator -(ValueRange ir1, ValueRange ir2) =>
				new (ir1.MinValue - ir2.MinValue, ir1.MaxValue - ir2.MaxValue);
		public static ValueRange operator *(ValueRange ir, int i) =>
				new (ir.MinValue * i, ir.MaxValue * i);
		public static ValueRange operator *(ValueRange ir1, ValueRange ir2) =>
				new (ir1.MinValue * ir2.MinValue, ir1.MaxValue * ir2.MaxValue);
		public static ValueRange operator *(ValueRange ir, float i) =>
				new ValueRange(ir.MinValue * i, ir.MaxValue * i);
		public static ValueRange operator *(ValueRange ir1, IntRange ir2) =>
				new (ir1.MinValue * ir2.MinValue, ir1.MaxValue * ir2.MaxValue);
		
		public static implicit operator ValueTuple<float, float>(ValueRange intRange) =>
				(intRange.MinValue, intRange.MaxValue);
		public static implicit operator ValueRange(ValueTuple<float, float> minMax) =>
				new (minMax.Item1, minMax.Item2);

		public bool IsOverlapping(float min, float max) => minValue >= min || maxValue <= max;
		public bool IsOverlapping(ValueTuple<float, float> minMax) => IsOverlapping(minMax.Item1, minMax.Item2);
		public bool IsOverlapping(ValueRange other) => IsOverlapping(other.MinValue, other.MaxValue);

		public bool IsInRange(float value) => minValue <= value && value <= maxValue;
	}
}