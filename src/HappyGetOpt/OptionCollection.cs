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

        public static OptionCollection Parse(IEnumerable<string> args, OptionCollection options)
        {
            var argsEnumerator = args.GetEnumerator();
            
            while (argsEnumerator.MoveNext())
            {
                string arg = argsEnumerator.Current;

                Option option = options.SingleOrDefault(o => o.Match(arg));

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

            Option missingRequiredOption = options.SingleOrDefault(o => o.IsRequired && !o.IsUsed);
            if (missingRequiredOption != null)
            {
                throw new MissingRequiredOptionException(missingRequiredOption.Name);
            }

            return options;
        }
    }
}
