using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGetOpt
{
    public interface ICommand
    {
        void Run(OptionCollection options);
        string Name { get;}
        OptionCollection Options { get; }
    }
}
