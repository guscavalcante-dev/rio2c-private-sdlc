# PlataformaRio2C Architecture Documentation

## 1. Overall System Architecture

PlataformaRio2C (MyRio2C) is built using a multi-layered architecture following Domain-Driven Design (DDD) principles and the Command Query Responsibility Segregation (CQRS) pattern. The system is implemented as an ASP.NET MVC application with separation of concerns distributed across multiple layers, each with its own responsibility.

### Major Components

```mermaid
graph TD
    PL[Presentation Layer] --> SL[Services Layer]
    SL --> AL[Application Layer]
    AL --> DL[Domain Layer]
    AL --> IL[Infrastructure Layer]
    DL --> IL
    
    subgraph "Presentation Layer"
        Web_Admin[Web.Admin]
        Web_Site[Web.Site]
    end

    subgraph "Services Layer"
        WebApi[WebApi Service]
    end

    subgraph "Application Layer"
        Application[Application Services]
        HubApplication[Real-time Hub]
        CQRS[CQRS Components]
    end

    subgraph "Domain Layer"
        Entities[Domain Entities]
        Validation[Validation Rules]
        Interfaces[Domain Interfaces]
    end

    subgraph "Infrastructure Layer"
        Data[Data Access]
        CrossCutting[Cross-cutting Concerns]
        FileStorage[File Storage]
        Report[Reporting Services]
    end

    Web_Admin -.-> WebApi
    Web_Site -.-> WebApi
    WebApi --> Application
    WebApi --> HubApplication
    Application --> CQRS
    CQRS --> Entities
    CQRS --> Data
    HubApplication --> Entities
    HubApplication --> Data
    Entities --> Validation
    Entities -.-> Interfaces
    Data --> Interfaces
    Data --> CrossCutting
    FileStorage --> CrossCutting
    Report --> CrossCutting
```

### Layer Responsibilities

1. **Presentation Layer**:
   - `PlataformaRio2C.Web.Admin`: Administrative interface for managing content, users, and events
   - `PlataformaRio2C.Web.Site`: User-facing website for participants to register, submit projects, and manage their profiles

2. **Services Layer**:
   - `PlataformaRio2C.WebApi`: REST API service exposing system functionality to front-end clients

3. **Application Layer**:
   - `PlataformaRio2C.Application`: Application services that orchestrate domain operations
   - `PlataformaRio2C.HubApplication`: Real-time communication hub for notifications and instant messaging
   - CQRS Components:
     - Commands: Data-changing operations
     - Queries: Data-retrieval operations
     - Command/Query Handlers: Process commands and queries

4. **Domain Layer**:
   - `PlataformaRio2C.Domain`: Core business entities, services, and validation rules
   - Domain Models: Business entities and aggregates
   - Validation Rules: Business validation logic
   - Interfaces: Repository and service interfaces

5. **Infrastructure Layer**:
   - Data:
     - `PlataformaRio2C.Infra.Data.Context`: Database context and entity mappings
     - `PlataformaRio2C.Infra.Data.Repository`: Repository implementations
     - `PlataformaRio2c.Infra.Data.FileRepository`: File storage implementations
   - Cross-cutting:
     - `PlataformaRio2C.Infra.CrossCutting.Identity`: Authentication and authorization
     - `PlataformaRio2C.Infra.CrossCutting.IOC`: Dependency injection configuration
     - `PlataformaRio2C.Infra.CrossCutting.CQRS`: CQRS framework integration
     - `PlataformaRio2C.Infra.CrossCutting.Resources`: Localization resources
     - `PlataformaRio2C.Infra.CrossCutting.Tools`: Common utilities
     - `PlataformaRio2C.Infra.CrossCutting.SalesPlatforms`: Sales platform integrations
     - `PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms`: Social media integrations
   - Report:
     - `PlataformaRio2C.Infra.Report`: Report generation services

## 2. Component Relationships and Dependencies

### Dependency Flow

The PlataformaRio2C architecture follows the Dependency Inversion Principle, with dependencies flowing from outer layers inward:

```mermaid
flowchart TD
    PL[Presentation Layer] --> SL[Services Layer]
    SL --> AL[Application Layer]
    AL --> DL[Domain Layer]
    
    IL[Infrastructure Layer] -.-> DL
    IL -.-> AL
    IL -.-> SL
    IL -.-> PL
    
    style IL fill:#f9f,stroke:#333,stroke-width:1px
    style DL fill:#bbf,stroke:#333,stroke-width:2px
```

The infrastructure layer (cross-cutting concerns) provides services to all layers but depends only on abstractions defined in the domain layer.

