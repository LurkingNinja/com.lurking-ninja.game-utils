using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;

namespace LurkingNinja.Utils.Types
{
    [Serializable,
     StructLayout(LayoutKind.Sequential)]
    public struct Optional<T>
    {
        /// <summary>
        /// The contained value.
        /// </summary>
        [SerializeReference]
        private T value;
        /// <summary>
        /// If there is an assigned value.
        /// </summary>
        [SerializeField]
        private bool hasValue;
        /// <summary>
        /// Construct with a usable value.
        /// </summary>
        /// 
        /// <param name="value">
        /// The value to assign.
        /// </param>
        public Optional(T value = default(T))
        {
            hasValue = true;
            this.value = value;
        }
        /// <summary>
        /// If there is a usable value.
        /// </summary>
        /// 
        /// <returns>
        /// True if there is a usable value. Otherwise false.
        /// </returns>
        public bool HasValue => hasValue;
        /// <summary>
        /// If there is no usable value.
        /// </summary>
        /// 
        /// <returns>
        /// True if there is no usable value. Otherwise false.
        /// </returns>
        public bool IsEmpty => !HasValue;
        /// <summary>
        /// Get or set the usable value. When assertions are enabled via
        /// UNITY_ASSERTIONS, getting it when there is no usable value
        /// results in an exception.
        /// </summary>
        /// 
        /// <returns>
        /// T if value exists.
        /// </returns>
        public T Value
        {
            get
            {
#if UNITY_ASSERTIONS
                Assert.IsTrue(HasValue);
#endif
                return value;
            }
            set
            {
                hasValue = true;
                this.value = value;
            }
        }
        /// <summary>
        /// Get the usable value or the default value if there is no usable value.
        /// </summary>
        /// 
        /// <returns>
        /// The usable value or the default value if there is no usable value.
        /// </returns>
        public T GetValueOrDefault() => value;
        /// <summary>
        /// Get the usable value or a default value if there is no usable value.
        /// </summary>
        /// 
        /// <param name="defaultValue">
        /// Value to return if there is no usable value.
        /// </param>
        /// 
        /// <returns>
        /// The usable value or <paramref name="defaultValue"/> if there is no
        /// usable value.
        /// </returns>
        public T GetValueOrDefault(T defaultValue) => HasValue ? value : defaultValue;
        /// <summary>
        /// Clear any usable value.
        /// </summary>
        public void Reset()
        {
            hasValue = false;
            value = default(T);
        }
        /// <summary>
        /// Get or set the usable value. When assertions are enabled via
        /// UNITY_ASSERTIONS and there is no usable value, an exception is thrown.
        /// </summary>
        /// 
        /// <param name="optional">
        /// Value to convert.
        /// </param>
        /// 
        /// <returns>
        /// True if there is a usable value. Otherwise false.
        /// </returns>
        public static explicit operator T(Optional<T> optional) => optional.Value;
        /// <summary>
        /// Construct with a usable value.
        /// </summary>
        /// 
        /// <param name="value">
        /// New instance with the assigned value.
        /// </param>
        public static explicit operator Optional<T>(T value) => new Optional<T>(value);
        /// <summary>
        /// Convert to a bool: (bool)opt.
        /// </summary>
        /// 
        /// <param name="optional">
        /// Value to convert.
        /// </param>
        /// 
        /// <returns>
        /// True if there is a usable value. Otherwise false.
        /// </returns>
        public static implicit operator bool(Optional<T> optional) => optional.HasValue;
        /// <summary>
        /// Convert to truth: if (opt).
        /// </summary>
        /// 
        /// <param name="optional">
        /// Value to convert.
        /// </param>
        /// 
        /// <returns>
        /// True if there is a usable value. Otherwise false.
        /// </returns>
        public static bool operator true(Optional<T> optional) => optional.HasValue;
        /// <summary>
        /// Convert to negative truth: if (!opt).
        /// </summary>
        /// 
        /// <param name="optional">
        /// Value to convert.
        /// </param>
        /// 
        /// <returns>
        /// True if there is no usable value. Otherwise false.
        /// </returns>
        public static bool operator false(Optional<T> optional) => !optional.HasValue;
        /// <returns>
        /// Return a new, empty instance.
        /// </returns>
        public static Optional<T> Empty() => new Optional<T>();
    }
}