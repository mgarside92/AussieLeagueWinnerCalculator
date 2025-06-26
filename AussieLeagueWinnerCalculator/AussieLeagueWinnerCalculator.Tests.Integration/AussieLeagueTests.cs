using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using AussieLeagueWinnerCalculator.Application.Models;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;

namespace AussieLeagueWinnerCalculator.Tests.Integration;

public sealed class AussieLeagueTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly Fixture _fixture;
    private readonly WebApplicationFactory<Program> _factory;

    public AussieLeagueTests()
    {
        _fixture = new Fixture();
        _factory = new WebApplicationFactory<Program>();
    }
    
    [Theory]
    [MemberData(nameof(ValidRequests))]
    public async Task GetLeagueWinnerAndRunnerUp_GivenValidRequests_ShouldReturnOkResponse(GetLeagueWinnerAndRunnerUpRequest request, string[] expectedResponse)
    {
        //Arrange
        var jsonRequest = GenerateJsonRequest(request);
        var client = _factory.CreateClient();
        
        //Act
        var response = await client.PostAsync("/api/GetLeagueWinnerAndRunnerUp", jsonRequest);

        //Assert
        var winnerAndRunnerUpResponse = await response.Content.ReadFromJsonAsync<string[]>();
        
        response.EnsureSuccessStatusCode();
        winnerAndRunnerUpResponse.ShouldNotBeNull();
        winnerAndRunnerUpResponse.Length.ShouldBe(expectedResponse.Length);
        winnerAndRunnerUpResponse.ShouldBe(expectedResponse);
    }

    [Fact]
    public async Task GetLeagueWinnerAndRunnerUp_GivenInvalidRequests_ShouldReturnBadRequest()
    {
        //Arrange
        var malformedRequest = _fixture.Build<GetLeagueWinnerAndRunnerUpRequest>();
        var jsonRequest = GenerateJsonRequest(malformedRequest);
        var client = _factory.CreateClient();

        //Act
        var response = await client.PostAsync("/api/GetLeagueWinnerAndRunnerUp", jsonRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    private static StringContent GenerateJsonRequest(object request)
        => new (JsonSerializer.Serialize(request), MediaTypeHeaderValue.Parse(MediaTypeNames.Application.Json));
    
    public static IEnumerable<object[]> ValidRequests()
    {
        yield return
        [
            new GetLeagueWinnerAndRunnerUpRequest(["a:teamA", "b:teamB"], ["a:b", "b:a"], ["52:32", "22:56"]),
            new[] { "a", "b" }
        ];
        yield return
        [
            new GetLeagueWinnerAndRunnerUpRequest(["a:teamA", "b:teamB", "c:teamC"], ["a:b", "b:a", "c:a", "c:b", "a:c", "b:c"], ["52:32", "22:56", "52:32", "22:56", "52:32", "22:56"]),
            new[] { "a", "c" }
        ];
    }
}