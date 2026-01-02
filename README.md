# Employee Management Portal API

## 📋 Project Overview
A RESTful API built with ASP.NET Core 8.0 for managing employee records in an organization. This API provides full CRUD operations for employee data with proper validation, error handling, and Swagger documentation.

## 🚀 Features
- **Full CRUD Operations**: Create, Read, Update, and Delete employee records
- **Data Validation**: Built-in validation for email, phone numbers, and salary
- **Swagger Documentation**: Interactive API documentation with XML comments
- **Entity Framework Core**: Database operations using EF Core with SQL Server
- **Proper REST Conventions**: Standard HTTP methods and status codes
- **CORS Support**: Cross-origin resource sharing enabled

## 🏗️ Architecture
```
EmployeeMgmtPortal/
├── Controllers/          # API Controllers
├── Data/                # Database Context and Migrations
├── DTOs/                # Data Transfer Objects
├── Models/Entities/              # Entity Models
├── Properties/          # Build properties
├── appsettings.json     # Configuration
├── Program.cs           # Application startup
└── EmployeeMgmtPortal.csproj
```

## 📦 Prerequisites

### Required Software
1. **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download)
2. **SQL Server** - LocalDB, Express, or Docker container
3. **Entity Framework Core Tools**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

### Optional Tools
- **Postman** or **Insomnia** - API testing
- **SQL Server Management Studio or Azure Data Studio** - Database management

## 🛠️ Installation & Setup

### 1. Clone and Navigate
```bash
git clone https://github.com/Emmanuel-Ejeagha/EmployeeMgmtPortal
cd EmployeeMgmtPortal
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Configure Database
Update `appsettings.json` with your SQL Server connection:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=EmployeeDB;User Id=sa;Password=your_password;TrustServerCertificate=True;"
  }
}
```

### 4. Apply Database Migrations
```bash
dotnet ef database update
```

### 5. Build and Run
```bash
dotnet build
dotnet run
```

The API will be available at:
- **API**: `https://localhost:5001`
- **Swagger UI**: `https://localhost:5001/swagger/index.html`

## 🗄️ Database Schema

### Employees Table
| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | uniqueidentifier | PRIMARY KEY, NOT NULL | Unique employee identifier |
| Name | nvarchar(100) | NOT NULL | Full name of employee |
| Email | nvarchar(100) | NOT NULL, UNIQUE | Email address (unique) |
| PhoneNumber | nvarchar(20) | NULL | Contact phone number |
| Salary | decimal(18,2) | NOT NULL | Annual salary in USD |

## 📡 API Endpoints

### Base URL
```
https://localhost:5001/api
```

### Endpoints Summary

| Method | Endpoint | Description | Status Codes |
|--------|----------|-------------|--------------|
| **GET** | `/api/Employees` | Get all employees | 200, 500 |
| **GET** | `/api/Employees/{id}` | Get employee by ID | 200, 404, 400 |
| **POST** | `/api/Employees` | Create new employee | 201, 400, 409 |
| **PUT** | `/api/Employees/{id}` | Update employee | 200, 400, 404, 409 |
| **DELETE** | `/api/Employees/{id}` | Delete employee | 204, 404 |

### 🔍 Detailed Endpoint Documentation

#### 1. **GET /api/Employees**
Retrieves all employees in the system.

**Response Example (200 OK):**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "John Doe",
    "email": "john.doe@company.com",
    "phoneNumber": "123-456-7890",
    "salary": 55000.00
  }
]
```

#### 2. **GET /api/Employees/{id}**
Retrieves a specific employee by GUID.

**Path Parameters:**
- `id`: GUID of the employee

**Response Example (200 OK):**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "John Doe",
  "email": "john.doe@company.com",
  "phoneNumber": "123-456-7890",
  "salary": 55000.00
}
```

#### 3. **POST /api/Employees**
Creates a new employee record.

**Request Body:**
```json
{
  "name": "Jane Smith",
  "email": "jane.smith@company.com",
  "phoneNumber": "987-654-3210",
  "salary": 65000.00
}
```

**Validation Rules:**
- `name`: Required, 2-100 characters
- `email`: Required, valid email format, unique
- `phoneNumber`: Optional, valid phone format
- `salary`: Required, positive number, max 1,000,000

**Response Example (201 Created):**
```json
{
  "id": "4fa85f64-5717-4562-b3fc-2c963f66afa7",
  "name": "Jane Smith",
  "email": "jane.smith@company.com",
  "phoneNumber": "987-654-3210",
  "salary": 65000.00
}
```

#### 4. **PUT /api/Employees/{id}**
Updates an existing employee.

**Path Parameters:**
- `id`: GUID of the employee to update

**Request Body:** Same as POST

**Response Example (200 OK):** Updated employee object

#### 5. **DELETE /api/Employees/{id}**
Deletes an employee record.

**Path Parameters:**
- `id`: GUID of the employee to delete

**Response:** 204 No Content (successful deletion)

## 🧪 Testing the API

### Using Swagger UI
1. Navigate to `https://localhost:5001/swagger`
2. Click on any endpoint
3. Click "Try it out"
4. Enter required parameters
5. Click "Execute"

