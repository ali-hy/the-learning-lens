using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Models.Archive;
using Integration.Dtos.Archived.Module;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController(AppDbContext context, IMapper mapper, ILogger<ModulesController> logger) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ModulesController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModules()
        {
            return await _context.Modules.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(long id)
        {
            var @module = await _context.Modules.FindAsync(id);

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(long id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            _context.Entry(@module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostModule(CreateModuleRequest request)
        {
            var @module = _mapper.Map<Module>(request);
            
            if (ContextItem.User(HttpContext) is UserAccount user)
                module.CreatedById = user.Id;
            else
            {
                _logger.LogError("[ModulesController] Expected to have user in HttpContext");
                return Unauthorized();
            }

            _context.Modules.Add(@module);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(long id)
        {
            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(long id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }
    }
}
