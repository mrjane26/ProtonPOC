# ProtonPOC
Proof of concept for testing ProtonMail

This is a proof of concept automation framework built using .NETCore 3.1 in combination with Selenium and NUnit.

The tests are aimed at the Folder and Labels functionality for ProtonMail, currently there are tests for:

1.Logging in and confirming success
2.Open the Folders and Labels page and confirm it's open
3.Create a Folder and confirm it's creatin (cleanup of test data included)
4.Create a Label and confirm it's creation (cleanup of test data included)

The framework is using a Page Object Model architecture.
The naming convention for the tests is - When_StateUnderTest_Expect_ExpectedBehavior so it's clear from the test name what is it actually doing.
The steps are written using method chaining so when looking at the test it's also clear what each step is meant to be doing.

A workflow is created for the project to be built and the tests to be executed using a headless Chrome driver.
