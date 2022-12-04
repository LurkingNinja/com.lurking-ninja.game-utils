using System;
using UnityEngine;

namespace LurkingNinja.Utils.Types
{
	[Serializable]
	public struct FloatRange
	{
		public bool IsSet => Mathf.Approximately(minValue, float.MinValue)
				|| Mathf.Approximately(maxValue, float.MaxValue);

		public float MinValue
		{
			get => minValue;
			set
			{
				if (value <= MaxValue) minValue = value;
			}
		}

		public float MaxValue
		{
			get => maxValue;
			set
			{
				if (value >= minValue) maxValue = value;
			}
		}

		[SerializeField]
		private float minValue;
		[SerializeField]
		private float maxValue;

		public FloatRange(FloatRange other) : this(other.MinValue, other.MaxValue) {}
		public FloatRange(float min = float.MinValue, float max = float.MaxValue)
		{
			if (min > max) throw new ArgumentException(
				$"{min.GetType().Name} should not be greater than {max.GetType().Name}.");
			minValue = min;
			maxValue = max;
		}
		
		public static FloatRange operator +(FloatRange ir, int i) =>
				new (ir.MinValue + i, ir.MaxValue + i);
		public static FloatRange operator +(FloatRange ir1, FloatRange ir2) =>
				new (ir1.MinValue + ir2.MinValue, ir1.MaxValue + ir2.MaxValue);
		public static FloatRange operator -(FloatRange ir, int i) =>
				new (ir.MinValue - i, ir.MaxValue - i);
		public static FloatRange operator -(FloatRange ir1, FloatRange ir2) =>
				new (ir1.MinValue - ir2.MinValue, ir1.MaxValue - ir2.MaxValue);
		public static FloatRange operator *(FloatRange ir, int i) =>
				new (ir.MinValue * i, ir.MaxValue * i);
		public static FloatRange operator *(FloatRange ir1, FloatRange ir2) =>
				new (ir1.MinValue * ir2.MinValue, ir1.MaxValue * ir2.MaxValue);
		public static FloatRange operator *(FloatRange ir, float i) =>
				new FloatRange(ir.MinValue * i, ir.MaxValue * i);
		public static FloatRange operator *(FloatRange ir1, IntRange ir2) =>
				new (ir1.MinValue * ir2.MinValue, ir1.MaxValue * ir2.MaxValue);
		
		public static implicit operator ValueTuple<float, float>(FloatRange intRange) =>
				(intRange.MinValue, intRange.MaxValue);
		public static implicit operator FloatRange(ValueTuple<float, float> minMax) =>
				new (minMax.Item1, minMax.Item2);

		public bool IsOverlapping(float min, float max) => minValue >= min || maxValue <= max;
		public bool IsOverlapping(ValueTuple<float, float> minMax) => IsOverlapping(minMax.Item1, minMax.Item2);
		public bool IsOverlapping(FloatRange other) => IsOverlapping(other.MinValue, other.MaxValue);

		public bool IsInRange(float value) => minValue <= value && value <= maxValue;
	}
}