# Galytix_BE_9920103092
Backend Project
Base Solution: I used Visual Studio 2019 to create a self-hosted web API server using .NET Core and Kestrel, listening on port 9091. The solution includes a base structure, and I've added a "CountryGwp" controller.

Data Source: The exercise mentions a "gwpByCountry.csv" file in the "Data" folder. For simplicity, I loaded this CSV file into memory on startup to simulate a database.

Endpoint: I implemented a POST endpoint at http://localhost:9091/api/gwp/avg. The endpoint takes a JSON payload with a "country" and "lob" (line of business) array, and it returns the average GWP (Gross Written Premium) for each line of business.
Example Input:

json
Copy code
{
  "country": "ae",
  "lob": ["property", "transport"]
}
Example Output:

json
Copy code
{
  "transport": 446001906.1,
  "liability": 634545022.9
}
Run/Testing Instructions:

Open the solution in Visual Studio (VS2019, VS2021, or VS2022).
Build and run the project.
Use a tool like Postman or CURL to send a POST request to http://localhost:9091/api/gwp/avg with the specified JSON payload.
Public Repository:

I have pushed the solution to a public repository on GitHub. You can find it here.
Bonus Points (Addressed):
Asynchronous Calls: I have implemented asynchronous calls for improved performance.
SOLID Principles: The solution follows SOLID principles, particularly the use of Dependency Injection (DI) and Inversion of Control (IoC) for better maintainability and flexibility.
Testing: Unit tests have been added using NUnit to validate data extraction. E2E tests can be added as needed.
Error Handling: Basic error handling has been added to handle unexpected scenarios gracefully.
