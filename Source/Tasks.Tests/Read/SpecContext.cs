﻿using StoryQ;

namespace Tasks.Tests.Read
{
    public abstract class SpecContext : AbstractStoryQFixture
    {
        public override Feature InitStory()
        {
            return new Story("Test")
                .InOrderTo("x")
                .AsA("x")
                .IWant("x");
        }

       

    }
}