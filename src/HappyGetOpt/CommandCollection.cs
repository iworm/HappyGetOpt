using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyGetOpt
{
    public class CommandCollection
    {
        readonly IList<ICommand> _commands = new List<ICommand>();

        public void Add(ICommand command)
        {
            _commands.Add(command);
        }

        public void Run(string[] args)
        {
            string commandName = GetCommandName(args);

            ICommand command = _commands.Single(c => c.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(commandName))
            {
                command.Run(OptionCollection.Parse(args, command.Options));
            }
            else
            {
                command.Run(OptionCollection.Parse(args.Skip(1), command.Options));
            }
        }

        private string GetCommandName(string[] args)
        {
            if (args.Length == 0)
            {
                return string.Empty;
            }

            if (args[0].StartsWith("-", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return args[0];
        }
    }
}
