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

        public bool Run(string[] args)
        {
            string commandName = GetCommandName(args);

            ICommand command = Get(commandName);

            if (string.IsNullOrEmpty(commandName))
            {
                return command.Run(args);
            }

            return command.Run(args.Skip(1).ToArray());
        }

        private ICommand Get(string name)
        {
            var command = _commands.SingleOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (command == null)
            {
                throw new CommandNotFoundException(name);
            }

            return command;
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
