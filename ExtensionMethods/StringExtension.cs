﻿using System;
using System.IO;
using System.Reflection;

namespace ExtensionMethods
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
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

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum = 0, rest;

            string digit, CNPJ;

            text = text.Trim();

            text = text.Remove(".", "-", "/");

            CNPJ = text.Substring(0, 12);

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(CNPJ[i].ToString()) * multiplier1[i];
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

            digit = rest.ToString();

            CNPJ = CNPJ + digit;

            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(CNPJ[i].ToString()) * multiplier2[i];
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

            digit = digit + rest.ToString();

            return text.EndsWith(digit);
        }

        public static bool IsCPF(this string text)
        {
            if (text.IsEmpty() || text.Length != 11)
            {
                return false;
            }

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string CPF, digit;

            int sum = 0, rest;

            text = text.Trim();

            text = text.Remove(".", "-");

            CPF = text.Substring(0, 9);

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(CPF[i].ToString()) * multiplier1[i];
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

            digit = rest.ToString();

            CPF = CPF + digit;

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(CPF[i].ToString()) * multiplier2[i];
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

            digit = digit + rest.ToString();

            return text.EndsWith(digit);
        }

        public static bool IsPIS(this string text)
        {
            if (text.IsEmpty() || text.Length != 11)
            {
                return false;
            }

            int[] multiplier = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum = 0, rest;

            text = text.Trim();

            text = text.Remove("-", ".").PadLeft(11, '0');

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(text[i].ToString()) * multiplier[i];
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

            return text.EndsWith(rest.ToString());
        }

        public static string AssemblyPath(this Assembly assembly)
        {
            return new Uri(assembly.CodeBase).LocalPath;
        }

        public static string Extension(this string text)
        {
            return Path.GetExtension(text);
        }
    }
}