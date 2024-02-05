using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;

namespace ExtensionMethods
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return true;
            }

            return false;
        }

        public static string Remove(this string text, params string[] characters)
        {
            try
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    text = text.Replace(characters[i], string.Empty);
                }

                return text;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static bool IsCNPJ(this string text)
        {
            if (text.IsEmpty() || text.Length != 14)
            {
                return false;
            }

            int[] multiplier1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

            int[] multiplier2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

            text = text.Trim();

            text = text.Remove(".", "-", "/");

            var cnpj = text[..12];

            var sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) * multiplier1[i];
            }

            var rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            var digit = rest.ToString();

            cnpj += digit;

            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) * multiplier2[i];
            }

            rest = (sum % 11);

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit += rest.ToString();

            return text.EndsWith(digit);
        }

        public static bool IsCPF(this string text)
        {
            if (text.IsEmpty() || text.Length != 11)
            {
                return false;
            }

            int[] multiplier1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];

            int[] multiplier2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

            text = text.Trim();

            text = text.Remove(".", "-");

            var cpf = text[..9];

            var sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * multiplier1[i];
            }

            var rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            var digit = rest.ToString();

            cpf += digit;

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * multiplier2[i];
            }

            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit += rest.ToString();

            return text.EndsWith(digit);
        }

        public static bool IsPIS(this string text)
        {
            if (text.IsEmpty() || text.Length != 11)
            {
                return false;
            }

            int[] multiplier = [3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

            text = text.Trim();

            text = text.Remove("-", ".").PadLeft(11, '0');

            var sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(text[i].ToString()) * multiplier[i];
            }

            var rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            return text.EndsWith(rest.ToString());
        }

        public static string AssemblyPath(this Assembly assembly)
        {
            return new Uri(assembly.Location).LocalPath;
        }

        public static string Extension(this string filename)
        {
            return Path.GetExtension(filename);
        }

        public static string MimeType(this string filename)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(filename, out string contentType);
            
            return contentType ?? MediaTypeNames.Application.Octet;
        }
    }
}