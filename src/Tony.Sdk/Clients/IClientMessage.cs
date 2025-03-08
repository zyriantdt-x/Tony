using DotNetty.Buffers;

namespace Tony.Sdk.Clients;
public interface IClientMessage {
    short Header { get; }
    IByteBuffer Buffer { get; set; }

    void Write( object obj, bool as_object = false );
    void WriteKeyValue( object key, object value );
    void WriteValue( object key, object value );
    void WriteDelimiter( object key, object value, string? delim = null );

    int ReadInt();
    int ReadBase64();
    bool ReadBoolean();
    string ReadString();
    byte[] ReadBytes( int len );
    byte[] RemainingBytes { get; }
}
