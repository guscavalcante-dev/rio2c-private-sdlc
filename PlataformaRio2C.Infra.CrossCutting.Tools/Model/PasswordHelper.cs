using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Model
{
    public static class PasswordHelper
    {
        private const string UPCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LOWCASE = "abcdefghijklmnopqrstuvwxyz";
        private const string NUMERIC = "0123456789";
        private const string SPECIAL = "@#$&*";

        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
#pragma warning disable CSE0001 // Use nameof when passing parameter names as arguments
                throw new ArgumentException("length must not be negative", "length");
#pragma warning restore CSE0001 // Use nameof when passing parameter names as arguments
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
#pragma warning disable CSE0001 // Use nameof when passing parameter names as arguments
                throw new ArgumentException("length is too big", "length");
#pragma warning restore CSE0001 // Use nameof when passing parameter names as arguments
            if (characterSet == null)
#pragma warning disable CSE0001 // Use nameof when passing parameter names as arguments
                throw new ArgumentNullException("characterSet");
#pragma warning restore CSE0001 // Use nameof when passing parameter names as arguments
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
#pragma warning disable CSE0001 // Use nameof when passing parameter names as arguments
                throw new ArgumentException("characterSet must not be empty", "characterSet");
#pragma warning restore CSE0001 // Use nameof when passing parameter names as arguments

            var bytes = new byte[length * 8];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }

        public static string GetNewRandomPassword(int length, bool UpcaseRequired = true, bool LowCaseRequired = true, bool NumericRequired = true, bool EspecialCharRequerid = true)
        {
            string characterSet = string.Empty;

            if (UpcaseRequired) characterSet += UPCASE;
            if (LowCaseRequired) characterSet += LOWCASE;
            if (NumericRequired) characterSet += NUMERIC;
            if (EspecialCharRequerid) characterSet += SPECIAL;

            bool invalid = false;
            string newPass;
            do
            {
                newPass = GetRandomString(length, characterSet);
                if (UpcaseRequired)
                {
                    if (!UPCASE.Any(e => newPass.Any(c => c == e)))
                    {
                        invalid = true;
                        continue;
                    }
                }

                if (EspecialCharRequerid)
                {
                    if (!SPECIAL.Any(e => newPass.Any(c => c == e)))
                    {
                        invalid = true;
                        continue;
                    }
                }

                if (LowCaseRequired)
                {
                    if (!LOWCASE.Any(e => newPass.Any(c => c == e)))
                    {
                        invalid = true;
                        continue;
                    }
                }

                if (NumericRequired)
                {
                    if (!NUMERIC.Any(e => newPass.Any(c => c == e)))
                    {
                        invalid = true;
                        continue;
                    }
                }

                invalid = false;

            } while (invalid);

            return newPass;
        }
    }
}
