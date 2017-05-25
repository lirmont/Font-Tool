using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class ParameterCollection : KeyedCollection<string, EffectParameter>
	{
		protected override string GetKeyForItem(EffectParameter item)
		{
			return item.Name;
		}

		protected override void InsertItem(int index, EffectParameter item)
		{
			if (base.Dictionary != null && base.Dictionary.ContainsKey(item.Name))
			{
				int thisIndex = 0;
				foreach (EffectParameter parameter in base.Items)
				{
					if (base[thisIndex].Name == item.Name)
					{
						base[thisIndex] = item;
						return;
					}
					thisIndex++;
				}
			}
			else
				base.InsertItem(index, item);
		}

		public override string ToString()
		{
			string parameterList = "";
			if (base.Dictionary != null && base.Dictionary.Keys.Count > 0)
			{
				List<string> parameters = new List<string>();
				foreach (string key in base.Dictionary.Keys)
					parameters.Add(string.Format("{0}: {1}", key, base[key].Value));
				parameterList = string.Join("; ", parameters.ToArray());
			}
			return parameterList;
		}
	}
}
