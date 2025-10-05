# Alza Test Project

This project provides a simple Web API for managing products in an online store.

## Summary

The Alza Test Project provides two API versions for managing products, supports SQLite for data storage, and includes optional Kafka-based asynchronous processing for updating stock information.
It also features Swagger documentation and xUnit tests for reliability and easy maintainability.

---

## Features

### API Versions

There are two versions of the API available:

#### 1. Basic Version

- Get all products
- Get a product by ID
- Create a product (requires name and image URL)
- Update product stock (directly)

#### 2. Advanced Version

- Get all products with pagination
- Get a product by ID
- Create a product (requires name and image URL)
- Update product stock via **asynchronous queue** (request is sent to a queue and processed later by a worker)

---

### Database

The project uses **SQLite** as its database.  
The connection is configured in the `appsettings.json` file:

```json
"ConnectionStrings": {
  "DefaultConnection": ""
}
```

It is recommended to specify a database file path located outside the project directory to prevent accidental deletion or overwriting during development.

---

### Asynchronous queue

The project supports two implementations of the asynchronous queue:

- **InMemory queue** (used by default)
- **Kafka queue** (enabled with the `--use-kafka` argument)

To enable Kafka-based queue processing, start the application with the `--use-kafka` flag.  
Make sure your Kafka server is up and running before doing so, and configure the necessary settings in `appsettings.json`.

Kafka configuration example:

```json
"Kafka": {
  "General": {
    "BootstrapServers": "", 
    "Username": "", 
    "Password": "", 
    "SaslMechanism": "", 
    "SecurityProtocol": ""
  },
  "Queues": {
    "UpdateStock": {
      "Topic": "", 
      "GroupId": ""
    }
  }
}
```
- The `BootstrapServers` and `UpdateStock` configuration fields are **required**.  
- Leave authentication fields empty (`Username`, `Password`, etc.) if your Kafka setup does not require them.  
- Supported values for `SaslMechanism`: `Gssapi`, `Plain`, `ScramSha256`, `ScramSha512`, `OAuthBearer`.  
- Supported values for `SecurityProtocol`: `Plaintext`, `Ssl`, `SaslPlaintext`, `SaslSsl`.

---

## Swagger Documentation

Both API versions include automatically generated **Swagger documentation**.  
Once the application is running, you can access it at: ``` https://localhost:44334/swagger/ ```

---

## Running the Application

You can run the project either from **Visual Studio** or **via the command line**.

### 🔹 From Visual Studio

1. Open the solution in **Visual Studio**.  
2. Make sure you have configured your `appsettings.json`:
   - Set the correct SQLite database path in `"DefaultConnection"`.
   - (Optional) Configure Kafka settings if using Kafka.
3. Select the AlzaTestProject as startup project and click **Run** (▶).

If you wish to use Kafka:

- Ensure that your **Kafka server** is running.
- Start the app with the following argument: ``` --use-kafka ```

### 🔹 From the Command Line

1. Navigate to the **AlzaTestPoject** directory.  
2. Ensure that your `appsettings.json` is configured properly.  
3. Run the application using:

 ```bash
 dotnet run
 ```
 or, to enable Kafka:
 ```bash
 dotnet run --use-kafka
 ```

---

## Running Unit Tests

Unit tests are implemented using xUnit.

### 🔹 From Visual Studio

1. Open **Test Explorer** (Test > Test Explorer).
2. Click **Run All Tests** or select individual tests to run.

### 🔹 From the Command Line

1. Navigate to the **AlzaTestPoject.Tests** directory.
2. Use the built-in .NET CLI test runner:
```bash
dotnet test
```
This will automatically build the solution and execute all tests.