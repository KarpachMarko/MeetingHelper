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
public class MoneyTransfersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<MoneyTransfer, App.BLL.DTO.MoneyTransfer> _mapper;

    public MoneyTransfersController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new MoneyTransferMapper(mapper);
    }

    // GET: api/MoneyTransfers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MoneyTransfer>>> GetMoneyTransfers()
    {
        var moneyTransfers = await _bll.MoneyTransfers.GetAllAsync();
        return Ok(_mapper.Map(moneyTransfers));
    }

    // GET: api/MoneyTransfers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MoneyTransfer>> GetMoneyTransfer(Guid id)
    {
        var moneyTransfer = await _bll.MoneyTransfers.FirstOrDefaultAsync(id);

        if (moneyTransfer == null)
        {
            return NotFound();
        }

        return _mapper.Map(moneyTransfer)!;
    }

    // PUT: api/MoneyTransfers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMoneyTransfer(Guid id, MoneyTransfer moneyTransfer)
    {
        if (id != moneyTransfer.Id)
        {
            return BadRequest();
        }

        await _bll.MoneyTransfers.UpdateAsync(_mapper.Map(moneyTransfer)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await MoneyTransferExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/MoneyTransfers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MoneyTransfer>> PostMoneyTransfer(MoneyTransfer moneyTransfer)
    {
        moneyTransfer.Id = Guid.NewGuid();
        _bll.MoneyTransfers.Add(_mapper.Map(moneyTransfer)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetMoneyTransfer", new { id = moneyTransfer.Id }, moneyTransfer);
    }

    // DELETE: api/MoneyTransfers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMoneyTransfer(Guid id)
    {
        await _bll.MoneyTransfers.RemoveAsync(id);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> MoneyTransferExists(Guid id)
    {
        return _bll.MoneyTransfers.ExistsAsync(id);
    }
}