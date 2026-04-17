namespace SmartMedical.Application.DTOs.Appointment;

public class UpdateAppointmentDto
{
    public DateTime DateTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public string UrgencyDescription { get; set; } = string.Empty;
}
