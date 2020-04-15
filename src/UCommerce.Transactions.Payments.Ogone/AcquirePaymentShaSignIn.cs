﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ucommerce.Transactions.Payments.Ogone
{
	/// <summary>
	/// Class used to build a string to hash when a payment needs to be acquired.
	/// Ogone needs a "sha sign" which is a signature used to verify that the data is identically in both ends.
	/// The "sha sign" is a sha-1 hash of the string this class builds.
	/// </summary>
	public class AcquirePaymentShaSignIn
	{

		/// <summary>
		/// Builds the string to hash when the merchant makes an acquire request at Ogone.
		/// Method is used in AcquirePaymentInternal at the PaymentMethodService.
		/// </summary>
		/// <param name="dict">The dict.</param>
		/// <param name="shaSignOut">The sha sign out.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public string BuildHashString(Dictionary<string, string> dict, string shaSignOut)
		{
			var concatString = new StringBuilder();

			var shaSign = shaSignOut;

			foreach (var key in dict.OrderBy(a => a.Key))
			{
				concatString.Append(BuildStringSection(key.Key, key.Value, shaSign));
			}
			return concatString.ToString();
		}

		/// <summary>
		/// Builds a section of the string to hash. 
		/// Method helps to match the rules of building a sha sign given by Ogone.
		/// Keys must be in upper and empty values must be ignored.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		/// <param name="shaSign">The sha sign.</param>
		/// <returns>A string section in "KEY=value" format.</returns>
		private string BuildStringSection(string parameter, string value, string shaSign)
		{
			if (string.IsNullOrEmpty(value))
				return "";
			return parameter.ToUpper() + "=" + value + shaSign;
		}
	}
}
