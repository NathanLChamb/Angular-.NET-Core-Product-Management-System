# Angular-.NET-Core-Product-Management-System

Full-stack product management system built with Angular, ASP.NET Core, and PostgreSQL featuring dynamic product variants, reactive forms, signals, and EF Core aggregate synchronization.

---

## Overview

The goal of this system is to provide a flexible way to manage products with complex variant structures while keeping both frontend and backend models consistent.

It simulates a real-world eCommerce product catalog where products can have:

- Categories
- Options (e.g. Color, Size)
- Option Values (e.g. Red, Blue, Large, Small)
- Dynamically generated product variants based on option combinations

The system supports full CRUD operations and keeps frontend and backend state synchronized across complex relational data.

---

## Key Features

- Automatic generation of product variants using a cartesian product algorithm
- Smart reconciliation logic that preserves manually edited variants
- Strongly typed Angular reactive forms
- EF Core-based relational data modeling
- RESTful API for products, categories, and options
- Centralized backend error handling middleware

---

## Tech Stack

### Frontend
- Angular
- TypeScript
- Reactive Forms
- rxResource
- Signals

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL

---

## Architecture Concepts

- Reactive state management
- Dynamic form reconciliation
- Aggregate synchronization
- DTO projection
- Relational data modeling
- Cartesian product generation
- Nested aggregate updates

---

## Screenshots

### Product Form
![Product Form](./screenshots/product-form.jpg)

### Product List
![Product List](./screenshots/product-list.jpg)

---

## Architecture Notes

### Dynamic Variant Generation

Product variants are generated from selected product options using a cartesian product algorithm. Existing variant rows are preserved through reconciliation logic using stable variant identity keys.

---

### Reactive Form Synchronization

The frontend uses Angular signals, computed values, and effects alongside Reactive Forms. Form state is synchronized with derived reactive state while preserving user edits during edit-mode hydration.

---

### Aggregate Update Pattern

The backend performs manual reconciliation of nested aggregates including:

- product categories
- product options
- product variants
- variant option values

This avoids blind entity replacement and maintains relational consistency.

---

## Form Architecture

The application uses strongly typed Angular Reactive Forms with:

- FormArray for dynamic variant rows
- FormControl<number[]> for generated relational values
- Shared create/edit form architecture
- Form seeding from backend DTOs
- Synchronization guards to prevent derived state overwrites

---

## Running the Project

### 1. Configuration

Create an `appsettings.json` file in the API project:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnectionString": "YOUR_CONNECTION_STRING"
  },
  "AllowedOrigins": [
    "http://localhost:5000",
    "http://localhost:5001",
    "http://localhost:4200"
  ]
}

```

### 2. Execution Steps

Backend:
dotnet ef database update
dotnet run

Frontend:
npm install
ng serve