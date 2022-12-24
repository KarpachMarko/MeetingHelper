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
public class QuestionnairesController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<Questionnaire, App.BLL.DTO.Questionnaire> _mapper;

    public QuestionnairesController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new QuestionnaireMapper(mapper);
    }

    // GET: api/Questionnaires
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Questionnaire>>> GetQuestionnaires()
    {
        var questionnaires = await _bll.Questionnaires.GetAllAsync();
        return Ok(_mapper.Map(questionnaires));
    }

    // GET: api/Questionnaires/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Questionnaire>> GetQuestionnaire(Guid id)
    {
        var questionnaire = await _bll.Questionnaires.FirstOrDefaultAsync(id);

        if (questionnaire == null)
        {
            return NotFound();
        }

        return _mapper.Map(questionnaire)!;
    }

    // PUT: api/Questionnaires/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuestionnaire(Guid id, Questionnaire questionnaire)
    {
        if (id != questionnaire.Id)
        {
            return BadRequest();
        }

        await _bll.Questionnaires.UpdateAsync(_mapper.Map(questionnaire)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await QuestionnaireExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Questionnaires
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Questionnaire>> PostQuestionnaire(Questionnaire questionnaire)
    {
        _bll.Questionnaires.Add(_mapper.Map(questionnaire)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetQuestionnaire", new { id = questionnaire.Id }, questionnaire);
    }

    // DELETE: api/Questionnaires/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestionnaire(Guid id)
    {
        await _bll.Questionnaires.RemoveAsync(id);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> QuestionnaireExists(Guid id)
    {
        return _bll.Questionnaires.ExistsAsync(id);
    }
}