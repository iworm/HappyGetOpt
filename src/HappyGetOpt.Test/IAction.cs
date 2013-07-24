using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyGetOpt.Test
{
    public interface IAction
    {
        void Run(string value);
    }
}
