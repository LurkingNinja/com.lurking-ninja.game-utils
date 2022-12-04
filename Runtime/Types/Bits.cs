using System;
using System.Text;
using UnityEngine;

namespace LurkingNinja.Utils.Types
{
    public class Bits : ScriptableObject
    {
        public const int MAX_BIT_CAPACITY = int.MaxValue;
        public const int MAX_BYTE_CAPACITY = MAX_BIT_CAPACITY / 8;

        public const int BIT_AUTO = -1;
        public const int BIT8 = 1;
        public const int BIT16 = 2;
        public const int BIT32 = 4;
        public const int BIT64 = 8;
        public const int BIT128 = 16;
        public const int BIT256 = 32;
        
        public int BitCapacity { get; private set; }
        public int ByteCapacity { get; private set; }

        [SerializeField]
        private byte[] bits;
        private int _byteIndex;
        private int _bitIndex;

        public Bits(string base64Encoded, int byteSize = BIT32) : this(byteSize) => FromBase64(base64Encoded);
        public Bits(Bits other, int byteSize = BIT_AUTO) :
                this(byteSize == BIT_AUTO ? other.ByteCapacity : byteSize) => other.CopyTo(bits);
        public Bits(int byteSize = BIT_AUTO)
        {
            if (byteSize == BIT_AUTO) byteSize = BIT32;
            if (byteSize < 0 || byteSize > MAX_BYTE_CAPACITY) throw new ArgumentOutOfRangeException(nameof(byteSize),
                    $"Must be between 0 and {MAX_BYTE_CAPACITY}.");
            ByteCapacity = byteSize;
            BitCapacity = byteSize * 8 - 1;
            bits = new byte[byteSize];
        }

        private void SetIndexes(int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > BitCapacity) throw new ArgumentOutOfRangeException(nameof(bitIndex),
                    $"Must be between 0 and {BitCapacity}.");
            _byteIndex = bitIndex / 8;
            _bitIndex = bitIndex % 8;
        }

        public void CopyTo(byte[] toArray, int index = 0) => bits.CopyTo(toArray, index);
        public void CopyFrom(byte[] fromArray) => fromArray.CopyTo(bits, 0);
        public byte GetRawByte(int bitIndex)
        {
            SetIndexes(bitIndex);
            return bits[_byteIndex];
        }

        public void Clear() => Fill(byte.MinValue);
        public void Fill(byte value = byte.MaxValue) { for (var i = 0; i < ByteCapacity; i++) bits[i] = value; }

        public void Toggle(int bitIndex)
        {
            SetIndexes(bitIndex);
            bits[_byteIndex] ^= (byte) (1 << (_bitIndex % 8));
        }

        public bool this[int bitIndex]
        {
            get
            {
                SetIndexes(bitIndex);
                return ((bits[_byteIndex] >> _bitIndex) & 1) == 1;
            }
            set
            {
                SetIndexes(bitIndex);
                if (value)
                {
                    bits[_byteIndex] |= (byte)(1 << _bitIndex);
                    return;
                }
                bits[_byteIndex] &= (byte)~(1 << _bitIndex);
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder(GetType().Name);
            str.Append("(");
            str.Append(BitCapacity + 1);
            str.Append(": ");
            for (var i = 0; i < bits.Length; i++)
            {
                str.Append(Convert.ToString(bits[i], 2).PadLeft(8, '0'));
                if (i < bits.Length - 1)
                    str.Append(" | ");
            }
            str.Append(" )");
            return str.ToString();
        }

        public char[] ToChar() => Encoding.ASCII.GetString(bits).ToCharArray();

        public void FromChar(char[] array) => array.CopyTo(bits, 0);
        public string ToCharString() => new string(ToChar());
        public string ToBase64() =>  Convert.ToBase64String(bits);
        public void FromBase64(string base64Encoded) => Convert.FromBase64String(base64Encoded).CopyTo(bits, 0);
    }
}