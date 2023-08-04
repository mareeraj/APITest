
using EpamTestApi.Config;
using TechTalk.SpecFlow;

namespace EpamTestApi.Hooks
{
    [Binding]
    public sealed class Hooks
    {    

        [BeforeScenario]
        public void BeforeScenario()
        {
            Configuration.BuildAppConfiguration();
        }

 
        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}