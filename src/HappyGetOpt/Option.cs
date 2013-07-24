using System;
using System.Globalization;

namespace HappyGetOpt
{
    public delegate void RunOptionDelegate(string value);

    public class Option
    {
        private readonly string _longOptionName;
        private readonly char _shortOptionName;
        private readonly RunOptionDelegate _action;
        private readonly OptionRequired _require;
        private readonly Following _following;

        public Option(string longOptionName, char shortOptionName,  OptionRequired require, Following following)
        {
            _longOptionName = longOptionName;
            _shortOptionName = shortOptionName;
            _require = require;
            _following = following;
        }

        public bool IsFollowingByValue
        {
            get { return _following == Following.Value; }
        }

        public bool IsUsed { get; set; }

        public string Value { get; set; }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_longOptionName))
                {
                    return _shortOptionName.ToString(CultureInfo.InvariantCulture);
                }

                if (string.IsNullOrEmpty(_shortOptionName.ToString(CultureInfo.InvariantCulture)))
                {
                    return _longOptionName;
                }
                
                return _longOptionName + "/" + _shortOptionName;
            }
        }

        public bool IsRequired
        {
            get { return _require == OptionRequired.Required; }
        }

        public bool Match(string optionName)
        {
            string realOptionName = optionName.TrimStart('-');

            if (_longOptionName.Equals(realOptionName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (_shortOptionName.ToString(CultureInfo.InvariantCulture).Equals(realOptionName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public void Run()
        {
            _action(Value.Trim('\"', '\''));
        }
    }
}
