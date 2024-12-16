# Employment System API


## Overview

This project is an Employment System that allows employers to create vacancies and job seekers (applicants) to search and apply for opened positions. The system also includes vacancy management and applicant limitations.

---

## Features

### User Types

The system includes two types of users:
- Employer
- Applicant

## Functions:
- Self-Registration: Both employers and applicants can register.
- Login: Both users can log in to the system.

## Employer Features:
- CRUD Operations for Vacancies: Employers can create, read, update, and delete job vacancies.
- Vacancy Constraints: Set the maximum number of allowed applications per vacancy.
- Add an expiry date for each vacancy.
- Manage Vacancies: Post or deactivate a vacancy.
- View a list of applicants for a specific vacancy.

## Applicant Features:
- Search for Vacancies: Applicants can search for available job positions.
- Apply for Vacancies: Cannot exceed the maximum number of applications allowed for a vacancy.
- Applicants are limited to one application per day (24 hours).

---

## Tech Stack

- Backend Framework:	ASP.NET Core (.NET Core)
- Database:	Microsoft SQL Server
- API Development: RESTful APIs
- Architecture:	CQRS, Onion Architecture
- System Design:	Clean Code Principles
  
---

## Requirements
- Microsoft SQL Server installed and running.
- .NET Core SDK (.NET 8).
- Visual Studio or any compatible IDE.

## Installation Steps
Clone the Repository:

```bash
git clone https://github.com/AdhamMo1/EmploymentSystemApi.git
cd employment-system
```
### Database Setup:

- Use Microsoft SQL Server and configure the connection string in appsettings.json.
### Run the Application:

- Open the project in Visual Studio.
- Restore dependencies:
```bash
dotnet restore
```
### Run the project:
```bash
dotnet run
```

---
## Contact
- For any questions or suggestions, feel free to reach out:

- Email: adhmmohamed066@gmail.com
- LinkedIn: https://www.linkedin.com/in/adham-mohamed1