### Key Dependencies

1. **Web Projects → Application Layer**:
   - The Web.Admin and Web.Site projects depend on the Application layer for business operations
   - WebApi depends on Application services to process requests

2. **Application Layer → Domain Layer**:
   - Application services depend on domain entities and services
   - Command/Query handlers use domain repositories and services

3. **Infrastructure → Domain Interfaces**:
   - Repositories implement domain interfaces
   - Infrastructure services implement domain service interfaces

4. **Cross-Project Dependencies**:
   - CQRS components connect application and domain layers
   - IoC container wires up all dependencies

### Dependency Injection

The system uses SimpleInjector for dependency injection. Dependencies are registered in bootstrapper classes:

```mermaid
graph TD
    IoCBootStrapper --> RegisterServices
    RegisterServices --> RegisterRepositories
    RegisterRepositories --> Container[SimpleInjector Container]
    
    CQRSBootStrapper --> RegisterCQRSServices
    RegisterCQRSServices --> Container
    
    FileRepositoryBootStrapper --> RegisterFileRepositories
    RegisterFileRepositories --> Container
    
    SiteIoCBootStrapper --> RegisterSiteServices
    RegisterSiteServices --> Container
    
    HubBootStrapper --> RegisterHubServices
    RegisterHubServices --> Container
```

## 3. Data Flow Diagrams and Descriptions

### Request Processing Flow

```mermaid
sequenceDiagram
    actor Client
    participant UI as Web UI
    participant API as Web API
    participant AppService as Application Service
    participant Command as Command Handler
    participant Domain as Domain Entities
    participant Repo as Repository
    participant DB as Database
    
    Client->>UI: User Action
    UI->>API: HTTP Request
    API->>AppService: Invoke Service
    AppService->>Command: Create & Send Command
    Command->>Domain: Apply Business Rules
    Command->>Repo: Store Changes
    Repo->>DB: Persist Data
    DB-->>Repo: Confirmation
    Repo-->>Command: Success/Failure
    Command-->>AppService: Validation Result
    AppService-->>API: Response
    API-->>UI: HTTP Response
    UI-->>Client: Update UI
```

### CQRS Data Flow

The system uses the CQRS pattern to separate read and write operations:

```mermaid
graph TD
    subgraph "Command Flow"
        Client1[Client] --> Command[Command]
        Command --> CommandHandler[Command Handler]
        CommandHandler --> DomainModel[Domain Model]
        DomainModel --> Repository[Repository]
        Repository --> Database[(Database)]
    end
    
    subgraph "Query Flow"
        Client2[Client] --> Query[Query]
        Query --> QueryHandler[Query Handler]
        QueryHandler --> ReadModel[Read Model/Repository]
        ReadModel --> Database
    end
```

### Authentication Flow

```mermaid
sequenceDiagram
    actor User
    participant Browser
    participant WebApp
    participant IdentityService
    participant Database
    
    User->>Browser: Enter Credentials
    Browser->>WebApp: POST /Account/Login
    WebApp->>IdentityService: Validate Credentials
    IdentityService->>Database: Query User
    Database-->>IdentityService: User Data
    IdentityService->>WebApp: Authentication Result
    
    alt Successful Authentication
        WebApp->>Browser: Set Authentication Cookie
        Browser-->>User: Redirect to Dashboard
    else Failed Authentication
        WebApp->>Browser: Error Message
        Browser-->>User: Display Error
    end
```

## 4. Design Patterns Implemented

### Architectural Patterns

1. **Layered Architecture**:
   - Segregates the system into distinct layers
   - Each layer has a specific responsibility
   - Dependencies flow from outer to inner layers

2. **Domain-Driven Design (DDD)**:
   - Focus on core domain logic
   - Rich domain models with business rules
   - Separation of entities, value objects, and services

3. **Command Query Responsibility Segregation (CQRS)**:
   - Commands for state changes (write operations)
   - Queries for data retrieval (read operations)
   - Separate command and query handlers
   - MediatR for in-process messaging

### Structural Patterns

1. **Repository Pattern**:
   - Abstracts data persistence operations
   - Repositories for each domain entity
   - Generic repository implementation with type-specific extensions

2. **Unit of Work Pattern**:
   - Coordinates operations across multiple repositories
   - Ensures atomicity of related changes
   - Implemented in the `UnitOfWorkWithLog` class

3. **Dependency Injection (DI)**:
   - SimpleInjector for IoC container
   - Constructor injection throughout the codebase
   - Registered dependencies in bootstrapper classes

