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
public class AnswerOptionsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<AnswerOption, App.BLL.DTO.AnswerOption> _mapper;

    public AnswerOptionsController(IAppBll bll,
        IMapper mapper)
    {
        _bll = bll;
        _mapper = new AnswerOptionMapper(mapper);
    }

    // GET: api/AnswerOptions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnswerOption>>> GetAnswerOptions()
    {
        var answerOptions = await _bll.AnswerOptions.GetAllAsync();
        return Ok(_mapper.Map(answerOptions));
    }

    // GET: api/AnswerOptions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AnswerOption>> GetAnswerOption(Guid id)
    {
        var answerOption = await _bll.AnswerOptions.FirstOrDefaultAsync(id);

        if (answerOption == null)
        {
            return NotFound();
        }

        return _mapper.Map(answerOption)!;
    }

    // PUT: api/AnswerOptions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAnswerOption(Guid id, AnswerOption answerOption)
    {
        if (id != answerOption.Id)
        {
            return BadRequest();
        }

        await _bll.AnswerOptions.UpdateAsync(_mapper.Map(answerOption)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await AnswerOptionExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/AnswerOptions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AnswerOption>> PostAnswerOption(AnswerOption answerOption)
    {
        answerOption.Id = Guid.NewGuid();
        _bll.AnswerOptions.Add(_mapper.Map(answerOption)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetAnswerOption", new { id = answerOption.Id }, answerOption);
    }

    // DELETE: api/AnswerOptions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnswerOption(Guid id)
    {
        await _bll.AnswerOptions.RemoveAsync(id);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> AnswerOptionExists(Guid id)
    {
        return _bll.AnswerOptions.ExistsAsync(id);
    }
}