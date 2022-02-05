using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Turtle.Graphics
{
	public class FormattedString
	{
		public const char ANSI_PREFIX = '\x1b';
		public readonly StringBuilder RESET_FORMAT = new Format ().GetFormatBuilder();

		public string RawValue { get; set; }

		public Dictionary<Regex, Format> Formats { get; set; } = new ();

		public FormattedString (string value)
		{
			this.RawValue = value;
		}

		public static implicit operator string(FormattedString s)
		{
			// TODO
			StringBuilder sb = new ();

			foreach (var line in s.RawValue.Split (Environment.NewLine)) {
				sb.AppendLine (s.formatLine (line));
			}

			return sb.ToString ();
		}

		public static implicit operator FormattedString(string s)
		{
			return new FormattedString (s);
		}

		private string formatLine(string line)
		{
			foreach (var format in this.Formats) {
				format.Key.Replace(line, m => {
					var fmtBuilder = format.Value.GetFormatBuilder ();

					fmtBuilder.Insert (0, ANSI_PREFIX);
					fmtBuilder.Append (m.Value);
					fmtBuilder.Append (ANSI_PREFIX);
					fmtBuilder.Append (RESET_FORMAT);

					return fmtBuilder.ToString ();
				});
			}

			return line;
		}
	}
}

