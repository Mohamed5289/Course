# Course Content API

## Overview
This project is a **Course Management System** that provides **22 API endpoints** for managing courses, users, instructors, authentication, and messaging. It follows the **CQRS (Command Query Responsibility Segregation)** pattern, utilizes **SignalR** for real-time communication, and implements various handlers for business logic.

---

## Key Features

### ✅ Authentication & Authorization:
- Uses **JWT tokens** with **Refresh & Revoke mechanisms**
- Implements secure **User Verification & Password Management**

### ✅ CQRS Pattern Implementation:
- Separates **Commands (write operations)** and **Queries (read operations)**
- Enhances **scalability** and **maintainability**

### ✅ Real-time Messaging with SignalR:
- Supports **real-time message updates** for users

### ✅ User & Course Management:
- Allows **user registration, enrollment, and instructor management**
- Supports **course creation, retrieval, and deletion**

### ✅ Security & Verification Handlers:
- Custom handlers for **token validation, password reset, and verification**

---

## Endpoints & Handlers

### **Authentication & Token Management**
- `RegisterHandler` → **User registration**
- `LoginHandler` → **User login**
- `RefreshTokenHandler` → **Refresh JWT token**
- `RevokeTokenHandler` → **Revoke user session**
- `ForgetPasswordHandler` → **Handle forgotten passwords**
- `RestPasswordHandler` → **Reset user password**

### **User Management**
- `GetUserHandler` → **Retrieve user details**
- `GetUserByUserNameHandler` → **Retrieve user by username**
- `GetUsersCourseHandler` → **Fetch all courses for a user**
- `GetUserEnrollHandler` → **Fetch enrolled courses for a user**

### **Course Management**
- `GetCourseHandler` → **Retrieve course details**
- `CreateCourseHandler` → **Create a new course**
- `EnrollHandler` → **Enroll user in a course**

### **Instructor Management**
- `CreateInstructorHandler` → **Add a new instructor**
- `DeleteInstructorHandler` → **Remove an instructor**

### **Department Management**
- `InsertDepartmentHandler` → **Add a new department**

### **Messaging & Real-time Communication**
- `GetMessagesHandler` → **Retrieve messages via SignalR**

### **Security & Verification**
- `VerificationHandler` → **Handles email/phone verification**

---

## Technologies Used
- **ASP.NET Core Web API**
- **CQRS (MediatR)**
- **SignalR (Real-time messaging)**
- **JWT Authentication & Refresh Tokens**
- **Entity Framework Core (EF Core)**
- **SQL Server (Database)**