### Using cURL Examples

**Get all employees:**
```bash
curl -X GET "https://localhost:5001/api/Employees" \
  -H "accept: application/json"
```

**Create new employee:**
```bash
curl -X POST "https://localhost:5001/api/Employees" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Alice Johnson",
    "email": "alice.johnson@company.com",
    "phoneNumber": "555-123-4567",
    "salary": 72000.00
  }'
```

**Update employee:**
```bash
curl -X PUT "https://localhost:5001/api/Employees/3fa85f64-5717-4562-b3fc-2c963f66afa6" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Updated",
    "email": "john.updated@company.com",
    "phoneNumber": "999-888-7777",
    "salary": 60000.00
  }'
```

**Delete employee:**
```bash
curl -X DELETE "https://localhost:5001/api/Employees/3fa85f64-5717-4562-b3fc-2c963f66afa6"
```


## 🔧 Configuration

### Environment Variables
| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Runtime environment | `Production` |
| `ConnectionStrings__DefaultConnection` | Database connection string | Required |

### AppSettings Structure
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=EmployeeDB;User Id=sa;Password=your_password;TrustServerCertificate=True;"
  },
  "AllowedHosts": "*"
}
```

## 📚 Code Structure

### Key Components

#### 1. **Employee Model** (`Models/Entities/Employee.cs`)
```csharp
public class Employee
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal Salary { get; set; }
}
```

#### 2. **Data Transfer Objects** (`DTOs/`)
- `AddEmployeeDto`: For creating employees
- `UpdateEmployeeDto`: For updating employees

#### 3. **DbContext** (`Data/AppDbContext.cs`)
```csharp
public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasPrecision(18, 2);
            
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}
```

#### 4. **Controller** (`Controllers/EmployeesController.cs`)
Implements REST endpoints with proper HTTP methods and response types.

## ⚠️ Common Issues & Troubleshooting

### 1. Database Connection Failed
**Error:** `A network-related or instance-specific error occurred`

**Solutions:**
- Verify SQL Server is running
- Check connection string format (use comma for port: `localhost,1433`)
- Ensure SQL Server Authentication is enabled
- Verify username/password

### 2. XML Documentation File Missing
**Error:** `Could not find file '...xml'`

**Solutions:**
```bash
# Add to .csproj:
<GenerateDocumentationFile>true</GenerateDocumentationFile>
<NoWarn>$(NoWarn);1591</NoWarn>

# Then rebuild:
dotnet clean && dotnet build
```

### 3. Migration Issues
**Error:** `There is already an object named 'Employees' in the database`

**Solutions:**
```bash
# Drop and recreate database (development only):
dotnet ef database drop --force
dotnet ef database update

# Or create idempotent migration:
dotnet ef migrations script --idempotent --output migration.sql
```

### 4. CORS Errors
**Error:** `Failed to fetch` or `CORS policy blocked`

**Solutions:**
- Ensure CORS is configured in `Program.cs`
- Check frontend origin matches CORS policy
- Disable CORS in development if needed (use with caution)

## 🧪 Testing Strategy

### Unit Tests
```csharp
[Fact]
public void GetEmployeeById_ReturnsEmployee_WhenExists()
{
    // Arrange
    var employee = new Employee { Id = Guid.NewGuid(), Name = "Test" };
    
    // Act
    var result = controller.GetEmployeeById(employee.Id);
    
    // Assert
    Assert.IsType<OkObjectResult>(result);
}
```

### Integration Tests
Test API endpoints with in-memory database:
```csharp
public class EmployeesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    [Fact]
    public async Task GetAllEmployees_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/api/employees");
        response.EnsureSuccessStatusCode();
    }
}
```

## 🔒 Security Considerations

### Current Security Features
1. **Input Validation**: All inputs validated with Data Annotations
2. **SQL Injection Prevention**: Entity Framework parameterized queries
3. **HTTPS Redirection**: Enabled in production

### Recommended Improvements
1. **Authentication/Authorization**: Add JWT tokens
2. **Rate Limiting**: Prevent abuse
3. **Input Sanitization**: Additional XSS protection
4. **Audit Logging**: Track all changes

## 📈 Performance Considerations

### Optimizations Implemented
1. **Eager Loading**: Only load necessary data
2. **Pagination**: For large datasets (to be implemented)
3. **Caching**: Response caching (to be implemented)

### Future Optimizations
1. **Database Indexing**: On frequently queried columns
2. **Compressed Responses**: Gzip compression
3. **Connection Pooling**: Optimize database connections

```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For support, please:
1. Check the [Troubleshooting](#-common-issues--troubleshooting) section
2. Create an issue in the GitHub repository
3. Contact: support@example.com

## 🎯 Future Enhancements

Planned features:
- [ ] Authentication & Authorization
- [ ] Email notifications
- [ ] Report generation
- [ ] Bulk operations
- [ ] Search and filtering
- [ ] Export to CSV/Excel
- [ ] Dashboard with statistics

---

**Happy Coding!** 👨‍💻👩‍💻