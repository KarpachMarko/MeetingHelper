using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionnaireRelationsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<QuestionnaireRelation, App.BLL.DTO.QuestionnaireRelation> _mapper;

    public QuestionnaireRelationsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new QuestionnaireRelatrionMapper(mapper);
    }

    // GET: api/QuestionnaireRelations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionnaireRelation>>> GetQuestionnaireRelations()
    {
        var questionnaireRelations = await _bll.QuestionnaireRelations.GetAllAsync();
        return Ok(_mapper.Map(questionnaireRelations));
    }

    // GET: api/QuestionnaireRelations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionnaireRelation>> GetQuestionnaireRelation(Guid id)
    {
        var questionnaireRelation = await _bll.QuestionnaireRelations.FirstOrDefaultAsync(id);

        if (questionnaireRelation == null)
        {
            return NotFound();
        }

        return _mapper.Map(questionnaireRelation)!;
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

        await _bll.QuestionnaireRelations.UpdateAsync(_mapper.Map(questionnaireRelation)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await QuestionnaireRelationExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/QuestionnaireRelations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<QuestionnaireRelation>> PostQuestionnaireRelation(QuestionnaireRelation questionnaireRelation)
    {
        questionnaireRelation.Id = Guid.NewGuid();
        _bll.QuestionnaireRelations.Add(_mapper.Map(questionnaireRelation)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetQuestionnaireRelation", new { id = questionnaireRelation.Id }, questionnaireRelation);
    }

    // DELETE: api/QuestionnaireRelations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestionnaireRelation(Guid id)
    {
        await _bll.QuestionnaireRelations.RemoveAsync(id);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> QuestionnaireRelationExists(Guid id)
    {
        return _bll.QuestionnaireRelations.ExistsAsync(id);
    }
}