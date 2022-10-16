using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class BankAccountsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<BankAccount, App.BLL.DTO.BankAccount> _mapper;

    public BankAccountsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new BankAccountMapper(mapper);
    }

    // GET: api/BankAccounts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccounts()
    {
        var bankAccounts = await _bll.BankAccounts.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(bankAccounts));
    }

    // GET: api/BankAccounts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BankAccount>> GetBankAccount(Guid id)
    {
        var bankAccount = await _bll.BankAccounts.FirstOrDefaultAsync(id, User.GetUserId());

        if (bankAccount == null)
        {
            return NotFound();
        }

        return _mapper.Map(bankAccount)!;
    }

    // PUT: api/BankAccounts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBankAccount(Guid id, BankAccount bankAccount)
    {
        if (id != bankAccount.Id)
        {
            return BadRequest();
        }

        await _bll.BankAccounts.UpdateAsync(_mapper.Map(bankAccount)!, User.GetUserId());

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await BankAccountExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/BankAccounts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
    {
        bankAccount.Id = Guid.NewGuid();
        bankAccount.UserId = User.GetUserId();
        _bll.BankAccounts.Add(_mapper.Map(bankAccount)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetBankAccount", new { id = bankAccount.Id }, bankAccount);
    }

    // DELETE: api/BankAccounts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBankAccount(Guid id)
    {
        await _bll.BankAccounts.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> BankAccountExists(Guid id)
    {
        return _bll.BankAccounts.ExistsAsync(id);
    }
}