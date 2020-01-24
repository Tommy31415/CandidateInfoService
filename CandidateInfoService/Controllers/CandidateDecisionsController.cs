using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandidateInfoService.Models;

namespace CandidateInfoService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateDecisionsController : ControllerBase
    {
        private readonly CandidateContext _context;

        public CandidateDecisionsController(CandidateContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDecision>>> GetDecisionItems()
        {
            return await _context.DecisionItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDecision>> GetCandidateDecision(long id)
        {
            var candidateDecision = await _context.DecisionItems.FindAsync(id);

            if (candidateDecision == null)
            {
                return NotFound();
            }

            return candidateDecision;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidateDecision(long id, CandidateDecision candidateDecision)
        {
            if (id != candidateDecision.Id)
            {
                return BadRequest();
            }

            _context.Entry(candidateDecision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateDecisionExists(id))
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
        public async Task<ActionResult<CandidateDecision>> PostCandidateDecision(CandidateDecision candidateDecision)
        {
            _context.DecisionItems.Add(candidateDecision);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidateDecision), new { id = candidateDecision.Id }, candidateDecision);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CandidateDecision>> DeleteCandidateDecision(long id)
        {
            var candidateDecision = await _context.DecisionItems.FindAsync(id);
            if (candidateDecision == null)
            {
                return NotFound();
            }

            _context.DecisionItems.Remove(candidateDecision);
            await _context.SaveChangesAsync();

            return candidateDecision;
        }

        private bool CandidateDecisionExists(long id)
        {
            return _context.DecisionItems.Any(e => e.Id == id);
        }
    }
}
