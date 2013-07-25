using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGetOpt
{
    public interface ICommand
    {
        bool Run(string[] args);
        string Name { get;}
        OptionCollection Options { get; }
    }
}
