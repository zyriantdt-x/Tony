using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Composers.Player;
public class UserObjectComposer : ComposerBase {
    public override short Header => 5;

    public int Id { get; init; }
    public string Username { get; init; }
    public string Figure { get; init; }
    public string Sex { get; init; } // 1 male 0 female
    public string Mission { get; init; }
    public int Tickets { get; init; }
    public string PoolFigure { get; init; }
    public int Film { get; init; }
    public bool ReceiveNews { get; init; }

    public UserObjectComposer( PlayerDto dto ) {
        this.Id = dto.Id;
        this.Username = dto.Username;
        this.Figure = dto.Figure;
        this.Sex = dto.Sex ? "M" : "F";
        this.Mission = dto.Mission;
        this.Tickets = dto.Tickets;
        this.PoolFigure = dto.PoolFigure;
        this.ReceiveNews = dto.ReceiveNews;
    }

    public override Message Compose() {
        Message msg = base.Compose();
        msg.Write( this.Id.ToString()! );
        msg.Write( this.Username );
        msg.Write( this.Figure );
        msg.Write( this.Sex );
        msg.Write( this.Mission );
        msg.Write( this.Tickets );
        msg.Write( this.PoolFigure );
        msg.Write( this.Film );
        msg.Write( this.ReceiveNews );
        return msg;
    }
}
