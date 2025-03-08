namespace Tony.Revisions.Parsers;
internal class HeaderAttribute : Attribute {
    public short Header { get; }
    public HeaderAttribute( short header ) { this.Header = header; }
}
