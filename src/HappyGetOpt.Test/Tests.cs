using System.Collections.Generic;
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
            var mock = new Mock<ICommand>();
            mock.SetupGet(c => c.Name).Returns(string.Empty);
            mock.SetupGet(c => c.Options).Returns(new OptionCollection());
            mock.Setup(m => m.Run(It.IsAny<string[]>()));

            ICommand defaultCommand = mock.Object;

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("help", 'h', OptionRequired.Optional, Following.None);

            const string args = "-h";
            commandCollection.Run(args.Split(' '));

            mock.Verify(command => command.Run(It.IsAny<string[]>()), Times.Exactly(1));
        }

        [Test]
        public void TestLongOptionWillNoValueAfter()
        {
            var mock = new Mock<ICommand>();
            mock.SetupGet(c => c.Name).Returns(string.Empty);
            mock.SetupGet(c => c.Options).Returns(new OptionCollection());
            mock.Setup(m => m.Run(It.IsAny<string[]>()));

            ICommand defaultCommand = mock.Object;

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("help", 'h', OptionRequired.Optional, Following.None);

            const string args = "--help";
            commandCollection.Run(args.Split(' '));
            mock.Verify(action => action.Run(It.IsAny<string[]>()), Times.Exactly(1));
        }

        [Test]
        public void TestShortOptionMustHaveValueAfter()
        {
            var mock = new Mock<ICommand>();
            mock.SetupGet(c => c.Name).Returns(string.Empty);
            mock.SetupGet(c => c.Options).Returns(new OptionCollection());
            mock.Setup(m => m.Run(It.IsAny<string[]>()));

            ICommand defaultCommand = mock.Object;

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', OptionRequired.Optional, Following.Value);

            const string args = "-m abc";
            commandCollection.Run(args.Split(' '));
            mock.Verify(action => action.Run(It.IsAny<string[]>()), Times.Exactly(1));
        }

        [Test]
        public void TestShortOptionValueHasSpace()
        {
            var mock = new Mock<ICommand>();
            mock.SetupGet(c => c.Name).Returns(string.Empty);
            mock.SetupGet(c => c.Options).Returns(new OptionCollection());
            mock.Setup(m => m.Run(It.IsAny<string[]>()));

            ICommand defaultCommand = mock.Object;

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', OptionRequired.Optional, Following.Value);

            string[] args = new []{"-m", "\"abc def\""};
            commandCollection.Run(args);
            mock.Verify(action => action.Run(It.IsAny<string[]>()), Times.Exactly(1));
        }

        [Test]
        [ExpectedException(typeof(OptionNotFoundException))]
        public void TestShortOptionWillNotHaveValueAfterButValueExists()
        {
            var mock = new Mock<ICommand>();
            mock.SetupGet(c => c.Name).Returns(string.Empty);
            mock.SetupGet(c => c.Options).Returns(new OptionCollection());
            mock.Setup(m => m.Run(It.IsAny<string[]>()));

            ICommand defaultCommand = mock.Object;

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', OptionRequired.Optional, Following.None);

            const string args = "-m abc";
            defaultCommand.Options.Parse(args.Split(' '));
        }

        [Test]
        [ExpectedException(typeof(MissingRequiredOptionException))]
        public void TestShortOptionIsRequired()
        {
            var mock = new Mock<ICommand>();
            mock.SetupGet(c => c.Name).Returns(string.Empty);
            mock.SetupGet(c => c.Options).Returns(new OptionCollection());
            mock.Setup(m => m.Run(It.IsAny<string[]>()));

            ICommand defaultCommand = mock.Object;

            CommandCollection commandCollection = new CommandCollection();
            commandCollection.Add(defaultCommand);

            defaultCommand.Options.Add("message", 'm', OptionRequired.Required, Following.None);
            defaultCommand.Options.Add("check", 'c', OptionRequired.Optional, Following.None);

            const string args = "-c";
            defaultCommand.Options.Parse(args.Split(' '));
        }
    }
}
