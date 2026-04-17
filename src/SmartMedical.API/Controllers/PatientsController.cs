using Microsoft.AspNetCore.Mvc;
using SmartMedical.Application.DTOs.Patient;
using SmartMedical.Application.Interfaces;

namespace SmartMedical.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var patient = await _patientService.RegisterPatientAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = patient.PatientID }, patient);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _patientService.UpdatePatientAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        try
        {
            await _patientService.DeletePatientAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
