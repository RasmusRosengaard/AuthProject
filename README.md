This project is my implementation of an easy setup Vue frontend with .NET Core Web API.

The purpose of this project is to demonstrate a simple frontend-backend authentication flow, showing how login, registration, and basic user state can work together in a Vue application with a .NET Core API.

Another purpose is for me to be able to quickly set up websites that require user handling for future projects.


IMPORTANT 
- Make sure your frontend runs on the port defined in program.cs to allow frontend to send Http request to backend. (:5173)


If you want to use token based backend, use the branch "feat/tokenbased"
- This version uses a local SQL database instead of SQLite
