# 🎟️ Event Viewer App

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
cd src/EventViewer.Api
dotnet restore
dotnet run