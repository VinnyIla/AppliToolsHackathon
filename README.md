# AppliToolsHackathon

To execute the Traditional tests
1. Open ApplitoolsHackathon.sln in Visual Studio 2019
2. Click Extensions > Manage Extensions, and ensure these are enabled
	SpecFlow for Visual Studio 2019 and
	NUnit 3 Test Adapter
3. Build the Project
4. In Text Explorer window, there should be 5 tests under Traditional Tests
5. For V1, change the url in App.config  and run the Traditional Tests.
6. All tests should pass, except Canvas Chart, where I cannot automate
7. For V2, change the url in App.config and run the Traditional Tests.
8. All tests should fail with a reason



To execute Visual Tests using Applitools,
1. Follow Steps 1-3 above
2. In Text Explorer window, there should be 1 tests under VisualUITests
3. For V1, change the url in App.config  and run the Visual Tests.
4. All tests should pass in Applitools Test Results
5. For V2, change the url in App.config and run the Traditional Tests.
6. All tests should fail highlighting the changes