# SauceDemo Automation Testing Suite

## Project Overview
This repository contains a robust automated testing suite for the [SauceDemo](https://www.saucedemo.com/) e-commerce platform. The project is built using **C#** and **.NET**, implementing professional testing standards to ensure high-quality software delivery.

## Tech Stack
* **Language:** C#
* **Framework:** .NET 8.0
* **Test Runner:** xUnit
* **Automation Tool:** Selenium WebDriver
* **Assertions:** Fluent Assertions
* **Design Pattern:** Page Object Model (POM)

## Key Features & Testing Coverage
The suite covers critical user journeys, ensuring business logic consistency:

* **Authentication Workflows:** Validation of login processes with different user roles.
* **Product Management:** Verifying sorting functionality (A-Z, Z-A, Price Low-High) and inventory accuracy.
* **Checkout Pipeline:** Full end-to-end testing of the shopping cart, including information entry and order completion.

## Project Structure
The project follows a modular structure to ensure maintainability:
* **Pages:** Contains the Page Object classes with encapsulated WebElements and actions.
* **Tests:** Contains the test classes with xUnit facts and theories.
* **Data:** Managed test data for different scenarios.

## Best Practices Implemented
* **Fluent Assertions:** For readable and expressive test validations.
* **Explicit Waits:** Used to handle asynchronous elements and prevent flakiness.
* **Clean Code:** Adhering to DRY (Don't Repeat Yourself) and SOLID principles.

---
*Developed as part of a technical specialization in Software Test Automation.*
