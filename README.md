# Guest Management API

## Description

This project is a simple ASP.NET Core Web API designed to manage guest information. It implements a CQRS (Command Query Responsibility Segregation) pattern for handling commands and queries. The API allows users to add guests, add phone numbers to existing guests, and retrieve guest information using in-memory storage for simplicity.

---

## How to Run the Project

### Steps to Run Locally

1. Clone this repository.

2. Open the project folder in Visual Studio or Visual Studio Code.

3. Restore the required packages:
   ```
   dotnet restore
   ```

4. Build the project:
   ```
   dotnet build
   ```

5. Run the project:
   ```
   dotnet run
   ```

6. The API will be hosted locally on `http://localhost:5013` or `https://localhost:7215` by default.

---

## Description of Endpoints
Please refer the GuestManagementApi.Http file to see the endpoint usage. Add Rest Client extension to the VS Code to run and see the response for individual endpoint in the GuestManagementApi.Http file.

### **1. `POST /api/Guests/AddGuest`**
Adds a new guest to the system.

---

### **2. `POST /api/Guests/AddPhone`**
Adds a phone number to an existing guest.

---

### **3. `GET /api/Guests/GetGuestById/{id}`**
Retrieves a guest by their ID.

---

### **4. `GET /api/Guests/GetAllGuests`**
Retrieves a list of all guests in the system.

---

## Future Improvements

### **1. Real Database Integration**
Currently, the API uses an in-memory database for development purposes. In a production environment, the API should use a real database such as SQL Server or PostgreSQL for persistent storage. This would require:
- Setting up a real database connection string.
- Using Entity Framework Core migrations to handle schema changes.

### **2. Advanced Validation**
Basic validation is implemented, however, additional validation could be added to:
- Ensure the email is in a valid format.
- Validate phone number format and uniqueness.
- Ensuring a guest's birthdate is a valid age.

---
