using System;
using System.Text.RegularExpressions;

namespace Turtle.Graphics
{
	public class FormattedString
	{
		public string RawValue { get; set; }

		public Dictionary<Regex, Format> Formats { get; set; }

		public FormattedString (string value)
		{
			this.RawValue = value;
		}

		public static implicit operator string(FormattedString s)
		{
			// TODO
			return null;
		}

		public static implicit operator FormattedString(string s)
		{
			return new FormattedString (s);
		}

		private string formatLine(string line)
		{
		}
	}
}

