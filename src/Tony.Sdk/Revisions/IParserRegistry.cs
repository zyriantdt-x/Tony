namespace Tony.Sdk.Revisions;
public interface IParserRegistry {
    IParser? GetParser( short header );
}
