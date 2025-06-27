using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integration.Dtos.Lesson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Forms;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILessonService _lessonService;

        public LessonsController(AppDbContext context, IMapper mapper, ILessonService lessonService)
        {
            _context = context;
            _mapper = mapper;
            _lessonService = lessonService;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IList<LessonBase>>> GetLessons()
        {
            return await _context.Lessons
                .ProjectTo<LessonBase>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonBase>> GetLesson(long id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return _mapper.Map<LessonBase>(lesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(long id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(CreateLessonForm formData)
        {
            // Check if prefab is provided
            if (formData == null)
            {
                return BadRequest("Lesson data is required.");
            }

            if (formData.Prefab == null || formData.Prefab.Length == 0)
            {
                return BadRequest("Prefab file is required.");
            }

            // Check if preview is provided
            if (formData.Preview == null || formData.Preview.Length == 0)
            {
                return BadRequest("Preview file is required.");
            }

            // Validate the lesson data
            if (string.IsNullOrWhiteSpace(formData.Title) || string.IsNullOrWhiteSpace(formData.Description))
            {
                return BadRequest("Title and Description are required.");
            }

            if (formData.Difficulty < 1 || formData.Difficulty > 5)
            {
                return BadRequest("Difficulty must be between 1 and 5.");
            }

            var lesson = _mapper.Map<Lesson>(formData);

            // Save preview file
            lesson.Preview = await _lessonService.SavePreviewFile(formData.Preview);

            // Save prefab file
            lesson.Prefab = new() { 
                Url = await _lessonService.SavePrefabFile(formData.Prefab),
                Name = formData.Prefab.FileName
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesson", new { id = lesson.Id }, formData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(long id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonExists(long id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
