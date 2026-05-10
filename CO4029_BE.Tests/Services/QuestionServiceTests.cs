using AgenticAR.Application.Services;
using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.CO4029_BE.Tests.Helpers;
using CO4029_BE.Domain.Entities;
using FluentAssertions;
using Moq;

namespace CO4029_BE.Tests.Services;

public class QuestionServiceTests
{
    private readonly Mock<IQuestionRepository> _questionRepo = new();
    private readonly Mock<IAnswerRepository> _answerRepo = new();
    private readonly Mock<ICategoryRepository> _categoryRepo = new();
    private readonly Mock<ICurrentUserService> _currentUser = new();
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly QuestionService _sut;

    public QuestionServiceTests()
        => _sut = new QuestionService(
            _questionRepo.Object,
            _answerRepo.Object,
            _categoryRepo.Object,
            _currentUser.Object,
            _userRepo.Object);

    [Fact]
    public async Task CreateCategory_WhenEmployee_ReturnsCategory()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        var cat = TestDataFactory.SampleCategory();
        _categoryRepo.Setup(x => x.CreateAsync(It.IsAny<Category>()))
            .ReturnsAsync(cat);

        var result = await _sut.CreateCategory(new CreateCategoryRequest { Name = "Test" }, "token");

        result.Should().NotBeNull();
        result.Name.Should().Be("Danh mục test");
    }

    [Fact]
    public async Task CreateCategory_WhenCustomer_ThrowsUnauthorized()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.CustomerUser());

        await _sut.Invoking(s => s.CreateCategory(new CreateCategoryRequest { Name = "Test" }, "token"))
            .Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task UpdateCategory_WhenExists_CallsUpdateAsync()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _categoryRepo.Setup(x => x.GetByIdAsync("cat-1"))
            .ReturnsAsync(TestDataFactory.SampleCategory());

        var result = await _sut.UpdateCategory(new CreateCategoryRequest { Name = "Tên mới" }, "cat-1", "token");

        _categoryRepo.Verify(x => x.UpdateAsync("cat-1", It.IsAny<Category>()), Times.Once);
        result.Name.Should().Be("Tên mới");
    }

    [Fact]
    public async Task UpdateCategory_WhenNotFound_ThrowsKeyNotFound()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _categoryRepo.Setup(x => x.GetByIdAsync("cat-x"))
            .ReturnsAsync((Category)null!);

        await _sut.Invoking(s => s.UpdateCategory(new CreateCategoryRequest { Name = "X" }, "cat-x", "token"))
            .Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task DeleteCategory_WhenExists_CallsDeleteAsync()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _categoryRepo.Setup(x => x.GetByIdAsync("cat-1"))
            .ReturnsAsync(TestDataFactory.SampleCategory());

        var result = await _sut.DeleteCategory("cat-1", "token");

        result.Should().BeTrue();
        _categoryRepo.Verify(x => x.DeleteAsync("cat-1"), Times.Once);
    }

    [Fact]
    public async Task DeleteCategory_WhenNotFound_ThrowsKeyNotFound()
    {
        _currentUser.Setup(x => x.GetCurrentUser(It.IsAny<string>()))
            .ReturnsAsync(TestDataFactory.EmployeeUser());
        _categoryRepo.Setup(x => x.GetByIdAsync("cat-x"))
            .ReturnsAsync((Category)null!);

        await _sut.Invoking(s => s.DeleteCategory("cat-x", "token"))
            .Should().ThrowAsync<KeyNotFoundException>();
    }
}