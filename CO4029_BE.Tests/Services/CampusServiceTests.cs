using AgenticAR.Application.Services;
using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.CO4029_BE.Tests.Helpers;
using CO4029_BE.Domain.Entities;
using FluentAssertions;
using Moq;

namespace CO4029_BE.Tests.Services;

public class CampusServiceTests
{
    private readonly Mock<IBuildingRepository> _buildingRepo = new();
    private readonly Mock<ICurrentUserService> _currentUser = new();
    private readonly CampusService _sut;

    public CampusServiceTests()
        => _sut = new CampusService(_buildingRepo.Object, _currentUser.Object);

    [Fact]
    public async Task GetAllBuildings_WhenEmployee_ReturnsAll()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _buildingRepo.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new[] { TestDataFactory.SampleBuilding() });

        var result = await _sut.GetAllBuildings("token");

        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllBuildings_WhenCustomer_ThrowsUnauthorized()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.CustomerUser());

        await _sut.Invoking(s => s.GetAllBuildings("token"))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task GetBuildingById_WhenEmployee_ReturnsBuilding()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _buildingRepo.Setup(x => x.GetByIdAsync("bld-1"))
            .ReturnsAsync(TestDataFactory.SampleBuilding());

        var result = await _sut.GetBuildingById("bld-1", "token");

        result.Should().NotBeNull();
        result.Name.Should().Be("Tòa A1");
    }

    [Fact]
    public async Task CreateBuilding_WhenEmployee_CallsCreateAsync()
    {
        var emp = TestDataFactory.EmployeeUser();
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(emp);
        var request = new CreateBuildingRequest { Name = "Tòa B2", Content = "Mô tả" };
        var building = new Building { ID = "bld-2", Name = "Tòa B2", Content = "Mô tả", UserId = emp.id };
        _buildingRepo.Setup(x => x.CreateAsync(It.IsAny<Building>()))
            .ReturnsAsync(building);

        var result = await _sut.CreateBuilding(request, "token");

        _buildingRepo.Verify(x => x.CreateAsync(It.IsAny<Building>()), Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteBuilding_WhenOwner_ReturnsTrueAndCallsDelete()
    {
        var emp = TestDataFactory.EmployeeUser();
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(emp);
        _buildingRepo.Setup(x => x.GetByIdAsync("bld-1"))
            .ReturnsAsync(TestDataFactory.SampleBuilding(emp.id));

        var result = await _sut.DeleteBuilding("bld-1", "token");

        result.Should().BeTrue();
        _buildingRepo.Verify(x => x.DeleteAsync("bld-1"), Times.Once);
    }

    [Fact]
    public async Task DeleteBuilding_WhenNotOwner_ThrowsUnauthorized()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _buildingRepo.Setup(x => x.GetByIdAsync("bld-1"))
            .ReturnsAsync(TestDataFactory.SampleBuilding("other-user-id"));

        await _sut.Invoking(s => s.DeleteBuilding("bld-1", "token"))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task DeleteBuilding_WhenNotFound_ThrowsKeyNotFound()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _buildingRepo.Setup(x => x.GetByIdAsync("bld-x"))
            .ReturnsAsync((Building)null!);

        await _sut.Invoking(s => s.DeleteBuilding("bld-x", "token"))
            .Should().ThrowAsync<KeyNotFoundException>();
    }
}