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
namespace DotNetRetry.Acceptance.Tests.Scenarios.Sequential.ForeverTests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "Forever,")]
    [Xunit.TraitAttribute("Category", "Functions")]
    public partial class ForeverForFunctionRetriesFeature : Xunit.IClassFixture<ForeverForFunctionRetriesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ForeverForFunctions.feature"
#line hidden
        
        public ForeverForFunctionRetriesFeature(ForeverForFunctionRetriesFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Forever for function retries", "\tIn order to test sequential retry policy\r\n\tAs using a forever-style algorithm\r\n\t" +
                    "I want to verify the number of attempts and time took to retry an operation", ProgrammingLanguage.CSharp, new string[] {
                        "Forever,",
                        "Functions"});
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
        
        [Xunit.FactAttribute(DisplayName="Execute operation errorless without retrying")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute operation errorless without retrying")]
        public virtual void ExecuteOperationErrorlessWithoutRetrying()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute operation errorless without retrying", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have entered a successfull returnable operation", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("no retry attempts should be made", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 12
 testRunner.And("result is Hello world!", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Zero time throws exception")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Zero time throws exception")]
        public virtual void ZeroTimeThrowsException()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Zero time throws exception", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("I have entered a successfull returnable operation", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 16
 testRunner.And("I have entered 0 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.When("I setup rule configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
 testRunner.Then("ArgumentOutOfRangeException is thrown when time is less than 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 1 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 1 try")]
        public virtual void ExecuteFailingOperationToSucceedAt1Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 1 try", ((string[])(null)));
#line 20
this.ScenarioSetup(scenarioInfo);
#line 21
 testRunner.Given("I have entered a failing returnable operation which succeeds at 1 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 22
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 24
 testRunner.Then("no retry attempts should be made", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 2 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 2 try")]
        public virtual void ExecuteFailingOperationToSucceedAt2Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 2 try", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 27
 testRunner.Given("I have entered a failing returnable operation which succeeds at 2 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("exactly 1 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 31
 testRunner.And("took around 100 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("OnFailure will be dispatched exactly 1 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("OnBeforeRetry will be dispatched exactly 1 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("OnAfterRetry will be dispatched exactly 1 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 3 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 3 try")]
        public virtual void ExecuteFailingOperationToSucceedAt3Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 3 try", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 38
 testRunner.Given("I have entered a failing returnable operation which succeeds at 3 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 39
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 41
 testRunner.Then("exactly 2 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 42
 testRunner.And("took around 200 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("OnFailure will be dispatched exactly 2 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("OnBeforeRetry will be dispatched exactly 2 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("OnAfterRetry will be dispatched exactly 2 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 4 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 4 try")]
        public virtual void ExecuteFailingOperationToSucceedAt4Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 4 try", ((string[])(null)));
#line 48
this.ScenarioSetup(scenarioInfo);
#line 49
 testRunner.Given("I have entered a failing returnable operation which succeeds at 4 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 50
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 52
 testRunner.Then("exactly 3 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 53
 testRunner.And("took around 300 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("OnFailure will be dispatched exactly 3 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("OnBeforeRetry will be dispatched exactly 3 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("OnAfterRetry will be dispatched exactly 3 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 5 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 5 try")]
        public virtual void ExecuteFailingOperationToSucceedAt5Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 5 try", ((string[])(null)));
#line 59
this.ScenarioSetup(scenarioInfo);
#line 60
 testRunner.Given("I have entered a failing returnable operation which succeeds at 5 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 61
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 63
 testRunner.Then("exactly 4 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 64
 testRunner.And("took around 400 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And("OnFailure will be dispatched exactly 4 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("OnBeforeRetry will be dispatched exactly 4 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.And("OnAfterRetry will be dispatched exactly 4 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 6 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 6 try")]
        public virtual void ExecuteFailingOperationToSucceedAt6Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 6 try", ((string[])(null)));
#line 70
this.ScenarioSetup(scenarioInfo);
#line 71
 testRunner.Given("I have entered a failing returnable operation which succeeds at 6 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 72
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 74
 testRunner.Then("exactly 5 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 75
 testRunner.And("took around 500 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
 testRunner.And("OnFailure will be dispatched exactly 5 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.And("OnBeforeRetry will be dispatched exactly 5 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.And("OnAfterRetry will be dispatched exactly 5 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 7 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 7 try")]
        public virtual void ExecuteFailingOperationToSucceedAt7Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 7 try", ((string[])(null)));
#line 81
this.ScenarioSetup(scenarioInfo);
#line 82
 testRunner.Given("I have entered a failing returnable operation which succeeds at 7 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 83
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 85
 testRunner.Then("exactly 6 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 86
 testRunner.And("took around 600 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
 testRunner.And("OnFailure will be dispatched exactly 6 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.And("OnBeforeRetry will be dispatched exactly 6 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
 testRunner.And("OnAfterRetry will be dispatched exactly 6 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 90
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 8 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 8 try")]
        public virtual void ExecuteFailingOperationToSucceedAt8Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 8 try", ((string[])(null)));
#line 92
this.ScenarioSetup(scenarioInfo);
#line 93
 testRunner.Given("I have entered a failing returnable operation which succeeds at 8 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 94
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 96
 testRunner.Then("exactly 7 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 97
 testRunner.And("took around 700 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
 testRunner.And("OnFailure will be dispatched exactly 7 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 99
 testRunner.And("OnBeforeRetry will be dispatched exactly 7 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
 testRunner.And("OnAfterRetry will be dispatched exactly 7 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 9 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 9 try")]
        public virtual void ExecuteFailingOperationToSucceedAt9Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 9 try", ((string[])(null)));
#line 103
this.ScenarioSetup(scenarioInfo);
#line 104
 testRunner.Given("I have entered a failing returnable operation which succeeds at 9 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 105
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 107
 testRunner.Then("exactly 8 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 108
 testRunner.And("took around 800 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 109
 testRunner.And("OnFailure will be dispatched exactly 8 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
 testRunner.And("OnBeforeRetry will be dispatched exactly 8 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
 testRunner.And("OnAfterRetry will be dispatched exactly 8 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.And("no AggregateException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Execute failing operation to succeed at 10 try")]
        [Xunit.TraitAttribute("FeatureTitle", "Forever for function retries")]
        [Xunit.TraitAttribute("Description", "Execute failing operation to succeed at 10 try")]
        public virtual void ExecuteFailingOperationToSucceedAt10Try()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute failing operation to succeed at 10 try", ((string[])(null)));
#line 114
this.ScenarioSetup(scenarioInfo);
#line 115
 testRunner.Given("I have entered a failing returnable operation which succeeds at 10 try", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 116
 testRunner.And("I have entered 100 milliseconds between", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 117
 testRunner.When("I attempt to run it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 118
 testRunner.Then("exactly 9 retry should happen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 119
 testRunner.And("took around 900 milliseconds in total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 120
 testRunner.And("OnFailure will be dispatched exactly 9 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 121
 testRunner.And("OnBeforeRetry will be dispatched exactly 9 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 122
 testRunner.And("OnAfterRetry will be dispatched exactly 9 times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 123
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
                ForeverForFunctionRetriesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ForeverForFunctionRetriesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion