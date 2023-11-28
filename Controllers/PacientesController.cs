﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba_brayan_caviedes.Models;

namespace prueba_brayan_caviedes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly PruebaBrayanCaviedesContext _baseDatos;

        public PacientesController(PruebaBrayanCaviedesContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var listaPacientes = await _baseDatos.Pacientes.ToListAsync();
            return Ok(listaPacientes);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Paciente request)
        {
            await _baseDatos.Pacientes.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }


        [HttpPut]
        [Route("Actualizar/{NumeroDocumento}")]
        public async Task<IActionResult> Actualizar(int NumeroDocumento, [FromBody] Paciente request)
        {
            if (NumeroDocumento != request.NumeroDocumento)
            {
                return BadRequest("El NumeroDocumento no coincide con ningun Paciente.");
            }

            // Buscar el Paciente en la base de datos
            var pacienteExistente = await _baseDatos.Pacientes.FindAsync(NumeroDocumento);

            // Si no se encuentra el Paciente, devolver un error 404 (No encontrado)
            if (pacienteExistente == null)
            {
                return NotFound("Paciente no encontrado");
            }

            // Actualizar las propiedades del Paciente existente con los valores del objeto request
            pacienteExistente.TipoDocumento = request.TipoDocumento;
            pacienteExistente.Nombre = request.Nombre;
            pacienteExistente.Apellido = request.Apellido;
            pacienteExistente.CorreoElectronico = request.CorreoElectronico;
            pacienteExistente.Telefono = request.Telefono;
            pacienteExistente.FechaNacimiento = request.FechaNacimiento;
            pacienteExistente.EstadoAfiliacion = request.EstadoAfiliacion;

            // Guardar los cambios en la base de datos
            await _baseDatos.SaveChangesAsync();

            return Ok(pacienteExistente);
        }



        [HttpDelete]
        [Route("Eliminar/{NumeroDocumento:int}")]
        public async Task<IActionResult> Eliminar(int NumeroDocumento)
        {
            var pacientesEliminar = await _baseDatos.Pacientes.FindAsync(NumeroDocumento);
            
            if (pacientesEliminar == null)
            {
                return BadRequest("No existe paciente");
            }
            else
            {
                _baseDatos.Pacientes.Remove(pacientesEliminar);
                await _baseDatos.SaveChangesAsync();
                return Ok();
            }
    }




}
}
