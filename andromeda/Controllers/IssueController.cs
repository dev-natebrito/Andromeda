namespace andromeda.Controllers;

using Data.Models;
using Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IssuesServices _issuesServices;

    public IssuesController(
        IssuesServices issuesServices)
    {
        _issuesServices = issuesServices;
    }

    [HttpGet]
    public async Task<List<Issue>> Get() =>
        await _issuesServices.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Issue>> Get(string id)
    {
        var issue = await _issuesServices.GetAsync(id);

        if (issue is null)
        {
            return NotFound();
        }

        return issue;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Issue newIssue)
    {
        await _issuesServices.CreateAsync(newIssue);

        return CreatedAtAction(nameof(Get), new {id = newIssue.Id}, newIssue);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Issue updatedIssue)
    {
        var issue = await _issuesServices.GetAsync(id);

        if (issue is null)
        {
            return NotFound();
        }

        updatedIssue.Id = issue.Id;

        await _issuesServices.UpdateAsync(id, updatedIssue);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var issue = await _issuesServices.GetAsync(id);

        if (issue is null)
        {
            return NotFound();
        }

        await _issuesServices.RemoveAsync(id);

        return NoContent();
    }
}