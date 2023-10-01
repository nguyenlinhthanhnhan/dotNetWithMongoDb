# MongoWithDotnet

## Project Architecture

This project is based on the N-Layer Architecture, which is a software architecture pattern that is used to separate the
business logic and the presentation logic from each other. It is also known as the **Onion Architecture**.

## Project Structure

```
.
├── docs
│   ├── Readme.md
├── src
│   ├── MongoWithDotnet.Application
│   ├── MongoWithDotnet.Core
│   ├── MongoWithDotnet.DataAccess
│   ├── MongoWithDotnet.Shared
│   ├── MongoWithDotnet.View.CRM
|   test
|   ├── MongoWithDotnet.Application.UnitTests

```

## Structure Description

- **MongoWithDotnet.Application**: Application layer, contains the application logic and the application services.
- **MongoWithDotnet.Core**: Core layer, contains the domain entities and the domain services.
- **MongoWithDotnet.DataAccess**: Data access layer, contains the repositories and the database context.
- **MongoWithDotnet.Shared**: Shared layer, contains the shared resources.
- **MongoWithDotnet.View.CRM**: Presentation layer, contains the user interface.

## Technologies

- **.NET 7.0**
- **MongoDB**

## Notes

- The project is still under development.
- Currently Authentication + Authorization is only demo, must refactor and improve.

# Checklists

[x] Create the project structure.

[x] Implement MongoDb Data Context

[x] Implement MongoDb Repositories

[x] Implement Authentication

[x] Implement Permission Based Authorization