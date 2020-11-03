using Mpesa.SDK.AspNetCore.Callbacks;
using System;
using System.Buffers;
using System.Text;

namespace Mpesa.SDK.AspNetCore.Extensions
{
    public static class StringExtensions
    {
        public static Balance GetBalance(this string[] balances)
        {
            return new Balance
            {
                Current = double.Parse(balances[2]),
                Available = double.Parse(balances[3]),
                Reserved = double.Parse(balances[4]),
                Unclear = double.Parse(balances[5])
            };
        }

        public static string GetString(this ReadOnlySequence<byte> sequence)
        {
            var decoder = Encoding.UTF8.GetDecoder();
            var sb = new StringBuilder();
            var processed = 0L;
            var total = sequence.Length;
            foreach (var item in sequence)
            {
                processed += item.Length;
                var isLast = processed == total;
                var span = item.Span;
                var charCount = decoder.GetCharCount(span, isLast);
                Span<char> buffer = stackalloc char[charCount];
                decoder.GetChars(span, buffer, isLast);
                sb.Append(buffer);
            }

            return sb.ToString();
        }
    }
}
