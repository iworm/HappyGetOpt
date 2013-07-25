using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGetOpt
{
    public class HelpCommand : ICommand
    {
        private readonly OptionCollection _options = new OptionCollection();

        public bool Run(string[] args)
        {
            _options.Parse(args);
            return true;
        }

        public string Name
        {
            get { return "help"; }
        }

        public OptionCollection Options
        {
            get { return _options; }
        }
    }
}
