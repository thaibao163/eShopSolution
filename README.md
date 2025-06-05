# eShopSolution – E-Commerce API with Clean Architecture

This is a backend web application for an e-commerce system, built using ASP.NET Core and applying Clean Architecture principles. The solution is designed to be scalable, maintainable, and testable.

## 🔧 Technologies

- ASP.NET Core 6
- Entity Framework Core
- SQL Server
- AutoMapper
- MediatR (CQRS Pattern)
- JWT Authentication
- Swagger (OpenAPI)
- Clean Architecture: Domain, Application, Infrastructure, WebAPI

## 📂 Project Structure

- `Domain`: Contains domain entities and interfaces.
- `Application`: Includes DTOs, business logic, commands, queries, and mapping profiles.
- `Infrastructure`: Implements data access and services.
- `WebApi`: Exposes API endpoints and handles HTTP requests.

## 🚀 Features

- 🛒 Product listing, filtering, and detail retrieval
- 👤 User authentication using JWT
- 🛠 Admin: manage products, categories, orders, and users
- 🔄 CQRS pattern with MediatR
- 🧭 Swagger UI for API testing

## ▶️ Getting Started

### Prerequisites

- Visual Studio 2022+
- .NET 6 SDK
- SQL Server

### Run the app locally

1. Clone this repository:
   ```bash
   git clone https://github.com/thaibao163/eShopSolution.git

2. Open the solution eShopSolution.sln in Visual Studio.

3. Update the connection string in appsettings.json:
    "ConnectionStrings": {
      "DefaultConnection": "Server=.;Database=eShopDB;Trusted_Connection=True;"
    }

4. Open Package Manager Console and run:
    Update-Database

5. Press F5 to run the Web API.

📌 Future Improvements
Add unit and integration tests

Add email/payment service


# Website quản lí Thương mại điện tử
***
- Cài đặt SQLServer
- Cài đặt Microsoft Visual Studio
  
## Hướng dẫn chạy code

### B1: Vào Presentantion --> WebApi --> appsettings.json
Tại dòng số 10 thay đổi Server="..." thành Server="Server name trên máy chủ của bạn"
  ![image](https://github.com/thaibao163/eShopSolution/assets/79973618/4816d3f7-ebf9-4d93-bbc1-aa18d34397e0)
  
### B2: Chọn Tools --> Nuget Package Manager --> Package Manager Console
![image](https://github.com/thaibao163/eShopSolution/assets/79973618/1154011d-1cde-45da-8cdb-f2bf7c9749a7)

### B3: Chạy lần lượt 2 dòng lệnh
    add-migration Tên_File_Bạn_Muốn_Tạo
    
    update-database
![image](https://github.com/thaibao163/eShopSolution/assets/79973618/e90cc5a9-f322-4459-ad3c-4ccc837cb310)

### B4: Click chuột phải vào WebApi chọn Set as Startup Project
![image](https://github.com/thaibao163/eShopSolution/assets/79973618/dc848d20-30e8-44d3-8b63-c662d5122dee)

### B5: Run Code
![image](https://github.com/thaibao163/eShopSolution/assets/79973618/5b17a259-bc90-402b-a9eb-ec30a8049191)

### B6: Sau khi chạy bạn vào SQLServer chạy dòng lệnh:
    TRUNCATE TABLE Permissons

### B7: Vào Infrastructure --> Persistence --> DatabaseSeeder --> DatabaseSeeder.cs
Mở SQLServer Select bảng Identity.Roles
    
### B8: Thay đổi RoleId thành Id tương ứng với bảng Identity.Roles
![image](https://github.com/thaibao163/eShopSolution/assets/79973618/dce46e24-db3f-4ae2-be78-eb0c621bac00)
![image](https://github.com/thaibao163/eShopSolution/assets/79973618/3082dca8-bf7f-45e5-8c76-2a3dcaedfd58)

### B9: Chạy lại code rồi kiểm tra lại bảng Permissons

- Tài khoản: superadmin
- Mật khẩu: admin






   
