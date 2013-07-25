using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HappyGetOpt
{
    public class OptionCollection : IEnumerable<Option>
    {
        readonly IList<Option> _options = new List<Option>();

        public void Add(string longOptionName, char shortOptionName, OptionRequired require, Following following)
        {
            Option option = new Option(longOptionName, shortOptionName, require, following);
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

        public Option Get(string name)
        {
            var option = _options.SingleOrDefault(o => o.Match(name));

            if (option == null)
            {
                throw new OptionNotFoundException(name);
            }

            return option;
        }

        public void Parse(IEnumerable<string> args)
        {
            var argsEnumerator = args.GetEnumerator();
            
            while (argsEnumerator.MoveNext())
            {
                string arg = argsEnumerator.Current;

                Option option = Get(arg);

                if (option == null)
                {
                    throw new OptionNotFoundException(arg);
                }

                option.IsUsed = true;

                if (option.IsFollowingByValue)
                {
                    if (!argsEnumerator.MoveNext())
                    {
                        throw new ValueIsRequiredAfterOptionException(arg);
                    }

                    option.Value = argsEnumerator.Current;
                }
                else
                {
                    option.Value = string.Empty;
                }
            }

            Option missingRequiredOption = this.SingleOrDefault(o => o.IsRequired && !o.IsUsed);
            if (missingRequiredOption != null)
            {
                throw new MissingRequiredOptionException(missingRequiredOption.Name);
            }
        }
    }
}
