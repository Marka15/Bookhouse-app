Bookhouse — Library Manager App
A full-stack web application for managing a library with books and authors.

Tech Stack
Backend: ASP.NET Core 8, Entity Framework Core, PostgreSQL, JWT
Frontend: React (Vite), React Router, Axios
Database: PostgreSQL 17

Prerequisites:

.NET 8 SDK
Node.js 18+
PostgreSQL 17

Getting Started:
1.  Clone the repository
    git clone https://github.com/Marka15/Bookhouse-app.git
    cd Bookhouse-app

2. Create the database

    CREATE DATABASE bookhouse_db;

3. Backend setup
  
    cd Bookhouse.Api

Right-click the project in Visual Studio → Manage User Secrets and paste:

    {
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=bookhouse_db;Username=postgres;Password=YOUR_PASSWORD"
  },
  "Jwt": {
    "Key": "your_super_secret_key_bookhouse_2024_very_long"
  }
}

Replace YOUR_PASSWORD with your local PostgreSQL password.

Run migrations:

dotnet ef database update
Start the API:
dotnet run

4. Frontend setup
cd Bookhouse.Client
npm install
npm run dev

Test credentials
Email:    admin@bookhouse.com
Password: Admin123



Features

Log in / log out with JWT authentication
View all books with pagination (8 per page)
Search books by title or author name
Sort books by title, price, or year
Add a new book with cover image URL
Edit book details
Delete a book
Add a new author
Delete an author and all their books
Export book list to CSV


Database Schema
users         — admin login
authors       — book authors
books         — books with cover URL
genres        — pre-seeded genres
book_authors  — many-to-many: books <-> authors
book_genres   — many-to-many: books <-> genres

Notes

Genres are seeded automatically on first run
Admin user is created automatically on first run
Cover images use URLs from Open Library