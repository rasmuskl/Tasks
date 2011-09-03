using NUnit.Framework;
using StoryQ;

namespace Tasks.Tests.StoryQ
{
    public class SampleFixture : AbstractStoryQFixture
    {
        public override Feature InitStory()
        {
            return new Story("test")
                .InOrderTo("test")
                .AsA("test")
                .IWant("test");
        }

        [Test]
        public void Test()
        {
            Scenario(s =>
                s.Given(Something)
                .When(Something)
                .Then(Something));
        }

        private void Something()
        {


        }
    }
}