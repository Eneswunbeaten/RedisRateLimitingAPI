# Redis Rate Limiting API

This is a .NET 6 Web API demonstrating rate limiting using Redis. It prevents clients from exceeding a certain number of requests within a specific time window.

## Prerequisites
- .NET 8
- A running Redis instance

## Configuration
- Set your Redis connection string in the `appsettings` or in the `Program.cs` as shown in the example.

## Usage
1. Run the application.
2. Make requests to any of the available endpoints.
3. If the request limit is reached, the API will return a 429 status code.

## Docker and Redis Setup
1. To install Docker, download and install the appropriate version for your operating system from the official Docker website.
2. After completing the installation, open a terminal or command prompt to verify that the Docker service is running.
3. To run Redis in Docker, use the following commands:
   ```
   docker run --rm -p 6379:6379 --name rediscontainer -d redis
   ```
   We need to connect to redis cli for testing, for this we enter the following command. Here '0c8' means the first three characters of the CONTAINER ID of redis.
   ```
   docker exec -it 0c8 sh
   ```
4. To confirm Redis is running, list the running containers with `docker ps`.

## PROJECT
If you find this project helpful, please consider giving it a star 🌟🌟
