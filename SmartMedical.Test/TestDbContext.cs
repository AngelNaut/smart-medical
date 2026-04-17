using Microsoft.EntityFrameworkCore;
using SmartMedical.Domain.Entities;
using SmartMedical.Infrastructure.Persistence;

namespace SmartMedical.Test;

public static class TestDbContext
{
    public static SmartMedicalDbContext CreateInMemoryContext(string databaseName = "TestDatabase")
    {
        var options = new DbContextOptionsBuilder<SmartMedicalDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName + Guid.NewGuid().ToString())
            .Options;

        return new SmartMedicalDbContext(options);
    }

    public static SmartMedicalDbContext CreateWithData(string databaseName = "TestDatabase")
    {
        var context = CreateInMemoryContext(databaseName);

        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Juan",
            LastName = "Perez",
            DateOfBirth = new DateTime(1990, 5, 15),
            ContactNum = "809-555-1234"
        };

        context.Patients.Add(patient);
        context.SaveChanges();

        return context;
    }
}