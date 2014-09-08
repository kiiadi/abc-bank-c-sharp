abc-bank-c-sharp Programming Test
Alexey Zengin 20140908
========

Changes:
--------
* Code cleaned and refactored.
* Bugs fixed.
* Existing unit test cases cleaned and new ones added to cover the targeted code.
* Featuares available only to managers separated into Manager.cs file.
* Interest rates calculations changed to accrue daily, including weekends, per-annum.
* Maxi-Savings accrual changed to an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%.
* Added a feature allowing a customer to transfer between his accounts.
---------