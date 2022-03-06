using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace andromeda.Data.Models;

public class AndromedaDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string AndromedaCollectionName { get; set; } = null!;
}
[BsonIgnoreExtraElements]
public class Issue
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("BugName")]
    public string BugName { get; set; } = null!;

    public string ReporterName { get; set; } = null!;
    public DateTime SubmitDate { get; set; }
    public string? ReporterEmail { get; set; }
    public string Summary { get; set; } = null!;
}