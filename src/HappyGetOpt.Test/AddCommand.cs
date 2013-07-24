using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGetOpt.Test
{
    public class AddCommand : ICommand
    {
        private OptionCollection _options = new OptionCollection();
        private string _commandName;

        public AddCommand(string commandName)
        {
            _commandName = commandName;
        }

        public void Run(OptionCollection options)
        {
            
        }

        public string Name
        {
            get { return _commandName; }
        }

        public OptionCollection Options
        {
            get { return _options; }
        }
    }
}
