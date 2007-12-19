using Eft.Interpreter;
using NUnit.Framework;

namespace FunctionalTest.Interpreter
{
    [TestFixture]
    public class ServerTest
    {
        [Test]
        public void should_be_able_to_launch_and_quit_uat_through_server()
        {
            Server server = new Server("localhost", 8080);
            server.Start();
            server.Stop();
        }
    }
}