4. **Factory Pattern**:
   - `RepositoryFactory` for creating repositories
   - `FileRepositoryFactory` for file storage strategy
   - `SalesPlatformServiceFactory` for external service integration

### Behavioral Patterns

1. **Strategy Pattern**:
   - File storage strategy (AWS or local)
   - Configurable via application settings
   - Implemented through `IFileRepository` interface

2. **Mediator Pattern**:
   - MediatR for dispatching commands and queries
   - Decouples senders and receivers
   - Centralized request handling

3. **Template Method Pattern**:
   - Base handlers with common functionality
   - Specialized handlers for specific commands/queries

## 5. Technical Decisions and Rationales

### Framework and Language Selection

1. **ASP.NET MVC with .NET Framework 4.8**:
   - Mature framework with robust ecosystem
   - Strong typing and compilation benefits
   - Integration with Microsoft technologies

2. **Entity Framework 6.x with SQL Server**:
   - Object-relational mapping for data access
   - Code-first approach for entity definition
   - Integration with SQL Server

3. **CQRS Implementation**:
   - Separation of read and write operations
   - MediatR for command/query dispatching
   - Improved performance for read-heavy operations

### Architectural Decisions

1. **Domain-Driven Design (DDD)**:
   - Rationale: Complex business domain with sector-specific rules
   - Benefit: Focuses on core business logic and domain experts' language
   - Implementation: Rich domain models with business rules encapsulated in entities

2. **Multi-layered Architecture**:
   - Rationale: Separation of concerns and maintainability
   - Benefit: Modular development, testability, and scalability
   - Implementation: 5 distinct layers with clear responsibilities

3. **CQRS Pattern**:
   - Rationale: Different requirements for read and write operations
   - Benefit: Optimization of read/write paths, scalability
   - Implementation: Command/Query handlers with MediatR

### Technology Selection

1. **SimpleInjector for DI**:
   - Rationale: Performance and feature-rich DI container
   - Benefit: Registration by convention, diagnostic tools
   - Implementation: Bootstrapper classes for component registration

2. **AWS/Local File Storage**:
   - Rationale: Flexibility in deployment environments
   - Benefit: Development in local environment, production in cloud
   - Implementation: Strategy pattern with configurable implementation

3. **Localization and Internationalization**:
   - Rationale: Multi-language support requirement
   - Benefit: Global user base with localized content
   - Implementation: Resource files with culture-specific variations

## 6. System Boundaries and External Integrations

### System Boundaries

1. **User Interface Boundaries**:
   - Web interfaces (Admin and Site)
   - REST API for programmatic access
   - Real-time communication via SignalR hub

2. **Persistence Boundaries**:
   - SQL Server database for structured data
   - File storage (local or AWS S3) for unstructured data

3. **Authentication Boundaries**:
   - Custom identity implementation
   - Role-based access control
   - JWT token authentication for API

### External Integrations

1. **AWS S3 Integration**:
   - Purpose: Cloud-based file storage
   - Integration point: `FileAwsRepository` implementation
   - Configuration: AWS credentials in Web.config

2. **Sales Platform Integration**:
   - Purpose: Ticket sales and registration
   - Integration point: `SalesPlatformServiceFactory` 
   - Features: Webhook processing, participant synchronization

3. **Social Media Platform Integration**:
   - Purpose: Social media content aggregation
   - Integration point: `SocialMediaPlatformServiceFactory`
   - Features: Instagram publications synchronization

4. **Email Services**:
   - Purpose: Notification and communication
   - Integration point: SMTP configuration in Web.config
   - Features: Email templates for various system events

### Integration Patterns

```mermaid
graph TD
    System[PlataformaRio2C System] --> FileStorage[File Storage Integration]
    System --> SalesPlatforms[Sales Platforms Integration]
    System --> SocialMedia[Social Media Integration]
    System --> Email[Email Service Integration]
    
    FileStorage --> LocalFS[Local File System]
    FileStorage --> AWS[AWS S3]
    
    SalesPlatforms --> WebhookProcessing[Webhook Processing]
    SalesPlatforms --> AttendeeSynchronization[Attendee Synchronization]
    
    SocialMedia --> Instagram[Instagram API]
    
    Email --> SMTP[SMTP Service]
    Email --> Templates[Email Templates]
```

## Conclusion

The PlataformaRio2C system follows a robust multi-layered architecture incorporating Domain-Driven Design principles and the CQRS pattern. It uses a variety of design patterns to address specific concerns and integrates with external systems through well-defined boundaries and interfaces. The architecture promotes maintainability, scalability, and separation of concerns while providing a flexible framework for implementing the complex business requirements of a multi-sector event management platform.