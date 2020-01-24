using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateInfoService.Controllers;
using CandidateInfoService.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CandidateInfoServiceTest
{
    public class CandidateDecisionsControllerTest
    {
        [Fact]
        public async Task ShouldReturnCorrectNumberOfPostedCandidates()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CandidateContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            var dbContext = new CandidateContext(optionsBuilder.Options);

            var controller = new CandidateDecisionsController(dbContext);
            var decision = new CandidateDecision
            {
                FirstName = "Jack", Id = 0, LastName = "Snow", IsGoingToHaveOffer = true
            };
            await controller.PostCandidateDecision(decision);

            var result = await controller.GetDecisionItems();

            var actionResult = result.Should().BeOfType<ActionResult<IEnumerable<CandidateDecision>>>().Subject;
            var candidateDecisionsList = actionResult.Value.Should().BeAssignableTo<IEnumerable<CandidateDecision>>().Subject;

            candidateDecisionsList.Count().Should().Be(1);
        }
    }

    
}
