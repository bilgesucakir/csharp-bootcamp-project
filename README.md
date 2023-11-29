# Travel Planner API

This project is a Travel Planner API built using .NET 7.0 and Entity Framework Core 7.0.9. It uses Microsoft SQL Server LocalDB for data storage.

## Entities

### User

- **Fields:**
  - `Id` (int): Unique identifier for the user
  - `Name` (string): Name of the user
  - `Surame` (string): Surname of the user
  - `Email` (string): Email of the user, unique
  - `Trips` (List<Trip>): List of trips associated with the user

### Trip

- **Fields:**
  - `Id` (Guid): Unique identifier for the trip
  - `Title` (string): Title of the trip
  - `Description` (string): Description of the trip
  - `StartDate` (DateTime): Start date of the trip
  - `EndDate` (DateTime): End date of the trip
  - `Budget` (decimal): Budget allocated for the trip
  - `UserID` (int): Foreign key linking to the User entity
  - `User` (User): Navigation property for the associated user
  - `Excursions` (List<Excursion>): List of excursions associated with the trip

### Excursion

- **Fields:**
  - `ExcursionId` (Guid): Unique identifier for the excursion
  - `Name` (string): Name of the excursion
  - `StartDate` (DateTime): Start date of the excursion
  - `EndDate` (DateTime): End date of the excursion
  - `Location` (string): Location of the excursion
  - `Description` (string): Description of the excursion
  - `Cost` (decimal): Cost associated with the excursion
  - `TripID` (Guid): Foreign key linking to the Trip entity
  - `Trip` (Trip): Navigation property for the associated trip

## Entity Relationships

- Users can have multiple trips, but a trip can be associated with one user
- Trips can have multiple Excursions, but an xxcursion can be associated with one trip

## Database

This project uses Entity Framework Core 7.0.9 for data access, and the data is stored in Microsoft SQL Server LocalDB.

## API Endpoints

### User Endpoints

- **GET /api/users** (Get list of all users)
    - With optional filters: **GET /api/users?name=\{userName\}&surname=\{userSurname\}**
- **GET /api/users/\{userId\}** (Get a specific user by its id)
- **GET /api/users/\{userId\}/trips** (Get list of trips associated with given user id)
- **GET /api/users/\{userId\}/trips/details** (Get list of trip details associated with given user id)
- **POST /api/users** (Create a new user)
- **PUT /api/users** (Update user information/Create a new user)
- **DELETE /api/users/\{userId\}** (Delete a user)

### Trip Endpoints

- **GET /api/trips** (Get list of all trips)
    - With optional filters: **GET /api/trips?minBudget={minBudget}&maxBudget={maxBudget}&minStartDate={minStartDate}&maxStartDate={maxStartDate}&minEndDate={minEndDate}&maxEndDate={maxEndDate}**
- **GET /api/trips/details** (Get list of all trip details)
- **GET /api/trips/{tripId}** (Get a specific trip by its id)
- **GET /api/trips/{tripId}/details** (Get a spsecific trip details by its id)
- **GET /api/trips/{tripId}/excursions** (Get list of excursions associated with given trip id)
- **GET /api/trips/{tripId}/excursions/details** (Get list of excursion details associated with given trip id)
- **POST /api/trips** (Create a new trip)
- **PUT /api/trips** (Update trip information/Cretae a new trip)
- **DELETE /api/trips/{tripId}** (Delete a trip)

### Excursion Endpoints

- **GET /api/excursions** (Get list of all excursions)
    - With optional filters: **GET /api/excursions?minCost={minCost}&maxCost={maxCost}&location={location}&minStartDate={minStartDate}&maxStartDate={maxStartDate}&minEndDate={minEndDate}&maxEndDate={maxEndDate}**
- **GET /api/excursions/details** (Get list of all excursion details)
- **GET /api/excursions/{excursionId}** (Get a specific excursion by its id)
- **GET /api/excursions/{excursionId}/details** (Get a specific excursion details by its id)
- **POST /api/excursions** (Create a new excursion)
- **PUT /api/excursions** (Update excursion information/Create a new excursion)
- **DELETE /api/excursions/{excursionId}** (Delete an excursion)

(Trip details consists of user name, surname and email as extra fields to trip entity)
(Excursion details consists of trip title, start date and end date as extra fields to excursion entity)
