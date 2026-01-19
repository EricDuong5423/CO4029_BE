using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CO4029_BE.API.Contracts.Examples;

public class ExampleSchemaFilter: ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(CreateUserRequest))
        {
            schema.Example = new OpenApiObject
            {
                { "email", new OpenApiString("tuduong05042003@gmail.com") },
                { "password", new OpenApiString("tu05042003") },
                { "name", new OpenApiString("Duong Thanh Tu") },
                { "phone", new OpenApiString("0966866203") },
                { "birthday", new OpenApiString("2025-12-11T05:33:56.193Z") },
                { "gender", new OpenApiString("Male") }
            };
        }

        // Thêm ví dụ cho LogInRequest
        if (context.Type == typeof(LoginRequest))
        {
            schema.Example = new OpenApiObject
            {
                { "email", new OpenApiString("tuduong05042003@gmail.com") },
                { "password", new OpenApiString("tu05042003") }
            };
        }

        if (context.Type == typeof(LoginResponse))
        {
            schema.Example = new OpenApiObject
            {
                { "accessToken", new OpenApiString("eyJhbGciOiJIUzI1NiIsImtpZCI6InNDZU9xLzBEUWY2OHlPWjAiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2JmdXZpYW9seWZ4ZGhkaHNzYmFxLnN1cGFiYXNlLmNvL2F1dGgvdjEiLCJzdWIiOiIxY2U3ZGU3OC0zNDhlLTRhZTEtYjhmZi1mNTA2YjYyOThjMmIiLCJhdWQiOiJhdXRoZW50aWNhdGVkIiwiZXhwIjoxNzY1NDM1MjA5LCJpYXQiOjE3NjU0MzE2MDksImVtYWlsIjoidHVkdW9uZzA1MDQyMDAzQGdtYWlsLmNvbSIsInBob25lIjoiIiwiYXBwX21ldGFkYXRhIjp7InByb3ZpZGVyIjoiZW1haWwiLCJwcm92aWRlcnMiOlsiZW1haWwiXX0sInVzZXJfbWV0YXRhdGEiOnsiYmlydGhkYXkiOiIyMDI1LTEyLTAyIiwiZW1haWwiOiJ0dWR1b25nMDUwNDIwMDNAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsIm5hbWUiOiJEdW9uZyBUaGFuaCBUdSIsInBob25lIjoiMDk2Njg2NjIwMyIsInBob25lX3ZlcmlmaWVkIjpmYWxzZSwic3ViIjoiMWNlN2RlNzgtMzQ4ZS00YWUxLWI4ZmYtZjUwNmI2Mjk4YzJiIn0sInJvbGUiOiJhdXRoZW50aWNhdGVkIiwiYWFsIjoiYWFsMSIsImFtciI6W3sibWV0aG9rIjoicGFzc3dvcmQiLCJ0aW1lc3RhbXAiOjE3NjU0MzE2MDl9XSwic2Vzc2lvbl9pZCI6ImE5NTk5MGFlLTFkYmUtNDQwZS04YWE4LWQ2Nzc4NzMxYjFjOSIsImlzX2Fub255bW91cyI6ZmFsc2V9.gwTcbv9CvL1NqOoCi3hUh-20rV1elBpB0Q_Sl2B932g") },
                { "refreshToken", new OpenApiString("mylqn2p7vq2l") },
                { "expiresAt", new OpenApiString("2025-12-11T06:40:09.513311") }
            };
        }

        if (context.Type == typeof(UserReponse))
        {
            schema.Example = new OpenApiObject
            {
                { "email", new OpenApiString("tuduong05042003@gmail.com") },
                { "name", new OpenApiString("Duong Thanh Tu") },
                { "phone", new OpenApiString("0966866203") },
                { "birthday", new OpenApiString("2025-12-11T05:33:56.193Z") },
                { "role", new OpenApiString("Customer") },
                { "gender", new OpenApiString("Male") }
            };
        }

        if (context.Type == typeof(UpdateUserRequest))
        {
            schema.Example = new OpenApiObject
            {
                { "name", new OpenApiString("Duong Thanh Tu") },
                { "phone", new OpenApiString("0966866203") },
                { "birthday", new OpenApiString("2025-12-11T05:33:56.193Z") },
                { "gender", new OpenApiString("Male") }
            };
        }

        if (context.Type == typeof(ChangePasswordRequest))
        {
            schema.Example = new OpenApiObject
            {
                {"email", new OpenApiString("tuduong05042003@gmail.com")},
                { "newPassword", new OpenApiString("123456789") },
                { "oldPassword", new OpenApiString("tu05042003") },
                {"otpCode", new OpenApiString("123456") },
            };
        }

        if (context.Type == typeof(SendOTPRequest))
        {
            schema.Example = new OpenApiObject
            {
                { "Email", new OpenApiString("tuduong05042003@gmail.com") }
            };
        }

        if (context.Type == typeof(CreateHistoryRequest))
        {
            schema.Example = new OpenApiObject
            {
                { "Header", new OpenApiString("Where is B4?") },
                { "Date", new OpenApiString("2025-12-11T06:40:09.513311") }
            };
        }

        if (context.Type == typeof(HistoryReponse))
        {
            schema.Example = new OpenApiObject
            {
                { "Header", new OpenApiString("Where is B4?") },
                { "Create_date", new OpenApiString("2025-12-11T06:40:09.513311") }
            };
        }
    }
}