namespace Tony.Sdk.Revisions;
public class HeaderAttribute : Attribute {
    public short Header { get; }
    public HeaderAttribute( short header ) { this.Header = header; }
}
