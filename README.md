# NTierManagement ASP.NET Core Web API
- This project is a company management system that performs basic CRUD operations through an ASP.NET Core Web API.
 The project is built using the NTier architecture, which provides strong separation between different layers of the application.

- Every company has a CEO and departments. Each department has a leader and employees. The person defined in each person type can serve as both CEO, Leader and employee.
## Architecture
The project is structured into the following layers:
- NTierManagement.BLL (Business Logic Layer): This layer contains the business logic of the application. Business rules are defined here, and it interacts with the DAL (Data Access Layer).
- NTierManagement.DAL (Data Access Layer): This layer handles direct interaction with the database. All database operations are performed here.
- NTierManagement.Entity: This layer defines the core data models of the application.
- NTierManagement.UI: This is the presentation layer of the API, where API requests are processed and responses are returned.
## Features
- Modular structure using NTier architecture.
- CRUD operations (Create, Read, Update, Delete).
- RESTful API adhering to industry standards.
- Integration with an MsSQL database using Entity Framework.
## Installation
### To clone and set up the project locally, use the following commands:
```
git clone https://github.com/tanercamm/NTierManagement.git
cd NTierManagement
```
### To install the required dependencies:
```
dotnet restore
```
### To run the project:
```
dotnet run
```
## Usage
- After running the project, you can access and test the API endpoints using Postman or a similar tool.
## API Endpoints
### Company
- GET /api/Company/Details => Retrieves the list of all companies with their details.
- GET /api/Company => Retrieves a list of all companies.
- GET /api/Company/{id} => Retrieves a specific company by ID.
- POST /api/Company => Adds a new company.
- PUT /api/Company/{id} => Updates an existing company by ID.
- DELETE /api/Company/{id} => Deletes a specific company by ID.
### Department
- GET /api/Department/Details => Retrieves the list of all departments with their details.
- GET /api/Department => Retrieves a list of all departments.
- GET /api/Department/{id} => Retrieves a specific department by ID.
- POST /api/Department => Adds a new department.
- PUT /api/Department/{id} => Updates an existing department by ID.
- DELETE /api/Department/{id} => Deletes a specific department by ID.
### Person
- GET /api/Person/Details => Retrieves the list of all people with their details.
- GET /api/Person => Retrieves a list of all people.
- GET /api/Person/{id} => Retrieves a specific person by ID.
- POST /api/Person => Adds a new person.
- PUT /api/Person/{id} => Updates an existing person by ID.
- DELETE /api/Person/{id} => Deletes a specific person by ID.
##
