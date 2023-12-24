using System;
using System.Collections.Generic;

namespace prueba_brayan_caviedes.Models;

public partial class Paciente
{
    public int Id { get; set; }

    public int NumeroDocumento { get; set; }

    public string? TipoDocumento { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? CorreoElectronico { get; set; }

    public long? Telefono { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? EstadoAfiliacion { get; set; }
}
