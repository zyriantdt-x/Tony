using DotNetty.Buffers;
using Tony.Sdk.Encoding;

namespace Tony.Sdk.Clients;
public class ClientMessage {
    public short Header { get; }

    private readonly List<byte> buffer;

    private int read_idx = 0;

    public ClientMessage( byte[] buffer ) {
        this.buffer = new( buffer );
        this.Header = ( short )Base64Encoding.Decode( this.ReadBytes( 2 ) );
    }

    public ClientMessage( short header ) {
        this.buffer = [.. Base64Encoding.Encode( header, 2 )];
        this.Header = header;
    }

    public override string ToString() {
        string str = $"{this.Header} | {System.Text.Encoding.Default.GetString( this.RemainingBytes )}";

        for( int i = 0 ; i < 14 ; i++ ) {
            str = str.Replace( Char.ToString( ( char )i ), "{" + i + "}" );
        }

        return str;
    }

    // might change this
    public void Write( object obj, bool as_object = false ) {
        if( as_object ) {
            this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( obj.ToString()! ) );
            return;
        }

        switch( obj ) {
            case string s:
                this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( s ) );
                this.buffer.Add( 2 );
                break;
            case int i:
                this.buffer.AddRange( VL64Encoding.Encode( i ) );
                break;
            case bool b:
                this.buffer.AddRange( VL64Encoding.Encode( b ? 1 : 0 ) );
                break;
            case object o:
                this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( o.ToString()! ) );
                break;
        }
    }

    public void WriteKeyValue( object key, object value ) {
        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );
        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( ":" ) );
        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
        this.buffer.Add( 13 );
    }

    public void WriteValue( object key, object value ) {
        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );
        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( "=" ) );
        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
        this.buffer.Add( 13 );
    }

    public void WriteDelimiter( object key, object value, string? delim = null ) {
        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );

        if( delim is not null )
            this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( delim ) );

        this.buffer.AddRange( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
    }

    public int ReadInt() {
        byte[] remaining = this.RemainingBytes;

        int length = (remaining[ 0 ] >> 3) & 7;
        int value = VL64Encoding.Decode( remaining );

        this.ReadBytes( length );

        return value;
    }

    public int ReadBase64() {
        return Base64Encoding.Decode( new byte[] {
            this.ReadBytes(1)[0],
            this.ReadBytes(1)[0]
        } );
    }

    public bool ReadBoolean() => this.ReadInt() == 1;

    public string ReadString() {
        int length = this.ReadBase64();
        byte[] data = this.ReadBytes( length );

        return System.Text.Encoding.Default.GetString( data );
    }

    public byte[] ReadBytes( int len ) => this.buffer[ this.read_idx..(this.read_idx += len) ].ToArray();

    public byte[] RemainingBytes => this.buffer[ this.read_idx.. ].ToArray();

    public byte[] Finalise() {
        this.buffer.Add( 1 );
        return this.buffer.ToArray();
    }
}
