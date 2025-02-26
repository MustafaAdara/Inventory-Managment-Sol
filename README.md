# **Inventory Management System**

An end-to-end **.NET 8** solution for managing **inventory, orders, transactions, and stock levels** across multiple **warehouses, suppliers, and consumers**. This project follows **Onion Architecture** principles for a clean and scalable design.

## **Table of Contents**

1. [Features](#features)
2. [Project Structure](#project-structure)
3. [Technologies Used](#technologies-used)
4. [Getting Started](#getting-started)
5. [Database Schemas](#database-schemas)
6. [Key Entities & Flows](#key-entities--flows)
7. [How to Contribute](#how-to-contribute)

---

## **Features**

- **Multi-Warehouse Support**: Track stock across multiple locations.
- **Order Management**: Handle supplier orders, consumer orders, and warehouse transfers.
- **Transaction Logging**: Maintain a historical record of all stock movements.
- **Onion Architecture**: Follows **Domain**, **Infrastructure**, **Application**, and **Presentation** layers.
- **Entity Framework Core**: Used for data access, migrations, and configurations.
- **Optimistic Concurrency**: Uses `RowVersion` fields to prevent conflicting updates.
- **Role-Based Security** *(Planned)*: ASP.NET Core Identity for user roles.

---

## **Project Structure**

```
MofaStockManagementSol/
│
├── MOFA.StockManagement.Domain
│   ├── Entities
│   │   ├── Item.cs
│   │   ├── Order.cs
│   │   ├── Transaction.cs
│   │   └── ... (Other Domain Models)
│   ├── Patterns
│   │   └── Repositories/Trackable
│   └── ...
│
├── MOFA.StockManagement.Infrastructure
│   ├── Data
│   │   ├── Contexts
│   │   │   ├── ApplicationDbContext.cs
│   │   │   └── Configuration
│   │   │       └── (EntityTypeConfiguration classes)
│   │   └── ...
│   ├── Migrations
│   ├── Repository
│   ├── Services
│   └── ...

├── MOFA.StockManagement.Api
│   ├── Service Api (Controllers)
│   ├── Identity Api
│   └── ...
│
├── MOFA.StockManagement.Application
│   ├── AppServices(HttpClients)
│   ├── IHttpClientHelper
│   ├── ViewModels
│   └── ...
│
├── MOFA.StockManagement.Presentation
│   ├── RazorPages
│   └── (Views, UI Logic)
│
├── MofaStockManagementSol.sln
└── README.md
```

- **Domain**: Contains core entities and domain logic.
- **Infrastructure**: Includes **Entity Framework Core DbContext**, configurations, migrations, repositories, and services.
- **API**: include service Api and indentity api.
- **Application**: Handles HttpClient and ViewModels.
- **Presentation**: Provides API controllers, and Razor Pages for UI interaction.

---

## **Technologies Used**

- **.NET 8 (C#)**
- **Entity Framework Core**
- **SQL Server**
- **Razor Pages**
- **Bootstrap / JavaScript / Ajax** *(for Responsive UI)*

---

## **Getting Started**

### **1. Clone the Repository**

```bash
git clone https://github.com/MustafaAdara/Inventory-Managment-Sol.git
cd Inventory-Managment-Sol
```

### **2. Set Up the Database**

1. **Configure the connection string** in `appsettings.json`.
2. **Apply migrations**:
   ```bash
   update-database -context MOFA.StockManagement.Infrastructure.Data.Contexts.DBContext
   ```

Note: Ensure that the **API project** is set as the startup project before updating the database.

### **3. Run the Project**

```bash
dotnet run --project MOFA.StockManagement.Presentation
```

- The **web application** will start on the configured port.
- **Run both the API and the Presentation layer** simultaneously for full functionality.

---

## **Database Schemas**

| **Schema Name** | **Entities**                                              |
| --------------- | --------------------------------------------------------- |
| `config`        | Warehouse, ItemType, UnitOfMeasure                        |
| `operation`     | Stock, Order, OrderDetail, Transaction, TransactionDetail |
| `sales`         | Supplier, Consumer, Item, ItemUnitOfMeasure               |

---

## **Key Entities & Flows**

### **1. Order**

- Linked to **OrderDetails** (1\:M)
- Has a **Transaction** for stock movement (1:1)

### **2. Transaction**

- Logs inventory changes (e.g., purchases, transfers, sales)
- References **Order, Supplier, Source/Destination Warehouse**

### **3. Stock**

- Always stored in **base units** (e.g., pieces)
- **ItemUnitOfMeasure** handles conversions for boxes/packs

### **4. Supplier & Consumer**

- Suppliers provide stock, Consumers purchase stock.

---

## **How to Contribute**

1. **Fork** the repository and create a new branch.
2. **Commit** changes with a descriptive message.
3. **Push** to your fork and create a **Pull Request**.
4. **Discuss and merge** after approval.

---

## **Contact & Future Plans**

- **Contact**: [mustafaadaraa@gmail.com](mailto\:mustafaadaraa@gmail.com) or GitHub [MustafaAdara](https://github.com/MustafaAdara)
- **Planned Features**:
  - **Role-Based Authentication** (Admin, Manager, etc.)
  - **Reporting & Analytics**
  - **Bulk Import/Export Features**

**Thank you for using and contributing to the Inventory Management System!** 🚀

