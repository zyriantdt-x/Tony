namespace Tony.Sdk.Revisions;
public interface IHandlerRegistry {
    IHandler? GetHandler( short header );
}
