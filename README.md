# Task Insight Engine

Task Insight Engine is a project monitoring and risk intelligence platform for teams. It provides real-time task tracking, automated risk analysis, and live collaboration tools to improve project visibility and team coordination.

---

## Problem Statement

Software teams commonly struggle with inaccurate status updates, repeated follow-ups, delayed reporting, and poor project visibility. This leads to reactive decision-making, weak team coordination, and low confidence in task data.

---

## Solution Overview

Task Insight Engine provides a centralized platform for tracking tasks, monitoring risks, and managing project progress in real time.

- Real-time task and risk updates
- Automated risk scoring and monitoring
- Live collaboration using SignalR
- Scheduled background analysis and reporting
- Centralized dashboards for project health

---

## Features

### User Management
- User registration and authentication
- JWT-based access with refresh token support (Redis-backed)
- Secure session management and logout

### Project Management
- Create and manage projects
- Add or remove project members
- Project dashboards and health metrics

### Task Management
- Create, assign, and track tasks
- Daily progress updates with full history
- Blocker and impediment tracking
- Task-level comments

### Risk Intelligence System (Core Feature)
- Automated task risk scoring
- Project-level risk dashboard
- Configurable scoring rules and thresholds

### Real-Time Collaboration
- Live discussions powered by SignalR
- Instant notifications on risk changes
- Group-based communication per task or impediment

### Risk Subscriptions & Automation
- Subscribe to project risk updates
- Scheduled background jobs via Hangfire
- Automated daily risk analysis and briefing generation

---

## Application Flow

1. Register as a Project Manager or Developer.
2. Project Managers can create projects and add team members.
3. Project Managers and Developers can create and assign tasks.
4. Team members can update progress, report blockers, and add comments.
5. Project Managers can subscribe to daily risk updates through the risk settings section.
6. The system automatically analyzes tasks and generates risk scores.
7. Project Managers can view project health and risk insights from the dashboard.

---

## Architecture & Design

Built with Clean Architecture.

- Swagger documentation
- Structured logging via Serilog
- Background processing via Hangfire
- Real-time communication via SignalR

---

## Authentication & Security

- Short-lived JWT access tokens with refresh token rotation
- HTTP-only secure cookies
- Redis-backed session management

---

## Database

- PostgreSQL — primary relational database
- Entity Framework Core — Code-First with Fluent API configuration
- EF Core migrations for schema management

---

## Prerequisites

- Docker
- Docker Compose

---

## Configure

An `.env.example` file is included in the repository. Create your local environment file:


Update the values in `.env` to match your local setup.

> Sensitive configuration such as database credentials, JWT keys, and Redis settings are protected using git-secrets.

---

## Run

```bash
docker compose up --build -d
```

This starts four services:

| Service | Port | Description |
|---|---|---|
| taskinsight-frontend | 3000 | Next.js frontend |
| taskinsight-api | 5000 | ASP.NET Core API |
| taskinsight-postgres | 5432 | PostgreSQL database |
| taskinsight-redis | 6379 | Redis cache & session store |

---

## Usage

Open the frontend in your browser:

```
http://localhost:3000
```

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Next.js (TypeScript) |
| Backend | ASP.NET Core Web API (.NET 9) |
| Architecture | Clean Architecture |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Caching | Redis |
| Authentication | JWT Authentication |
| Real-Time Communication | SignalR |
| Background Processing | Hangfire |
| Logging & Monitoring | Serilog |
| Containerization | Docker + Docker Compose |
| API Documentation | Swagger |
