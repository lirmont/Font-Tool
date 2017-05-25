using System.Collections.Generic;
using System.Collections;
using System;
using System.Reflection;

namespace FontTool
{
	public partial class SupportFunctions
	{
		public static int CompareKeyValuePairByBlockCode(KeyValuePair<UnicodeBlock, List<ulong>> a, KeyValuePair<UnicodeBlock, List<ulong>> b)
		{
			if (a.Key.Start > b.Key.Start)
				return 1;
			else if (a.Key.Start < b.Key.Start)
				return -1;
			else
				return 0;
		}

		public static bool Equivalent(IList A, IList B)
		{
			if (A.Count != B.Count)
				return false;
			//
			if (A.Count > 0)
			{
				for (int i = 0; i < A.Count; i++)
				{
					Type TypeA = A[i].GetType();
					Type TypeB = B[i].GetType();
					if (TypeA != TypeB)
						return false;
					else if (!A[i].Equals(B[i]))
						return false;
				}
			}
			return true;
		}

		public static List<Type> GetInheritingTypes(Type type)
		{
			Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			List<Type> typesThatInherit = new List<Type>();
			foreach (Type thisType in types)
				if (thisType.IsSubclassOf(type))
					typesThatInherit.Add(thisType);
			return typesThatInherit;
		}

		public static String TitleCaseString(String s)
		{
			if (s == null) return s;
			String[] words = s.Split(' ');
			for (int i = 0; i < words.Length; i++)
			{
				if (words[i].Length == 0) continue;
				Char firstChar = Char.ToUpper(words[i][0]);
				String rest = "";
				if (words[i].Length > 1)
					rest = words[i].Substring(1).ToLower();
				words[i] = firstChar + rest;
			}
			return String.Join(" ", words);
		}
	}
}
