# Introduction 
This is a project template implementing an N-Layer architecture. In this template, we utilize four distinct layers, each serving a specific role within the application:

1. **API Layer**
Description: This layer is responsible for handling incoming HTTP requests and returning appropriate HTTP responses. It acts as the entry point for the client applications, processing requests, invoking the necessary application services, and formatting the data to be sent back to the client. This layer ensures that the application can interact with external clients through well-defined API endpoints.

2. **services Layer**
Description: This layer contains the business logic and orchestrates the overall application flow. It processes the data received from the API layer, applies business rules, and coordinates with the DataAccess layer to retrieve or persist data. This layer serves as an intermediary between the API and DataAccess layers.

3. **DataAccess Layer**
Description: This layer is responsible for communicating with the database. It provides an abstraction over the data sources, enabling the application to perform CRUD (Create, Read, Update, Delete) operations. This layer ensures that the data access code is separated from the business logic, promoting a clean separation of concerns and making the application easier to maintain and test.

4. **Core Layer**
Description: This layer contains the fundamental building blocks of the application, including domain entities, Enum, Constants, and shared utilities. It defines the core concepts and contracts that are used across other layers. By centralizing these core components, this layer promotes reusability and consistency within the application, ensuring that the essential elements are easily accessible and maintainable.



# Apply Migrations
------------- 
TODO:  Add-Migration 'Intial' -OutputDir "Persistence/Migrations" 

