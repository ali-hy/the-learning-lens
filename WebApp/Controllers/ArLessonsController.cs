using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Forms;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArLessonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ArLessonsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ArLessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArLesson>>> GetArLesson()
        {
            return await _context.ArLesson.ToListAsync();
        }

        // GET: api/ArLessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArLesson>> GetArLesson(long id)
        {
            var arLesson = await _context.ArLesson.FindAsync(id);

            if (arLesson == null)
            {
                return NotFound();
            }

            return arLesson;
        }

        // PUT: api/ArLessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArLesson(long id, ArLesson arLesson)
        {
            if (id != arLesson.Id)
            {
                return BadRequest();
            }

            _context.Entry(arLesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArLessonExists(id))
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

        // POST: api/ArLessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArLesson>> PostArLesson([FromForm] CreateArLessonForm formData)
        {
            ArLesson arLesson = _mapper.Map<ArLesson>(formData);

            // Save preview file
            if (formData.Preview != null && formData.Preview.Length > 0)
            {
                var previewFileName = $"{Guid.NewGuid()}_{formData.Preview.FileName}";
                var previewFilePath = Path.Combine("arLesson", "previews", previewFileName);
                using (var stream = new FileStream(previewFilePath, FileMode.Create))
                {
                    await formData.Preview.CopyToAsync(stream);
                }
                arLesson.Preview = previewFileName;
            }

            // Save reference images
            if (formData.ReferenceImages != null && formData.ReferenceImages.Count > 0)
            {
                arLesson.ReferenceImages = new List<ReferenceImage>();
                foreach (var image in formData.ReferenceImages)
                {
                    if (image.Length > 0)
                    {
                        var imageFileName = $"{Guid.NewGuid()}_{image.FileName}";
                        var imageFilePath = Path.Combine("arLesson", "referenceImages", imageFileName);
                        using (var stream = new FileStream(imageFilePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        arLesson.ReferenceImages.Add(new ReferenceImage { Url = imageFileName });
                    }
                }
            }

            _context.ArLesson.Add(arLesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArLesson", new { id = arLesson.Id }, formData);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ArLessonAssessment([FromForm] AssessArLessonForm formData)
        {
            return Ok();
        }

        // DELETE: api/ArLessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArLesson(long id)
        {
            var arLesson = await _context.ArLesson.FindAsync(id);
            if (arLesson == null)
            {
                return NotFound();
            }

            // Delete preview file if exists
            if (!string.IsNullOrEmpty(arLesson.Preview))
            {
                var previewFilePath = Path.Combine("arLesson", "previews", arLesson.Preview);
                if (System.IO.File.Exists(previewFilePath))
                {
                    System.IO.File.Delete(previewFilePath);
                }
            }

            // Delete reference images if exists
            if (arLesson.ReferenceImages != null && arLesson.ReferenceImages.Count > 0)
            {
                foreach (var image in arLesson.ReferenceImages)
                {
                    var imageFilePath = Path.Combine("arLesson", "referenceImages", image.Url);
                    if (System.IO.File.Exists(imageFilePath))
                    {
                        System.IO.File.Delete(imageFilePath);
                    }
                }
            }

            _context.ArLesson.Remove(arLesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArLessonExists(long id)
        {
            return _context.ArLesson.Any(e => e.Id == id);
        }
    }
}
