using System;

namespace HappyGetOpt
{
    public class ValueIsRequiredAfterOptionException : Exception
    {
        private readonly string _optionName;

        public ValueIsRequiredAfterOptionException(string optionName)
        {
            _optionName = optionName;
        }

        public string OptionName
        {
            get { return _optionName; }
        }
    }
}
