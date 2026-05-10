using AgenticAR.Application.Services;
using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.CO4029_BE.Tests.Helpers;
using CO4029_BE.Domain.Entities;
using FluentAssertions;
using Moq;

namespace CO4029_BE.Tests.Services;

public class HistoryServiceTests
{
    private readonly Mock<IHistoryRepository> _historyRepo = new();
    private readonly Mock<ICurrentUserService> _currentUser = new();
    private readonly HistoryService _sut;

    public HistoryServiceTests()
        => _sut = new HistoryService(_historyRepo.Object, _currentUser.Object);

    [Fact]
    public async Task CreateHistory_SetsUserIdFromToken()
    {
        var emp = TestDataFactory.EmployeeUser();
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(emp);
        var history = new History { id = "h-1", header = "Tìm đường", user_id = emp.id };
        _historyRepo.Setup(x => x.CreateAsync(It.IsAny<History>()))
            .ReturnsAsync(history);

        var result = await _sut.CreateHistory(
            new CreateHistoryRequest { Header = "Tìm đường" }, "token");

        _historyRepo.Verify(x => x.CreateAsync(
            It.Is<History>(h => h.user_id == emp.id)), Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetHistoryByUserId_ReturnsHistoriesOfCurrentUser()
    {
        var emp = TestDataFactory.EmployeeUser();
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(emp);
        _historyRepo.Setup(x => x.GetHistorysByUserId(emp.id))
            .ReturnsAsync(new[] { new History { id = "h-1", user_id = emp.id } });

        var result = await _sut.GetHistoryByUserId("token");

        result.Should().HaveCount(1);
    }
}