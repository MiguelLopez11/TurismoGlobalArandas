using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<FilesController> _logger;
        private readonly TurismoGlobalContext _context;

        public FilesController(
            IHostEnvironment env,
            ILogger<FilesController> logger,
            TurismoGlobalContext context
        )
        {
            _context = context;
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{PaymentProviderId}")]
        public async Task<ActionResult<Files>> getCustomers(int PaymentProviderId)
        {
            var files = await _context.Files
                .Include(i => i.PaymentProviders)
                .Where(w => w.PaymentProviderId == PaymentProviderId)
                .ToListAsync();
            return Ok(files);
        }


        [HttpGet("Download/{FileId}")]
        public async Task<ActionResult> DownloadFile(int FileId)
        {
            try
            {
                var file = await _context.Files
                    .Include(i => i.PaymentProviders)
                    .FirstOrDefaultAsync(f => f.FileId == FileId);
                if (file == null)
                {
                    return BadRequest();
                }
                var path = Path.Combine(_env.ContentRootPath, "Resource/Files", file.GuidName);

                if (System.IO.File.Exists(path))
                {
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        stream.CopyTo(memory);
                    }

                    memory.Position = 0;
                    return File(memory, "application/octet-stream", file.Name);
                }
                else
                {
                    _logger.LogWarning($"Archivo no encontrado: {file.GuidName}");
                    return NotFound("Archivo no encontrado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al descargar archivo: {FileId}");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error interno del servidor"
                );
            }
        }


        [HttpPost("{PaymentProviderId}")]
        public async Task<IActionResult> CargarArchivo(List<IFormFile> files, int PaymentProviderId)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No se proporcionaron archivos");
            }
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "Resource/Files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var extensionesPermitidas = new[] { ".xlsx", ".xls", ".docx", ".doc", ".pdf" };
                foreach (var file in files)
                {
                    if (!extensionesPermitidas.Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        _logger.LogWarning($"Archivo no permitido: {file.FileName}");
                        continue;
                    }
                    var GuidName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    var Name = Path.GetFileName(file.FileName);
                    var rutaCompleta = Path.Combine(path, GuidName);
                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var newFile = new Files
                    {
                        GuidName = GuidName,
                        Name = Name,
                        Type = GetTypeByMimeType(file.ContentType),
                        UploadDate = DateTime.Today,
                        PaymentProviderId = PaymentProviderId
                    };
                    _context.Files.Add(newFile);
                    await _context.SaveChangesAsync();
                }
                return Ok("Archivos cargados exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{FileId}")]
        public async Task<ActionResult> UpdateFile(int FileId, IFormFile File)
        {
            try
            {
                var file = await _context.Files
                .Include(i => i.PaymentProviders)
                .FirstOrDefaultAsync(f => f.FileId == FileId);
                if (file == null)
                {
                    return BadRequest();
                }
                var path = Path.Combine(_env.ContentRootPath, "Resource/Files", file.GuidName);

                if (System.IO.File.Exists(path))
                {
                    var GuidName =
                        Guid.NewGuid().ToString() + "_" + Path.GetFileName(File.FileName);
                    var Name = Path.GetFileName(File.FileName);
                    var rutaCompletaNuevo = Path.Combine(
                        _env.ContentRootPath,
                        "Resource/Files",
                        GuidName
                    );
                    using (var stream = new FileStream(rutaCompletaNuevo, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }
                    file.Name = Name;
                    file.GuidName = GuidName;
                    _context.Files.Update(file);
                    await _context.SaveChangesAsync();
                    System.IO.File.Delete(path);
                    _logger.LogInformation(
                        $"Archivo actualizado y renombrado a: {Name}"
                    );

                    return Ok($"Archivo actualizado y renombrado a: {Name}");
                }
                else
                {
                    _logger.LogWarning($"Archivo no encontrado para actualizar: {FileId}");
                    return NotFound("Archivo no encontrado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar archivo: {FileId}");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error interno del servidor"
                );
            }
        }
        [HttpDelete("{FileId}")]
        public async Task<ActionResult> DeleteFile(int FileId)
        {
            try
            {
                var file = await _context.Files
                    .Include(i => i.PaymentProviders)
                    .FirstOrDefaultAsync(f => f.FileId == FileId);
                var path = Path.Combine(_env.ContentRootPath, "Resource/Files", file.GuidName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    _context.Remove(file);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Archivo borrado: {file.Name}");
                    return Ok("Archivo borrado exitosamente");
                }
                else
                {
                    _logger.LogWarning($"Archivo no encontrado para borrar: {file.Name}");
                    return NotFound("Archivo no encontrado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al borrar archivo con el ID: {FileId}");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error interno del servidor"
                );
            }
        }
        private string GetTypeByMimeType(string mimeType)
        {
            // Este método realiza un mapeo simple de representaciones MIME a extensiones de archivo comunes.
            // Puedes extender este mapeo según tus necesidades específicas.

            switch (mimeType)
            {
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return "docx";
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return "xlsx";
                case "application/pdf":
                    return "pdf";
                // Agrega más casos según sea necesario...
                default:
                    return string.Empty; // En caso de no encontrar una correspondencia
            }
        }
    }
}
