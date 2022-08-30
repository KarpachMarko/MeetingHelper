using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireRelationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionnaireRelationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionnaireRelations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionnaireRelation>>> GetQuestionnaireRelations()
        {
          if (_context.QuestionnaireRelations == null)
          {
              return NotFound();
          }
            return await _context.QuestionnaireRelations.ToListAsync();
        }

        // GET: api/QuestionnaireRelations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionnaireRelation>> GetQuestionnaireRelation(Guid id)
        {
          if (_context.QuestionnaireRelations == null)
          {
              return NotFound();
          }
            var questionnaireRelation = await _context.QuestionnaireRelations.FindAsync(id);

            if (questionnaireRelation == null)
            {
                return NotFound();
            }

            return questionnaireRelation;
        }

        // PUT: api/QuestionnaireRelations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionnaireRelation(Guid id, QuestionnaireRelation questionnaireRelation)
        {
            if (id != questionnaireRelation.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionnaireRelation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionnaireRelationExists(id))
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

        // POST: api/QuestionnaireRelations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionnaireRelation>> PostQuestionnaireRelation(QuestionnaireRelation questionnaireRelation)
        {
          if (_context.QuestionnaireRelations == null)
          {
              return Problem("Entity set 'AppDbContext.QuestionnaireRelations'  is null.");
          }
            _context.QuestionnaireRelations.Add(questionnaireRelation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionnaireRelation", new { id = questionnaireRelation.Id }, questionnaireRelation);
        }

        // DELETE: api/QuestionnaireRelations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionnaireRelation(Guid id)
        {
            if (_context.QuestionnaireRelations == null)
            {
                return NotFound();
            }
            var questionnaireRelation = await _context.QuestionnaireRelations.FindAsync(id);
            if (questionnaireRelation == null)
            {
                return NotFound();
            }

            _context.QuestionnaireRelations.Remove(questionnaireRelation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionnaireRelationExists(Guid id)
        {
            return (_context.QuestionnaireRelations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
