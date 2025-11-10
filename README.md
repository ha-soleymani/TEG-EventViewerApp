﻿# 🎟️ Event Viewer App

A responsive, cloud-hosted web application built with .NET 8 and React. It displays events by venue, handles unreliable data sources with fallback logic, and follows Clean Architecture principles for maintainability and testability.

## 📦 Tech Stack

- **Backend**: ASP.NET Core (.NET 8), Clean Architecture
- **Frontend**: React 18, TypeScript, Bootstrap 5
- **Resilience**: Polly (fallback + retry)
- **Testing**: xUnit, Moq, RichardSzalay.MockHttp, Jest, Testing Library
- **Hosting**: Azure App Service / AWS Amplify

## 🛠️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/) (v18+)

### Backend Setup

```bash
cd backend\src\WebApi
dotnet restore
dotnet run
````
API will be available at https://localhost:5001.

### Frontend Setup
```bash
cd frontend
npm install
npm run dev
```
Frontend will be available at http://localhost:5173.

🧪 Running Tests
## Backend Tests
```bash
cd src/EventViewer.Tests
dotnet test
````
## Frontend Tests
```bash
cd client
npm test
```

## 🧯 Fallback Strategy
If the external event API is unavailable, the backend automatically loads a local fallback file located at:
/Fallback/events-fallback.json


Ensure this file is deployed with your API.

## 📄 Sample JSON Schema
```json
{
  "events": [
    {
      "id": 10000,
      "name": "10cc In Concert",
      "startDate": "2022-11-10T12:00:00Z",
      "venueId": 121
    }
  ],
  "venues": [
    {
      "id": 121,
      "name": "The Coding Theatre",
      "capacity": 450,
      "location": "TEG Metaverse"
    }
  ]
}
```