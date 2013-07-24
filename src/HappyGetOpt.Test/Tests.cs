using Moq;
using NUnit.Framework;

namespace HappyGetOpt.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestShortOptionWillNoValueAfter()
        {
            var mock = new Mock<IAction>();
            mock.Setup(m => m.Run(string.Empty));

            Command defaultCommand = new Command(string.Empty);

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("help", 'h', mock.Object, OptionRequired.Optional, Following.None);

            const string args = "-h";
            commandCollection.Run(args.Split(' '));
            mock.Verify(action => action.Run(string.Empty), Times.Exactly(1));
        }

        [Test]
        public void TestLongOptionWillNoValueAfter()
        {
            var mock = new Mock<IAction>();
            mock.Setup(m => m.Run(string.Empty));

            Command defaultCommand = new Command(string.Empty);

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("help", 'h', mock.Object, OptionRequired.Optional, Following.None);

            const string args = "--help";
            commandCollection.Run(args.Split(' '));
            mock.Verify(action => action.Run(string.Empty), Times.Exactly(1));
        }

        [Test]
        public void TestShortOptionMustHaveValueAfter()
        {
            var mock = new Mock<IAction>();
            mock.Setup(m => m.Run("abc"));

            Command defaultCommand = new Command(string.Empty);

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', mock.Object, OptionRequired.Optional, Following.Value);

            const string args = "-m abc";
            commandCollection.Run(args.Split(' '));
            mock.Verify(action => action.Run("abc"), Times.Exactly(1));
        }

        [Test]
        public void TestShortOptionValueHasSpace()
        {
            var mock = new Mock<IAction>();
            mock.Setup(m => m.Run("abc def"));

            Command defaultCommand = new Command(string.Empty);

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', mock.Object, OptionRequired.Optional, Following.Value);

            string[] args = new string[]{"-m", "\"abc def\""};
            commandCollection.Run(args);
            mock.Verify(action => action.Run("abc def"), Times.Exactly(1));
        }

        [Test]
        [ExpectedException(typeof(OptionNotFoundException))]
        public void TestShortOptionWillNotHaveValueAfterButValueExists()
        {
            var mock = new Mock<IAction>();
            mock.Setup(m => m.Run(string.Empty));

            Command defaultCommand = new Command(string.Empty);

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', mock.Object, OptionRequired.Optional, Following.None);

            const string args = "-m abc";
            commandCollection.Run(args.Split(' '));
            mock.Verify(action => action.Run(string.Empty), Times.Exactly(1));
        }

        [Test]
        [ExpectedException(typeof(MissingRequiredOptionException))]
        public void TestShortOptionIsRequired()
        {
            var mock = new Mock<IAction>();
            mock.Setup(m => m.Run(string.Empty));

            Command defaultCommand = new Command(string.Empty);

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', mock.Object, OptionRequired.Required, Following.None);
            defaultCommand.Options.Add("check", 'c', mock.Object, OptionRequired.Optional, Following.None);

            const string args = "-c";
            commandCollection.Run(args.Split(' '));
        }
    }
}
