 
# Bookshelf API Challenge

**Deadline:** 6 days from assignment date

## Overview

This challenge is designed to test your ability to build a robust .NET Web API implementing real-world requirements. You will create a Bookshelf API that manages authors, categories, books, orders, and users (customers) using advanced architectural and security practices.

## Objective

Build a RESTful Bookshelf API using .NET with the following features:

- **Onion Architecture:** Implement a 4-layer structure consisting of Domain, Application, Infrastructure, and WebAPI layers.
- **Authentication & Security:** Secure the API using JWT tokens signed with HMACSHA and AES encryption. Provide endpoints for user registration and login.
- **Dependency Injection:** Replace the default DI container with Autofac.
- **Object Mapping:** Use AutoMapper to map between your domain models and DTOs.
- **Swagger:** Document your API using Swagger, ensuring that it supports authenticated testing.
- **Data Access:** Utilize a generic repository pattern along with an Identity DbContext for user management.

## Entities

Your API must include at least the following entities:

- **Author**
- **Category**
- **Book**
- **Ordering**
- **Users (Customer)**

### Relationships

- **Author & Book:** Many-to-Many  
- **Category & Book:** Many-to-Many  
- **Book & Ordering:** Many-to-Many  

## Technical Requirements

- **.NET Web API:** Use .NET Core/5/6 (or later) to build your API.
- **Onion Architecture:** Structure your project into the following layers:
  - **Domain:**
  - **Application:** 
  - **Infrastructure:**  
  - **WebAPI:**  
- **Identity Management:** Create an Identity DbContext for managing user authentication and roles.
- **Generic Repository:** Implement a generic repository pattern to manage CRUD operations for your entities.
- **Dependency Injection:** Use Autofac for DI configuration.
- **JWT Authentication:** Secure endpoints using JWT tokens, implementing HMACSHA for signing and AES encryption for extra security.
- **Swagger Documentation:** Configure Swagger to automatically generate API docs. Secure the Swagger UI with authentication.

## API Endpoints

The following table details the essential API endpoints that you must implement:

| HTTP Method | Endpoint                     | Description                                    | Authentication Required |
|-------------|------------------------------|------------------------------------------------|-------------------------|
| **Author Endpoints** ||||
| GET         | `/api/authors`               | Retrieve all authors                           | Yes                     |
| GET         | `/api/authors/{id}`          | Retrieve a specific author by ID               | Yes                     |
| POST        | `/api/authors`               | Create a new author                            | Yes                     |
| PUT         | `/api/authors/{id}`          | Update an existing author                      | Yes                     |
| DELETE      | `/api/authors/{id}`          | Delete an author                               | Yes                     |
| **Category Endpoints** ||||
| GET         | `/api/categories`            | Retrieve all categories                        | Yes                     |
| GET         | `/api/categories/{id}`       | Retrieve a specific category by ID             | Yes                     |
| POST        | `/api/categories`            | Create a new category                          | Yes                     |
| PUT         | `/api/categories/{id}`       | Update an existing category                    | Yes                     |
| DELETE      | `/api/categories/{id}`       | Delete a category                              | Yes                     |
| **Book Endpoints** ||||
| GET         | `/api/books`                 | Retrieve all books                             | Yes                     |
| GET         | `/api/books/{id}`            | Retrieve a specific book by ID                 | Yes                     |
| POST        | `/api/books`                 | Create a new book                              | Yes                     |
| PUT         | `/api/books/{id}`            | Update an existing book                        | Yes                     |
| DELETE      | `/api/books/{id}`            | Delete a book                                  | Yes                     |
| **Ordering Endpoints** ||||
| GET         | `/api/orders`                | Retrieve all orders                            | Yes                     |
| GET         | `/api/orders/{id}`           | Retrieve a specific order by ID                | Yes                     |
| POST        | `/api/orders`                | Create a new order                             | Yes                     |
| PUT         | `/api/orders/{id}`           | Update an existing order                       | Yes                     |
| DELETE      | `/api/orders/{id}`           | Delete an order                                | Yes                     |
| **User Endpoints** ||||
| GET         | `/api/users`                 | Retrieve all users (customers)                 | Yes                     |
| GET         | `/api/users/{id}`            | Retrieve a specific user by ID                 | Yes                     |
| POST        | `/api/users/register`        | Register a new user                            | No                      |
| POST        | `/api/users/login`           | Login and obtain JWT token                     | No                      |

## Additional Functional Requirements

- **Relationship Management:**  
  - Manage many-to-many relationships (e.g., link/disconnect authors with books, categories with books, and books with orders).
- **Validation & Error Handling:**  
  - Validate incoming request data and handle errors gracefully.

## Submission Guidelines

- Submit the repository link before the deadline.

## Evaluation Criteria

Your submission will be evaluated based on:

- **Functionality:** The API meets all the specified requirements.
- **Code Quality:** Code is clean, well-organized, and maintainable.
- **Architecture:** Correct implementation of Onion Architecture and design patterns.
- **Security:** Proper implementation of JWT authentication, HMACSHA signing, and AES encryption.
- **Testing:** Inclusion and quality of unit/integration tests.
- **Documentation:** Clarity of instructions and overall project documentation.
 
Good luck, and happy coding!
