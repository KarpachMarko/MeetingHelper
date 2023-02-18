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
public class PaymentsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<Payment, App.BLL.DTO.Payment> _mapper;

    public PaymentsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new PaymentMapper(mapper);
    }

    [HttpGet("requirement/{requirementId}")]
    public async Task<ActionResult<double>> GetRequirementPayments(Guid requirementId)
    {
        var requirementPayments = await _bll.Payments.GetRequirementPayments(requirementId);
        return Ok(_mapper.Map(requirementPayments));
    }
    
    [HttpGet("event/{eventId}/total")]
    public async Task<ActionResult<double>> GetEventPayments(Guid eventId)
    {
        return await _bll.Payments.GetEventTotalPayments(eventId);
    }

    // GET: api/Payments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
    {
        var payments = await _bll.Payments.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(payments));
    }

    // GET: api/Payments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetPayment(Guid id)
    {
        var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.GetUserId());

        if (payment == null)
        {
            return NotFound();
        }

        return _mapper.Map(payment)!;
    }

    // PUT: api/Payments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPayment(Guid id, Payment payment)
    {
        if (id != payment.Id)
        {
            return BadRequest();
        }

        await _bll.Payments.UpdateAsync(_mapper.Map(payment)!, User.GetUserId());

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await PaymentExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Payments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Payment>> PostPayment(Payment payment)
    {
        payment.Id = Guid.NewGuid();
        payment.UserId = User.GetUserId();
        _bll.Payments.Add(_mapper.Map(payment)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
    }

    // DELETE: api/Payments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        await _bll.Payments.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> PaymentExists(Guid id)
    {
        return _bll.Payments.ExistsAsync(id);
    }
}