using System.Collections.Generic;
using System.Linq;

namespace HappyGetOpt
{
    public class Command
    {
        readonly OptionCollection _options = new OptionCollection();
        private readonly string _commandName;

        public OptionCollection Options
        {
            get { return _options; }
        }

        public string Name
        {
            get { return _commandName; }
        }

        public Command(string commandName)
        {
            _commandName = commandName;
        }

        public void Run(IEnumerable<string> args)
        {
            var argsEnumerator = args.GetEnumerator();
            
            while (argsEnumerator.MoveNext())
            {
                string arg = argsEnumerator.Current;

                Option option = _options.SingleOrDefault(o => o.Match(arg));

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

            Option missingRequiredOption = _options.SingleOrDefault(o => o.IsRequired && !o.IsUsed);
            if (missingRequiredOption != null)
            {
                throw new MissingRequiredOptionException(missingRequiredOption.Name);
            }

            _options.Run();
        }
    }
}
