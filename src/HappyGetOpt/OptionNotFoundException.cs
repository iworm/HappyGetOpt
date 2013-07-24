using System;

namespace HappyGetOpt
{
    public class OptionNotFoundException : Exception
    {
        private readonly string _optionName;

        public OptionNotFoundException(string optionName)
        {
            _optionName = optionName;
        }

        public string OptionName
        {
            get { return _optionName; }
        }
    }
}
