Programming Test
========

This is a dummy application to be used as part of a software development interview.

Instructions
--------

* Treat this code as if you owned this application, do whatever you feel is necessary to make this your own.
* There are several deliberate design, code quality and test issues that should be identified and resolved.
* Below is a list of the current features supported by the application; as well as some additional features that have been requested by the business owner.
* In order to work on this take a fork into your own GitHub area; make whatever changes you feel are necessary and when you are satisfied submit back via a pull request. See details on GitHub's [Fork & Pull](https://help.github.com/articles/using-pull-requests) model
* Refactor and add features (from the below list) as you see fit; there is no need to add all the features in order to "complete" the exercise. Keep in mind that code quality is the critical measure and there should be an obvious focus on testing.
* You'll notice there is no database or UI; these are not needed - the exercise deliberately avoids these requirements.
* REMEMBER: this is YOUR code, made any changes you feel are necessary.
* You're welcome to spend as much time as you like; however it's anticipated that this should take about 2 hours.

Prerequisites
--------
* This project was set up using Visual Studio 2013 Express Edition but feel free to use a different (even older) version.  The source should easily transfer across versions.
* The project uses nuget to automatically resolve dependencies however if you want to avoid nuget the only external binary that's required is NUnit 2.6.3.
* NUnit GUI runner can always be used to run the tests if Express edition of Visual Studio is being used.

abc-bank
--------

A dummy application for a bank; should provide various functions of a retail bank.

### Current Features

* A customer can open an account
* A customer can deposit / withdraw funds from an account
* A customer can request a statement that shows transactions and totals for each of their accounts
* Different accounts have interest calculated in different ways
  * **Checking accounts** have a flat rate of 0.1%
  * **Savings accounts** have a rate of 0.1% for the first $1,000 then 0.2%
  * **Maxi-Savings accounts** have a rate of 2% for the first $1,000 then 5% for the next $1,000 then 10%
* A bank manager can get a report showing the list of customers and how many accounts they have
* A bank manager can get a report showing the total interest paid by the bank on all accounts

### Additional Features

* A customer can transfer between their accounts
* Change **Maxi-Savings accounts** to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%
* Interest rates should accrue daily (incl. weekends), rates above are per-annum


### Changes Made

* Code cleaned and refactored.
* Bugs fixed.
* Existing unit test cases cleaned and new ones added to cover the targeted code.
* Featuares available only to managers separated into Manager.cs file.
* Interest rates calculations changed to accrue daily, including weekends, per-annum.
* Maxi-Savings accrual changed to an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%.
* Added a feature allowing a customer to transfer between his accounts.
