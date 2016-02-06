using NSubstitute;
using NumberWorderApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberWorderTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Program_WhenCalledWithNoParameters_ShouldWriteToConsoleHelpMessage()
        {
            IConsole console = Substitute.For<IConsole>();
            Program.Console = console;
            Program.Main(new string[] { });

            console.Received().WriteLine(Program.UsageMessage);
        }

        [Test]
        public void Program_WhenCalledWithMoreThanOneParameter_ShouldWriteToConsoleHelpMessage()
        {
            IConsole console = Substitute.For<IConsole>();
            Program.Console = console;
            Program.Main(new string[] { "1234", "5678" });

            console.Received().WriteLine(Program.UsageMessageMoreThanOneParam);
        }

        [Test]
        public void Program_WhenCalledWithOneValidParameter_ShouldCallNumberWorderAndEchoResultToConsole()
        {
            IConsole console = Substitute.For<IConsole>();
            INumberWorder numberWorder = Substitute.For<INumberWorder>();

            var input = "1234";
            numberWorder.Parse(Arg.Any<string>()).Returns(input);

            Program.Console = console;
            Program.NumberWorder = numberWorder;

            
            Program.Main(new string[] { input });

            numberWorder.Received().Parse(input);
            console.Received().WriteLine(input);
        }

        [Test]
        public void Program_WhenCalledWithInvalidParameter_ShouldCatchExceptionAndEchoToConsole()
        {
            IConsole console = Substitute.For<IConsole>();
            INumberWorder numberWorder = Substitute.For<INumberWorder>();

            var input = "ABCD";
            var exceptionText = "fake exception text";
            numberWorder.When(a => a.Parse(input)).Do(ctx => { throw new Exception(exceptionText); } );

            Program.Console = console;
            Program.NumberWorder = numberWorder;
            
            Program.Main(new string[] { input });
            numberWorder.Received().Parse(input);
            console.Received().ReceivedWithAnyArgs().WriteLine(Arg.Any<string>());
            StringAssert.Contains(exceptionText,
                console.ReceivedCalls().ToList()[0].GetArguments()[0].ToString());

        }
    }
}
