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
    public class CandidateInfoController : ControllerBase
    {
        private readonly CandidateContext _context;

        public CandidateInfoController(CandidateContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateInfo>>> GetCandidates()
        {
            return await _context.Candidates.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateInfo>> GetCandidate(long id)
        {
            var candidateDecision = await _context.Candidates.FindAsync(id);

            if (candidateDecision == null)
            {
                return NotFound();
            }

            return candidateDecision;
        }

        [HttpPost]
        public async Task<ActionResult<CandidateInfo>> PostCandidate(CandidateInfo candidateInfo)
        {
            _context.Candidates.Add(candidateInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidate), new { id = candidateInfo.Id }, candidateInfo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CandidateInfo>> DeleteCandidate(long id)
        {
            var candidateDecision = await _context.Candidates.FindAsync(id);
            if (candidateDecision == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidateDecision);
            await _context.SaveChangesAsync();

            return candidateDecision;
        }

    }
}
