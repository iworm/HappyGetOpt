using System;

namespace HappyGetOpt
{
    public class MissingRequiredOptionException : Exception
    {
        private readonly string _optionName;

        public MissingRequiredOptionException(string optionName)
        {
            _optionName = optionName;
        }

        public string OptionName
        {
            get { return _optionName; }
        }
    }
}
