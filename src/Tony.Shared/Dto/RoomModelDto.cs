using System.Text;

namespace Tony.Shared.Dto;
public class RoomModelDto {
    public string Id { get; set; } = "noid";
    public int DoorX { get; set; }
    public int DoorY { get; set; }
    public int DoorZ { get; set; }
    public int DoorDir { get; set; }
    public string Heightmap { get; set; } = "nohmap";
    public string TriggerClass { get; set; } = "notriggerclass";

    public string ParseHeightmap() {
        string[] lines = this.Heightmap.Split( '|' );

        int sizeX = lines[ 0 ].Length;
        int sizeY = lines.Length;

        StringBuilder heightmap = new StringBuilder();
        for( int y = 0 ; y < sizeY ; y++ ) {
            string line = lines[ y ];

            for( int x = 0 ; x < sizeX ; x++ ) {
                string tile = line[ x ].ToString();

                // maybe we need this down the line for more htings?

                heightmap.Append( tile );
            }

            heightmap.Append( '\r' );
        }

        return heightmap.ToString();
    }
}
