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
    public class CandidateInfoControllerTest
    {
        private CandidateInfoController _controller;
        private CandidateContext _dbContext;

        public CandidateInfoControllerTest()
        {
            InitController();
        }

        private void InitController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CandidateContext>();
            optionsBuilder.UseInMemoryDatabase("testDatabase");
            _dbContext = new CandidateContext(optionsBuilder.Options);
            _controller = new CandidateInfoController(_dbContext);
        }

        private async Task<CandidateInfo>  SetupPrimaryCandidate()
        {
            var candidate = new CandidateInfo
            {
                FirstName = "Jack",
                LastName = "Snow",
                IsGoingToHaveOffer = false
            };
            await _controller.PostCandidate(candidate);

            return candidate;
        }

        [Fact]
        public async Task ShouldReturnCorrectNumberOfPostedCandidates()
        {
            _dbContext.Database.EnsureDeleted();
            await SetupPrimaryCandidate();

            var result = await _controller.GetCandidates();

            var actionResult = result.Should().BeOfType<ActionResult<IEnumerable<CandidateInfo>>>().Subject;
            var candidateDecisionsList = actionResult.Value.Should().BeAssignableTo<IEnumerable<CandidateInfo>>().Subject;

            candidateDecisionsList.Count().Should().Be(1);
        }

        [Fact]
        public async Task ShouldGetCandidateById()
        {
            _dbContext.Database.EnsureDeleted();
            var candidate = await SetupPrimaryCandidate();
            var result = await _controller.GetCandidate(candidate.Id);

            var actionResult = result.Should().BeOfType<ActionResult<CandidateInfo>>().Subject;
            actionResult.Value.Id.Should().Be(candidate.Id);
        }
        
        [Fact]
        public async Task ShouldRemoveCandidateFromDatabase()
        {
            _dbContext.Database.EnsureDeleted();
            var candidate = await SetupPrimaryCandidate();
            var result = await _controller.DeleteCandidate(candidate.Id);

            var actionResult = result.Should().BeOfType<ActionResult<CandidateInfo>>().Subject;
            actionResult.Value.Id.Should().Be(candidate.Id);

        }

    }

    
}
