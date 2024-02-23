using Autofac.Extras.Moq;
using Castle.Core.Internal;
using CrudAppDotNet8.Entities;
using Moq;
using Shouldly;

namespace CrudAPI.Tests.Unit;

public class UserServiceTest
{
    private AutoMock _mock;
    private Mock<DataContext> _context;
    private Mock<IMapper> _mapper;

    [OneTimeSetUp] // This method is called only once before any tests are run
    public void ClassSetup()
    {
        _mock = AutoMock.GetLoose();
    }

    [SetUp] // This method is called before each test
    public void TestSetup()
    {
        _context = _mock.Mock<DataContext>();
        _mapper = _mock.Mock<IMapper>();
    }

    [TearDown] // This method is called after each test
    public void TestCleanup()
    {
        _context?.Reset();
        _mapper?.Reset();
    }

    [OneTimeTearDown] // This method is called only once after all tests are run
    public void ClassCleanup() => _mock?.Dispose();

    [Test]
    public void GetById_ProvideInvalidId_ThrowException()
    {
        // Arrange
        _context.SetReturnsDefault(_context.Object);
        _mapper.SetReturnsDefault(_mapper.Object);

        var service = _mock.Create<UserService>();

        // Act
        Should.Throw<Exception>(() => service.GetById(0));

        // Assert
        //user?.ShouldBe(null);
        //user?.ShouldBeOfType<User>();
        //user?.Id.ShouldBe(0);
    }
}