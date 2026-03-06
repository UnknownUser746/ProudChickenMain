# Proud Chicken — Exam Project

A Windows desktop application built with **WPF (.NET)** and **C#** for managing a chicken restaurant. Developed as a group exam project (Williamgruppe).

## About

Proud Chicken is a restaurant management system that handles customer data and orders through a clean WPF interface backed by a local SQL database.

## Features

- Customer management (create, view, update, delete)
- Local SQL database (`Kunder.bak`) for persistent data storage
- WPF desktop UI with intuitive navigation
- Built as a structured group exam project

## Tech Stack

| Technology | Purpose |
|---|---|
| C# / WPF | Desktop UI framework |
| SQL Server | Customer database |
| .NET | Runtime |
| Visual Studio | IDE |

## Project Structure

```
ProudChickenMain/
├── ProudChickenEksamenProjektWPFMain/   # Main WPF project folder
│   └── ...                              # Views, models, logic
├── ProudChickenEksamenProjektWPFMain.sln # Solution file
├── Kunder.bak                           # SQL Server database backup
└── .gitignore
```

## Getting Started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) with .NET desktop development workload
- SQL Server or SQL Server Express
- .NET 6.0 or later

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/UnknownUser746/ProudChickenMain.git
   ```

2. **Restore the database:**
   - Open SQL Server Management Studio (SSMS)
   - Right-click **Databases → Restore Database**
   - Select `Kunder.bak` as the source file
   - Click OK to restore

3. Open `ProudChickenEksamenProjektWPFMain.sln` in Visual Studio

4. Update the connection string in the project to point to your local SQL Server instance

5. Press **F5** to build and run

## Group

Developed by **Williamgruppe** as part of a school exam project.

## License

This project is open source and available for educational purposes.
