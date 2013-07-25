using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGetOpt
{
    public class CommandNotFoundException : Exception
    {
        private readonly string _name;

        public CommandNotFoundException(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
