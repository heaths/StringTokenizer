// Copyright 2020 Heath Stewart.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// Derived from https://github.com/dotnet/extensions/blob/master/src/Primitives/src/StringTokenizer.cs
// licensed under the Apache License, version 2.0. See ThirdPartyNotices.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

internal readonly struct StringTokenizer : IEnumerable<StringSegment>
{
    private readonly StringSegment _value;
    private readonly char[] _separators;
    private readonly StringSplitOptions _options;

    public StringTokenizer(string value, char[] separators, StringSplitOptions options = default)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _separators = separators ?? throw new ArgumentNullException(nameof(separators));
        _options = options;
    }

    public StringTokenizer(StringSegment value, char[] separators, StringSplitOptions options = default)
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _value = value;
        _separators = separators ?? throw new ArgumentNullException(nameof(separators));
        _options = options;
    }

    public Enumerator GetEnumerator() => new Enumerator(in _value, _separators, _options);

    IEnumerator<StringSegment> IEnumerable<StringSegment>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<StringSegment>
    {
        private readonly StringSegment _value;
        private readonly char[] _separators;
        private readonly StringSplitOptions _options;
        private int _index;

        public Enumerator(ref StringTokenizer tokenizer)
        {
            _value = tokenizer._value;
            _separators = tokenizer._separators;
            _options = tokenizer._options;

            Current = default;
            _index = 0;
        }

        internal Enumerator(in StringSegment value, char[] separators, StringSplitOptions options)
        {
            _value = value;
            _separators = separators;
            _options = options;

            Current = default;
            _index = 0;
        }

        public StringSegment Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (!_value.HasValue || _index > _value.Length)
            {
                Current = default;
                return false;
            }

            if ((_options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.RemoveEmptyEntries)
            {
                _value.Skip(ref _index, _separators);
                if (_index == _value.Length)
                {
                    return false;
                }
            }

            int next = _value.IndexOfAny(_separators, _index);
            if (next == -1)
            {
                next = _value.Length;
            }

            Current = _value.Subsegment(_index, next - _index);
            _index = next + 1;

            return true;
        }

        public unsafe bool UnsafeMoveNext()
        {
            if (!_value.HasValue || _index > _value.Length)
            {
                Current = default;
                return false;
            }

            if ((_options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.RemoveEmptyEntries)
            {
                _value.UnsafeSkip(ref _index, _separators);
                if (_index == _value.Length)
                {
                    return false;
                }
            }

            int next = _value.IndexOfAny(_separators, _index);
            if (next == -1)
            {
                next = _value.Length;
            }

            Current = _value.Subsegment(_index, next - _index);
            _index = next + 1;

            return true;
        }

        public void Reset()
        {
            Current = default;
            _index = 0;
        }
    }
}
