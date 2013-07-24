using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyGetOpt
{
    public class CommandCollection
    {
        readonly IList<Command> _commands = new List<Command>();

        public void Add(Command command)
        {
            _commands.Add(command);
        }

        public void Run(string[] args)
        {
            string commandName = GetCommandName(args);

            Command command = _commands.Single(c => c.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(commandName))
            {
                command.Run(args);
            }
            else
            {
                command.Run(args.Skip(1));
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
