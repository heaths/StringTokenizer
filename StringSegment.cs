// Copyright 2020 Heath Stewart.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// Derived from https://github.com/dotnet/extensions/blob/master/src/Primitives/src/StringSegment.cs
// licensed under the Apache License, version 2.0. See ThirdPartyNotices.txt in the project root for license information.

using System;
using System.Runtime.CompilerServices;

internal readonly struct StringSegment : IEquatable<StringSegment>, IEquatable<string>
{
    public static readonly StringSegment Empty = string.Empty;

    public StringSegment(string buffer)
    {
        Buffer = buffer;
        Offset = 0;
        Length = buffer?.Length ?? 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public StringSegment(string buffer, int offset, int length)
    {
        // TODO: Throw argument exceptions.

        Buffer = buffer;
        Offset = offset;
        Length = length;
    }

    public string Buffer { get; }

    public int Offset { get; }

    public int Length { get; }

    public string Value => HasValue ? Buffer.Substring(Offset, Length) : null;

    public bool HasValue => Buffer != null;

    public char this[int index]
    {
        get
        {
            if ((uint)index >= (uint)Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return Buffer[Offset + index];
        }
    }

    public static bool IsNullOrEmpty(StringSegment value) => !value.HasValue || value.Length == 0;

    public static bool operator ==(StringSegment left, StringSegment right) => left.Equals(right);

    public static bool operator !=(StringSegment left, StringSegment right) => !left.Equals(right);

    public static implicit operator StringSegment(string value) => new StringSegment(value);

    public static implicit operator ReadOnlySpan<char>(StringSegment value) => value.AsSpan();

    public static implicit operator ReadOnlyMemory<char>(StringSegment value) => value.AsMemory();

    public ReadOnlySpan<char> AsSpan() => Buffer.AsSpan(Offset, Length);

    public ReadOnlyMemory<char> AsMemory() => Buffer.AsMemory(Offset, Length);

    public int IndexOfAny(char[] separators, int startIndex = 0) => IndexOfAny(separators, startIndex, Length - startIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOfAny(char[] separators, int startIndex, int count)
    {
        int index = -1;

        if (HasValue)
        {
            if (startIndex < 0 || Offset + startIndex > Buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (count < 0 || Offset + startIndex + count > Buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            index = Buffer.IndexOfAny(separators, Offset + startIndex, count);
            if (index != -1)
            {
                index -= Offset;
            }
        }

        return index;
    }

    public StringTokenizer Split(char[] separators, StringSplitOptions options = default) => new StringTokenizer(this, separators, options);

    public StringSegment Subsegment(int offset) => Subsegment(offset, Length - offset);

    public StringSegment Subsegment(int offset, int length)
    {
        // TODO: Throw argument exceptions.

        return new StringSegment(Buffer, Offset + offset, length);
    }

    public StringSegment Trim() => TrimStart().TrimEnd();

    public StringSegment UnsafeTrim() => UnsafeTrimStart().UnsafeTrimEnd();

    public StringSegment TrimStart()
    {
        int trimmedStart = Offset;
        int length = Offset + Length;

        while (trimmedStart < length)
        {
            char c = Buffer[trimmedStart];
            if (!char.IsWhiteSpace(c))
            {
                break;
            }

            trimmedStart++;
        }

        return new StringSegment(Buffer, trimmedStart, length - trimmedStart);
    }

    public unsafe StringSegment UnsafeTrimStart()
    {
        int trimmedStart = Offset;
        int length = Offset + Length;

        fixed (char* p = Buffer)
        {
            while (trimmedStart < length)
            {
                char c = p[trimmedStart];
                if (!char.IsWhiteSpace(c))
                {
                    break;
                }

                trimmedStart++;
            }
        }

        return new StringSegment(Buffer, trimmedStart, length - trimmedStart);
    }

    public StringSegment TrimEnd()
    {
        int offset = Offset;
        int trimmedEnd = offset + Length - 1;

        while (trimmedEnd >= offset)
        {
            char c = Buffer[trimmedEnd];
            if (!char.IsWhiteSpace(c))
            {
                break;
            }

            trimmedEnd--;
        }

        return new StringSegment(Buffer, offset, trimmedEnd- offset + 1);
    }

    public unsafe StringSegment UnsafeTrimEnd()
    {
        int offset = Offset;
        int trimmedEnd = offset + Length - 1;

        fixed (char* p = Buffer)
        {
            while (trimmedEnd >= offset)
            {
                char c = p[trimmedEnd];
                if (!char.IsWhiteSpace(c))
                {
                    break;
                }

                trimmedEnd--;
            }
        }

        return new StringSegment(Buffer, offset, trimmedEnd- offset + 1);
    }

    public static bool Equals(StringSegment left, StringSegment right, StringComparison comparisonType) => left.Equals(right, comparisonType);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is StringSegment value && Equals(value);
    }

    public bool Equals(string other) => Equals(other, StringComparison.Ordinal);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(string other, StringComparison comparisonType)
    {
        if (other is null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        int length = other.Length;
        if (!HasValue || Length != length)
        {
            return false;
        }

        return string.Compare(Buffer, Offset, other, 0, length, comparisonType) == 0;
    }

    public bool Equals(StringSegment other) => Equals(other, StringComparison.Ordinal);

    public bool Equals(StringSegment other, StringComparison comparisonType)
    {
        if (Length != other.Length)
        {
            return false;
        }

        return string.Compare(Buffer, Offset, other.Buffer, other.Offset, other.Length, comparisonType) == 0;
    }

    public override int GetHashCode() => Value?.GetHashCode() ?? 0;

    public override string ToString() => Value ?? string.Empty;

    internal void Skip(ref int index, char[] skipChars)
    {
        while (index < Offset + Length)
        {
            bool found = false;
            char c = Buffer[index];

            for (int i = 0; i < skipChars.Length; ++i)
            {
                if (c == skipChars[i])
                {
                    found = true;
                    index++;

                    break;
                }
            }

            if (!found)
            {
                break;
            }
        }
    }

    internal unsafe void UnsafeSkip(ref int index, char[] skipChars)
    {
        fixed (char* p = Buffer)
        {
            while (index < Offset + Length)
            {
                bool found = false;
                char c = p[index];

                for (int i = 0; i < skipChars.Length; ++i)
                {
                    if (c == skipChars[i])
                    {
                        found = true;
                        index++;

                        break;
                    }
                }

                if (!found)
                {
                    break;
                }
            }
        }
    }
}
