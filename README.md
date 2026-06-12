# System.SharedKernel

[![.NET 10](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
[![Architecture: DDD](https://img.shields.io/badge/Architecture-DDD-orange.svg)]()
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A reusable, enterprise-ready **Shared Kernel** designed using **Domain-Driven Design (DDD)** principles and functional programming concepts for .NET applications. This repository acts as a single source of truth for shared abstractions, domain building blocks, unified error handling, and common cross-cutting implementations across multiple boundaries or microservices.

By cleanly splitting abstractions from concrete implementations, this library allows your Core Domain to remain pristine and completely independent of external infrastructure frameworks.

---

## 📂 Repository Architecture & Layout

The codebase is split into two foundational layers to ensure maximum flexibility and decoupled architectures:

### 1. `SharedKernel.Abstract`
This project contains purely decoupled, framework-agnostic interfaces, abstract base classes, and universal types. Drop this reference directly into your **Domain Layer** (Core).

* 📂 **`Entity/`** — Core Domain tactical patterns.
  * `BaseEntity.cs`: Generic base class ensuring uniform identity tracking (`Id`) for all domain entities.
  * `AggregateRoot.cs`: Extends `BaseEntity` to encapsulate business invariant protection and track domain events.
  * `AuditLogEntity.cs`: A system entity tracking write/update timestamps and structural auditing metadata.
* 📂 **`Messaging/`** — Intra-system decoupling.
  * `IDomainEvent.cs`: Interface marker indicating state-changing system triggers.
* 📂 **`Repository/`** — Data abstraction contracts.
  * `IRepository.cs`: Generic repository interface contract to isolate your database layer from domain logic.
* 📂 **`Result/`** — Advanced structural error handling.
  * `IResult.cs` & `Result.cs`: Functional result wrappers encapsulating process states instead of using heavy exception-throwing patterns.
  * `Error.cs`: Extensible definitions of failure payloads (Code, Message, Kind).
* 📂 **`Shared/Enum/`** — Domain enums globally unique across the system context:
  * `AccountStatus.cs`, `ErrorKind.cs`, `AuthProvider.cs`, `DeviceType.cs`, `OtpType.cs`, etc.

### 2. `SharedKernel.Impl`
This project contains the technical runtime implementations of the abstract definitions. Inject this project into your **Infrastructure**, **Persistence**, or **API Presentation layers**.

* 📂 **`Api/`** — Global API Controller utilities.
  * `BaseApiController.cs`: A foundational controller that transforms functional `Result` objects seamlessly into clean RESTful standard HTTP responses (`200 OK`, `400 BadRequest`, `401 Unauthorized`).
* 📂 **`Model/`** — Shared Data Transfer Objects.
  * `BaseModel.cs`: Core blueprint model standard for payloads/DTOs mapping.

---

## 🚀 Key Patterns & How to Use Them

### 🧩 The Functional Result Pattern
Avoid using expensive `try-catch` structures for expected business failures. Use the expressive `Result` type:

```csharp
public Result<User> SuspendUserAccount(User user)
{
    if (user.Status == AccountStatus.Closed)
    {
        return Result.Failure<User>(Error.Validation("Account.Closed", "Cannot suspend an already closed account."));
    }

    user.Status = AccountStatus.Closed;
    return Result.Success(user);
}
