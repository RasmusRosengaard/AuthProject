This branch uses JWT tokens for authorization instead of saving the user's email directly in localStorage.

It still uses localStorage to store the token, to minimize backend requests.
Uses a SQL database, JWT tokens, and Entity Framework.

IMPORTANT:

Make sure your frontend runs on the port defined in Program.cs to allow it to send HTTP requests to the backend (default: :5173).

To run:

Navigate to the webapi/ folder and run:

dotnet run

Navigate to the Frontend/ folder and run:

npm run dev

This setup is intended for development purposes only.

Future work:

Focus on applying SOLID principles where they make sense to improve testing and scalability.