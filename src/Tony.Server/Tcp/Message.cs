using DotNetty.Buffers;
using System;
using Tony.Sdk.Clients;
using Tony.Sdk.Encoding;

namespace Tony.Server.Tcp;
internal class Message : IClientMessage {
    public short Header { get; }

    public IByteBuffer Buffer { get; set; }

    public Message( IByteBuffer buf ) {
        this.Buffer = buf;
        this.Header = ( short )Base64Encoding.Decode( [ this.Buffer.ReadByte(), this.Buffer.ReadByte() ] );
    }

    public Message( short header ) {
        this.Buffer = Unpooled.Buffer();
        this.Buffer.WriteBytes( Base64Encoding.Encode( header, 2 ) );
        this.Header = header;
    }

    public override string ToString() {
        string str = $"{this.Header} | {System.Text.Encoding.Default.GetString(this.RemainingBytes)}";

        for( int i = 0 ; i < 14 ; i++ ) {
            str = str.Replace( Char.ToString( ( char )i ), "{" + i + "}" );
        }

        return str;
    }

    // might change this
    public void Write( object obj, bool as_object = false ) {
        if( as_object ) {
            this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( obj.ToString()! ) );
            return;
        }

        switch( obj ) {
            case string s:
                this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( s ) );
                this.Buffer.WriteByte( 2 );
                break;
            case int i:
                this.Buffer.WriteBytes( VL64Encoding.Encode( i ) );
                break;
            case bool b:
                this.Buffer.WriteBytes( VL64Encoding.Encode( b ? 1 : 0 ) );
                break;
            case object o:
                this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( o.ToString()! ) );
                break;
        }
    }

    public void WriteKeyValue( object key, object value ) {
        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );
        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( ":" ) );
        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
        this.Buffer.WriteByte( 13 );
    }

    public void WriteValue( object key, object value ) {
        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );
        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( "=" ) );
        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
        this.Buffer.WriteByte( 13 );
    }

    public void WriteDelimiter( object key, object value, string? delim = null ) {
        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );

        if(delim is not null)
            this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( delim ) );

        this.Buffer.WriteBytes( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
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

    public byte[] ReadBytes( int len ) {
        byte[] payload = new byte[ len ];
        this.Buffer.ReadBytes( payload );

        return payload;
    }

    public byte[] RemainingBytes { 
        get {
            this.Buffer.MarkReaderIndex();

            byte[] bytes = new byte[ this.Buffer.ReadableBytes ];
            this.Buffer.ReadBytes( bytes );

            this.Buffer.ResetReaderIndex();
            return bytes;
        } 
    }
}
