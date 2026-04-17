namespace SmartMedical.Domain.Entities;

public class PriorityScore
{
    public int ScoreID { get; set; }
    public int AppointmentID { get; set; }
    public int CalculatedScore { get; set; }
    public DateTime CalculationDate { get; set; } = DateTime.Now;

    public Appointment Appointment { get; set; } = null!;
}
