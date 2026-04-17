using SmartMedical.Domain.Entities;

public class PriorityService
{
    public int CalculateScore(Appointment appointment)
    {
        int score = 0;

        // 🔹 1. Urgencia
        var urgency = appointment.UrgencyDescription.ToLower();

        if (urgency.Contains("emergencia"))
            score += 50;
        else if (urgency.Contains("urgente"))
            score += 30;
        else
            score += 10;

        // 🔹 2. Edad (calculada desde DateOfBirth)
        var patient = appointment.Patient;

        int age = DateTime.Now.Year - patient.DateOfBirth.Year;

        if (age >= 65) score += 40;
        else if (age >= 40) score += 20;
        else score += 10;

        // 🔹 3. Condiciones médicas
        if (patient.MedicalConditions != null && patient.MedicalConditions.Any())
            score += 30;

        // 🔹 4. Tiempo de espera (RequestedAt)
        int days = (DateTime.Now - appointment.RequestedAt).Days;

        if (days > 5) score += 30;
        else if (days >= 2) score += 20;
        else score += 10;

        return score;
    }

    public string GetLevel(int score)
    {
        if (score >= 100) return "Alta";
        if (score >= 60) return "Media";
        return "Baja";
    }
}