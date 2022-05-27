using System.Collections.Generic;

namespace Reservanto.ApiClient.Responses
{
	internal abstract class DictionaryResponse<TKey, TValue> : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje hodnoty, které přijdou z API.
		/// (Kvůli neschopnému JavascriptSerializeru je nutné mít typované string, object)
		/// </summary>
		public Dictionary<object, TValue> Values
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací dictionary, která má už odpovídající typy klíče a hodnoty.
		/// </summary>
		public Dictionary<TKey, TValue> TypedValues
		{
			get
			{
				var result = new Dictionary<TKey, TValue>();
				foreach (var keyValue in this.Values)
				{
					result.Add(ConvertKey(keyValue.Key), keyValue.Value);
				}
				return result;
			}
		}

		protected abstract TKey ConvertKey(object key);
	}

	internal class StringKeyDictionaryResponse<TValue> : DictionaryResponse<string, TValue>
	{
		protected override string ConvertKey(object key)
		{
			return key.ToString();
		}
	}

	internal class IntKeyDictionaryResponse<TValue> : DictionaryResponse<int, TValue>
	{
		protected override int ConvertKey(object key)
		{
			return int.Parse(key.ToString());
		}
	}
}
