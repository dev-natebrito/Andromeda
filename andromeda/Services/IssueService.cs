using andromeda.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace andromeda.Services;

public class IssuesServices
{
    private readonly IMongoCollection<Issue> _issuesCollection;

    public IssuesServices(
        IOptions<AndromedaDatabaseSettings> andromedaDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            andromedaDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            andromedaDatabaseSettings.Value.DatabaseName);

        _issuesCollection = mongoDatabase.GetCollection<Issue>(
            andromedaDatabaseSettings.Value.AndromedaCollectionName);
    }

    public async Task<List<Issue>> GetAsync() =>
        await _issuesCollection.Find(_ => true).ToListAsync();

    public async Task<Issue?> GetAsync(string id) =>
        await _issuesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Issue newIssue) =>
        await _issuesCollection.InsertOneAsync(newIssue);

    public async Task UpdateAsync(string id, Issue updatedIssue) =>
        await _issuesCollection.ReplaceOneAsync(x => x.Id == id, updatedIssue);

    public async Task RemoveAsync(string id) =>
        await _issuesCollection.DeleteOneAsync(x => x.Id == id);
}