﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.2.0.0
//      SpecFlow Generator Version:2.2.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace DotNetRetry.Acceptance.Tests.Scenarios.Exponential.ForeverTests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "Forever")]
    public partial class ForeverForActionRetriesFeature : Xunit.IClassFixture<ForeverForActionRetriesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Forever.feature"
#line hidden
        
        public ForeverForActionRetriesFeature(ForeverForActionRetriesFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Forever for action retries", "\tIn order to test exponential retry policy\r\n\tAs using a forever-style algorithm\r\n" +
                    "\tI want to verify the number of attempts and time took to retry an operation", ProgrammingLanguage.CSharp, new string[] {
                        "Forever"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute exponential operation errorless without retrying")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute exponential operation errorless without retrying")]
        public virtual void ExecuteExponentialOperationErrorlessWithoutRetrying()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute exponential operation errorless without retrying", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have entered a successfull non-returnable operation", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("no retry attempts should be made", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Zero time throws exception for exponential")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Zero time throws exception for exponential")]
        public virtual void ZeroTimeThrowsExceptionForExponential()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Zero time throws exception for exponential", ((string[])(null)));
#line 13
this.ScenarioSetup(scenarioInfo);
#line 14
 testRunner.Given("I have entered a successfull non-returnable operation", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
 testRunner.And("I have entered 0 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.When("I setup exponential configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
 testRunner.Then("ArgumentOutOfRangeException is thrown when time is less than 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 1 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 1 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt1Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 1 try", ((string[])(null)));
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 1 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
 testRunner.Then("no retry attempts should be made", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 2 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 2 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt2Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 2 try", ((string[])(null)));
#line 25
this.ScenarioSetup(scenarioInfo);
#line 26
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 2 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
 testRunner.Then("exactly 1 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 30
 testRunner.And("OnFailure will be dispatched exactly 1 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And("OnBeforeRetry will be dispatched exactly 1 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("OnAfterRetry will be dispatched exactly 1 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 3 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 3 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt3Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 3 try", ((string[])(null)));
#line 35
this.ScenarioSetup(scenarioInfo);
#line 36
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 3 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 37
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("exactly 2 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 40
 testRunner.And("OnFailure will be dispatched exactly 2 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("OnBeforeRetry will be dispatched exactly 2 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("OnAfterRetry will be dispatched exactly 2 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 4 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 4 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt4Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 4 try", ((string[])(null)));
#line 45
this.ScenarioSetup(scenarioInfo);
#line 46
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 4 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 47
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.Then("exactly 3 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 50
 testRunner.And("OnFailure will be dispatched exactly 3 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("OnBeforeRetry will be dispatched exactly 3 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("OnAfterRetry will be dispatched exactly 3 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 5 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 5 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt5Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 5 try", ((string[])(null)));
#line 55
this.ScenarioSetup(scenarioInfo);
#line 56
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 5 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 57
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 59
 testRunner.Then("exactly 4 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 60
 testRunner.And("OnFailure will be dispatched exactly 4 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.And("OnBeforeRetry will be dispatched exactly 4 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
 testRunner.And("OnAfterRetry will be dispatched exactly 4 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 6 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 6 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt6Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 6 try", ((string[])(null)));
#line 65
this.ScenarioSetup(scenarioInfo);
#line 66
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 6 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 67
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 69
 testRunner.Then("exactly 5 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 70
 testRunner.And("OnFailure will be dispatched exactly 5 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And("OnBeforeRetry will be dispatched exactly 5 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("OnAfterRetry will be dispatched exactly 5 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 7 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 7 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt7Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 7 try", ((string[])(null)));
#line 75
this.ScenarioSetup(scenarioInfo);
#line 76
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 7 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 77
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 79
 testRunner.Then("exactly 6 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 80
 testRunner.And("OnFailure will be dispatched exactly 6 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.And("OnBeforeRetry will be dispatched exactly 6 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And("OnAfterRetry will be dispatched exactly 6 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 8 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 8 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt8Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 8 try", ((string[])(null)));
#line 85
this.ScenarioSetup(scenarioInfo);
#line 86
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 8 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 87
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 89
 testRunner.Then("exactly 7 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 90
 testRunner.And("OnFailure will be dispatched exactly 7 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
 testRunner.And("OnBeforeRetry will be dispatched exactly 7 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
 testRunner.And("OnAfterRetry will be dispatched exactly 7 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 9 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 9 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt9Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 9 try", ((string[])(null)));
#line 95
this.ScenarioSetup(scenarioInfo);
#line 96
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 9 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 97
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 99
 testRunner.Then("exactly 8 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 100
 testRunner.And("OnFailure will be dispatched exactly 8 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
 testRunner.And("OnBeforeRetry will be dispatched exactly 8 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
 testRunner.And("OnAfterRetry will be dispatched exactly 8 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing exponential operation to succeed at 10 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for action retries")]
        [Xunit.TraitAttribute("Description", "Execute failing exponential operation to succeed at 10 try")]
        public virtual void ExecuteFailingExponentialOperationToSucceedAt10Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing exponential operation to succeed at 10 try", ((string[])(null)));
#line 105
this.ScenarioSetup(scenarioInfo);
#line 106
 testRunner.Given("I have entered a failing non-returnable operation which succeeds at 10 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 107
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 108
 testRunner.When("I attempt to run exponential", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 109
 testRunner.Then("exactly 9 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 110
 testRunner.And("OnFailure will be dispatched exactly 9 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
 testRunner.And("OnBeforeRetry will be dispatched exactly 9 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.And("OnAfterRetry will be dispatched exactly 9 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 113
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ForeverForActionRetriesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ForeverForActionRetriesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion