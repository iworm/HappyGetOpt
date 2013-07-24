using System;
using System.Globalization;

namespace HappyGetOpt
{
    public class Option
    {
        private readonly string _longOptionName;
        private readonly char _shortOptionName;
        private readonly IAction _action;
        private readonly OptionRequired _require;
        private readonly Following _following;

        public Option(string longOptionName, char shortOptionName, IAction action, OptionRequired require, Following following)
        {
            _longOptionName = longOptionName;
            _shortOptionName = shortOptionName;
            _action = action;
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
            _action.Run(Value.Trim('\"', '\''));
        }
    }
}
