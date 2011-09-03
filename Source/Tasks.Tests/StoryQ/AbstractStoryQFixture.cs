using System;
using StoryQ;

namespace Tasks.Tests.StoryQ
{
    public abstract class AbstractStoryQFixture
    {
        public abstract Feature InitStory();

        public void Scenario(Func<Scenario, Outcome> scenarioFunc)
        {
            Scenario scenario = InitStory().WithScenario(StoryQHelper.ConvertTestMethodAsScenario());
            scenarioFunc(scenario).Execute();
        }

        public void Scenario(string scenarioName, Func<Scenario, Outcome> scenarioFunc)
        {
            Scenario scenario = InitStory().WithScenario(scenarioName);
            scenarioFunc(scenario).Execute();
        }
    }
}