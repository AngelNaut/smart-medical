using Microsoft.AspNetCore.Mvc;
using SmartMedical.Application.DTOs.Appointment;
using SmartMedical.Application.Interfaces;

namespace SmartMedical.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAppointment([FromBody] CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var appointment = await _appointmentService.RegisterAppointmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = appointment.AppointmentID }, appointment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status = null, [FromQuery] string? urgency = null)
    {
        var appointments = await _appointmentService.GetAllAppointmentsAsync(status, urgency);
        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null) return NotFound();
        return Ok(appointment);
    }

    [HttpPut("{id}/review-schedule")]
    public async Task<IActionResult> ReviewAndScheduleAppointment(int id, [FromBody] ReviewScheduleAppointmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var appointment = await _appointmentService.ReviewAndScheduleAppointmentAsync(id, dto.ScheduledDateTime);
            return Ok(appointment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _appointmentService.UpdateAppointmentAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        try
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
