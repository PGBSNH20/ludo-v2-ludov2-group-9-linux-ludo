using Xunit;
using LinuxLudo.API;

namespace LinuxLudo.Test.Game
{
    public class GameEngineTest
    {
        private readonly GameEngine engine;
        public GameEngineTest() => engine = new GameEngine();

        [Fact]
        public void RollDice_Between_Range()
        {
            int roll = engine.RollDice();
            Assert.InRange(roll, 0, 6);
        }
    }
}