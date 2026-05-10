using AgenticAR.Application.Services;
using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.CO4029_BE.Tests.Helpers;
using CO4029_BE.Domain.Entities;
using FluentAssertions;
using Moq;

namespace CO4029_BE.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<ICurrentUserService> _currentUser = new();
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _sut = new UserService(_userRepo.Object, null!, null!, _currentUser.Object);
    }

    [Fact]
    public async Task RegisterCustomer_WhenEmailExists_ThrowsInvalidOperation()
    {
        _userRepo.Setup(x => x.GetByEmailAsync("dup@test.com"))
            .ReturnsAsync(TestDataFactory.CustomerUser());

        await _sut.Invoking(s => s.RegisterCustomerAsync(
            new CreateUserRequest { Email = "dup@test.com", Password = "pass" }))
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*Email đã được sử dụng*");
    }

    [Fact]
    public async Task UpdateUser_WhenValidToken_CallsUpdateAsync()
    {
        var existing = TestDataFactory.CustomerUser();
        existing.phone = "0900000000";
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(existing);

        var request = new UpdateUserRequest { Name = "Tên Mới" };
        await _sut.UpdateUserAsync(request, "token");

        _userRepo.Verify(x => x.UpdateAsync(existing.id, It.Is<User>(u => u.name == "Tên Mới")), Times.Once);
    }

    [Fact]
    public async Task UpdateUser_WhenPartialUpdate_KeepsOldValues()
    {
        var existing = TestDataFactory.CustomerUser();
        existing.phone = "0900000000";
        existing.gender = "Male";
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(existing);
        _userRepo.Setup(x => x.UpdateAsync(existing.id, It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        var result = await _sut.UpdateUserAsync(new UpdateUserRequest { Name = "Chỉ đổi tên" }, "token");

        result.Phone.Should().Be("0900000000");
        result.Gender.Should().Be("Male");
    }
}
