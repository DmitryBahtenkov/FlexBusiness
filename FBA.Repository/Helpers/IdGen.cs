using MongoDB.Bson;

namespace FBA.Repository.Helpers
{
    public static class IdGen
    {
        public static string NewId() => ObjectId.GenerateNewId().ToString();
    }
}