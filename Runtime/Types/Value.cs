using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LurkingNinja.Utils.Types
{
	/// <summary>
	/// Value type 
	/// </summary>
	[Serializable,
	 StructLayout(LayoutKind.Explicit)]
	public struct Value : IEquatable<Value>, IEqualityComparer<Value>, IComparable<Value>
	{
		[SerializeField,
		 FieldOffset(0)]
		public long longValue;
		[NonSerialized,
		 FieldOffset(0)]
		public ulong ulongValue;
		[NonSerialized,
		 FieldOffset(0)]
		public bool boolValue;
		[NonSerialized,
		 FieldOffset(0)]
		public byte byteValue;
		[NonSerialized,
		 FieldOffset(0)]
		public char charValue;
		[NonSerialized,
		 FieldOffset(0)]
		public double doubleValue;
		[NonSerialized,
		 FieldOffset(0)]
		public float floatValue;
		[NonSerialized,
		 FieldOffset(0)]
		public int intValue;
		[NonSerialized,
		 FieldOffset(0)]
		public KeyCode keyCodeValue;
		[NonSerialized,
		 FieldOffset(0)]
		public sbyte sbyteValue;
		[NonSerialized,
		 FieldOffset(0)]
		public short shortValue;
		[NonSerialized,
		 FieldOffset(0)]
		public uint uintValue;
		[NonSerialized,
		 FieldOffset(0)]
		public ushort ushortValue;

#region Implicit operators
		public static implicit operator long(Value value) => value.longValue;
		public static implicit operator Value(long value) => new() { longValue = value };

		public static implicit operator ulong(Value value) => value.ulongValue;
		public static implicit operator Value(ulong value) => new() { ulongValue = value };

		public static implicit operator bool(Value value) => value.boolValue;
		public static implicit operator Value(bool value) => new() { boolValue = value };

		public static implicit operator byte(Value value) => value.byteValue;
		public static implicit operator Value(byte value) => new() { byteValue = value };

		public static implicit operator char(Value value) => value.charValue;
		public static implicit operator Value(char value) => new() { charValue = value };

		public static implicit operator double(Value value) => value.doubleValue;
		public static implicit operator Value(double value) => new() { doubleValue = value };

		public static implicit operator float(Value value) => value.floatValue;
		public static implicit operator Value(float value) => new() { floatValue = value };

		public static implicit operator int(Value value) => value.intValue;
		public static implicit operator Value(int value) => new() { intValue = value };

		public static implicit operator KeyCode(Value value) => value.keyCodeValue;
		public static implicit operator Value(KeyCode value) => new() { keyCodeValue = value };

		public static implicit operator sbyte(Value value) => value.sbyteValue;
		public static implicit operator Value(sbyte value) => new() { sbyteValue = value };

		public static implicit operator short(Value value) => value.shortValue;
		public static implicit operator Value(short value) => new() { shortValue = value };

		public static implicit operator uint(Value value) => value.uintValue;
		public static implicit operator Value(uint value) => new() { uintValue = value };

		public static implicit operator ushort(Value value) => value.ushortValue;
		public static implicit operator Value(ushort value) => new() { ushortValue = value };
#endregion
		
		public bool Equals(Value other) => longValue.Equals(other.longValue);
		public bool Equals(Value x, Value y) => x.longValue.Equals(y.longValue);

		public int GetHashCode(Value obj) => longValue.GetHashCode();

		public int CompareTo(Value other) => longValue.CompareTo(other);
	}
}