using System;
using Tony.Listener.Encoding;

namespace Tony.Listener.Tcp;
internal class Message {
    public short Header { get; }
    public List<byte> Body { get; }
    public int Index { get; set; } = 0;

    public Message( byte[] buffer ) {
        this.Body = new List<byte>();

        int idx = 0;
        int length = Base64Encoding.Decode( [ buffer[ idx++ ], buffer[ idx++ ], buffer[ idx++ ] ] ); // idk if we care about this?
        this.Header = ( short )Base64Encoding.Decode( [ buffer[ idx++ ], buffer[ idx++ ] ] );

        this.Body.AddRange( buffer[ idx..(idx + length - 2)] );
    }

    public Message( short header ) {
        this.Header = header;
        this.Body = new();
    }

    public override string ToString() {
        string str = $"{System.Text.Encoding.Default.GetString( Base64Encoding.Encode( this.Header, 2 ) )} | {System.Text.Encoding.Default.GetString(this.Body.ToArray())}";

        for( int i = 0 ; i < 14 ; i++ ) {
            str = str.Replace( Char.ToString( ( char )i ), "{" + i + "}" );
        }

        return str;
    }

    // might change this
    public void Write(object obj) {
        switch( obj ) {
            case string s:
                this.Body.AddRange( System.Text.Encoding.Default.GetBytes( s ) );
                this.Body.Add( 2 );
                break;
            case int i:
                this.Body.AddRange( VL64Encoding.Encode( i ) );
                break;
            case bool b:
                this.Body.AddRange( VL64Encoding.Encode( b ? 1 : 0 ) );
                break;
            case object o:
                this.Body.AddRange( System.Text.Encoding.Default.GetBytes( o.ToString()! ) );
                break;
        }
    }

    public void WriteKeyValue( object key, object value ) {
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( ":" ) );
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
        this.Body.Add( 13 );
    }

    public void WriteValue( object key, object value ) {
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( "=" ) );
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
        this.Body.Add( 13 );
    }

    public void WriteDelimiter( object key, object value ) {
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( key.ToString()! ) );
        this.Body.AddRange( System.Text.Encoding.Default.GetBytes( value.ToString()! ) );
    }

    public int ReadInt() {
        List<byte> remaining = this.RemainingBytes;

        int length = (remaining[ 0 ] >> 3) & 7;
        int value = VL64Encoding.Decode( remaining.ToArray() );
        this.Index += length;

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
        byte[] payload = this.Body[ this.Index..(this.Index + len) ].ToArray();
        this.Index += len;

        return payload;
    }

    public List<byte> RemainingBytes => this.Body[ this.Index.. ];
}
