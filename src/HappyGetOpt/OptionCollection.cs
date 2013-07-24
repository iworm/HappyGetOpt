using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HappyGetOpt
{
    public class OptionCollection : IEnumerable<Option>
    {
        readonly IList<Option> _options = new List<Option>();

        public void Add(string longOptionName, char shortOptionName, IAction action, OptionRequired require, Following following)
        {
            Option option = new Option(longOptionName, shortOptionName, action, require, following);
            _options.Add(option);
        }

        public IEnumerator<Option> GetEnumerator()
        {
            return _options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Run()
        {
            var usedOptions = _options.Where(o => o.IsUsed);
            foreach (var option in usedOptions)
            {
                option.Run();
            }
        }
    }
}
