namespace Tracer.Library.Services.Serialization
{
    public interface ISerializer
    {
        string Serialize(Entity.TraceResult result);
    }
}
