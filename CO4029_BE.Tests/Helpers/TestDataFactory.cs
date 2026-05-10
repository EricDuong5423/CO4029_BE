using CO4029_BE.Domain.Entities;

namespace CO4029_BE.CO4029_BE.Tests.Helpers;

public static class TestDataFactory
{
    public static User EmployeeUser() => new()
        { id = "user-emp-1", email = "emp@test.com", role = "Employee" };

    public static User CustomerUser() => new()
        { id = "user-cus-1", email = "cus@test.com", role = "Customer" };

    public static Building SampleBuilding(string ownerId = "user-emp-1") => new()
        { ID = "bld-1", Name = "Tòa A1", Content = "Mô tả", UserId = ownerId };

    public static Category SampleCategory() => new()
        { ID = "cat-1", Name = "Danh mục test" };
}