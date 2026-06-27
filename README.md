# 🛒 Matger - E-Commerce RESTful API

A production-style E-Commerce RESTful API built with **ASP.NET Core .NET 9** following **Onion Architecture** and modern backend development practices.

The API allows customers to browse products, search and filter items, manage shopping baskets, authenticate securely, create orders, and complete online payments using Stripe.

---

# Features

- Product Management
- Product Search
- Product Filtering
- Product Sorting
- Pagination
- Shopping Basket
- JWT Authentication
- User Registration & Login
- ASP.NET Core Identity
- Address Management
- Order Management
- Payment Integration (Stripe)
- Redis Basket Storage
- Global Exception Handling
- Data Seeding
- Swagger Documentation

---

# Technologies

- ASP.NET Core .NET 9
- C#
- SQL Server
- Entity Framework Core
- LINQ
- ASP.NET Core Identity
- JWT Authentication
- Redis
- Stripe Payment Gateway
- AutoMapper
- Swagger / OpenAPI
- Postman

---

# Design Patterns

- Onion Architecture
- Repository Pattern
- Unit of Work Pattern
- Specification Pattern
- Dependency Injection

---

# Solution Structure

<p align="center">
<img src="docs/images/Solution%20Structure/Solution%20Structure.png" width="900">
</p>

---

# Swagger Documentation

## API Overview

<p align="center">
<img src="docs/images/Swagger/swagger-home.png" width="900">
</p>

## Products

<p align="center">
<img src="docs/images/Swagger/Product.png" width="900">
</p>

## Accounts

<p align="center">
<img src="docs/images/Swagger/Accounts.png" width="900">
</p>

## Basket

<p align="center">
<img src="docs/images/Swagger/Basket.png" width="900">
</p>

## Orders

<p align="center">
<img src="docs/images/Swagger/Orders.png" width="900">
</p>

## Payments

<p align="center">
<img src="docs/images/Swagger/Payments.png" width="900">
</p>

---

# Postman Testing

## Authentication

<p align="center">
<img src="docs/images/Postman/Login.png" width="900">
</p>

## Products

<p align="center">
<img src="docs/images/Postman/Product.png" width="900">
</p>

## Product Search

<p align="center">
<img src="docs/images/Postman/ProductsSearch.png" width="900">
</p>

## Product Filtering

<p align="center">
<img src="docs/images/Postman/ProductsFiltration.png" width="900">
</p>

## Product Sorting

<p align="center">
<img src="docs/images/Postman/ProductsSort.png" width="900">
</p>

## Product Pagination

<p align="center">
<img src="docs/images/Postman/ProductsPagination.png" width="900">
</p>

## Product Brands

<p align="center">
<img src="docs/images/Postman/ProductBrands.png" width="900">
</p>

## Product Types

<p align="center">
<img src="docs/images/Postman/ProductTypes.png" width="900">
</p>

## Basket

<p align="center">
<img src="docs/images/Postman/Basket.png" width="900">
</p>

## Orders

<p align="center">
<img src="docs/images/Postman/Order.png" width="900">
</p>

## Payments

<p align="center">
<img src="docs/images/Postman/Payment.png" width="900">
</p>

## Error Handling

<p align="center">
<img src="docs/images/Postman/ErrorsHandling.png" width="900">
</p>

---

# Database

## ER Diagram

<p align="center">
<img src="docs/images/Database/ERD.png" width="900">
</p>

## Application Tables

<p align="center">
<img src="docs/images/Database/Matger_Tables.png" width="900">
</p>

## Identity Tables

<p align="center">
<img src="docs/images/Database/Matger_Identity_Tables.png" width="900">
</p>

---

# Redis

<p align="center">
<img src="docs/images/Redis/redis.png" width="900">
</p>

Redis is used to store customer shopping baskets, providing fast data access and improving application performance.

---

# API Modules

- Products
- Product Brands
- Product Types
- Basket
- Accounts
- Orders
- Payments

---

# Getting Started

```bash
git clone https://github.com/mohamedhabibb/Matger.git
```

```bash
cd Matger
```

Update your **appsettings.json** with:

- SQL Server Connection String
- JWT Settings
- Stripe Secret Key
- Redis Connection

Run:

```bash
dotnet restore
```

```bash
dotnet ef database update
```

```bash
dotnet run
```

Open Swagger:

```
https://localhost:xxxx/swagger
```

---

# Project Highlights

✔ Onion Architecture

✔ Repository Pattern

✔ Unit of Work

✔ Specification Pattern

✔ JWT Authentication

✔ ASP.NET Core Identity

✔ Stripe Payment Integration

✔ Redis Caching

✔ SQL Server

✔ Entity Framework Core

✔ Swagger Documentation

✔ Postman Collection

✔ Production-style REST API

---

# Author

**Mohamed Abdelnasser**

Backend .NET Developer
