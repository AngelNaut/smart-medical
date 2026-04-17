using SmartMedical.Application.DTOs.Patient;
using SmartMedical.Application.Interfaces;
using SmartMedical.Application.Services;
using SmartMedical.Domain.Interfaces;
using SmartMedical.Infrastructure.Repositories;
using SmartMedical.Infrastructure.Persistence;

namespace SmartMedical.Test;

public class PatientTests
{
    [Fact]
    public async Task RegisterPatientAsync_ValidData_ReturnsCreatedPatient()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IPatientRepository repository = new PatientRepository(context);

        var service = new PatientService(repository);

        var dto = new CreatePatientDto
        {
            FirstName = "Maria",
            LastName = "Garcia",
            DateOfBirth = new DateTime(1985, 3, 20),
            ContactNum = "809-555-9876"
        };

        var result = await service.RegisterPatientAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("Maria", result.FirstName);
        Assert.Equal("Garcia", result.LastName);
        Assert.True(result.PatientID > 0);
    }

    [Fact]
    public async Task RegisterPatientAsync_EmptyFirstName_ThrowsException()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IPatientRepository repository = new PatientRepository(context);

        var service = new PatientService(repository);

        var dto = new CreatePatientDto
        {
            FirstName = "",
            LastName = "Garcia",
            DateOfBirth = new DateTime(1985, 3, 20),
            ContactNum = "809-555-9876"
        };

        await Assert.ThrowsAsync<Exception>(() => service.RegisterPatientAsync(dto));
    }

    [Fact]
    public async Task RegisterPatientAsync_EmptyLastName_ThrowsException()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IPatientRepository repository = new PatientRepository(context);

        var service = new PatientService(repository);

        var dto = new CreatePatientDto
        {
            FirstName = "Maria",
            LastName = "",
            DateOfBirth = new DateTime(1985, 3, 20),
            ContactNum = "809-555-9876"
        };

        await Assert.ThrowsAsync<Exception>(() => service.RegisterPatientAsync(dto));
    }

    [Fact]
    public async Task RegisterPatientAsync_MissingDateOfBirth_ThrowsException()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IPatientRepository repository = new PatientRepository(context);

        var service = new PatientService(repository);

        var dto = new CreatePatientDto
        {
            FirstName = "Maria",
            LastName = "Garcia",
            DateOfBirth = default,
            ContactNum = "809-555-9876"
        };

        await Assert.ThrowsAsync<Exception>(() => service.RegisterPatientAsync(dto));
    }

    [Fact]
    public async Task GetAllPatientsAsync_ReturnsAllPatients()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IPatientRepository repository = new PatientRepository(context);

        var service = new PatientService(repository);

        await service.RegisterPatientAsync(new CreatePatientDto
        {
            FirstName = "Paciente1",
            LastName = "Test1",
            DateOfBirth = new DateTime(1990, 1, 1),
            ContactNum = "809-111-1111"
        });

        await service.RegisterPatientAsync(new CreatePatientDto
        {
            FirstName = "Paciente2",
            LastName = "Test2",
            DateOfBirth = new DateTime(1991, 2, 2),
            ContactNum = "809-222-2222"
        });

        var result = await service.GetAllPatientsAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetPatientByIdAsync_ExistingPatient_ReturnsPatient()
    {
        var context = TestDbContext.CreateWithData();
        IPatientRepository repository = new PatientRepository(context);

        var service = new PatientService(repository);

        var result = await service.GetPatientByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Juan", result.FirstName);
    }

    [Fact]
    public async Task GetPatientByIdAsync_NonExistingPatient_ReturnsNull()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IPatientRepository repository = new PatientRepository(context);

        var service = new PatientService(repository);

        var result = await service.GetPatientByIdAsync(999);

        Assert.Null(result);
    }
}