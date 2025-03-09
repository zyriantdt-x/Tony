using Tony.Shared.Dto;
using Tony.Shared.Protos;

namespace Tony.Shared.Mappers;
public static class PlayerDataMapper {
    public static PlayerDto ToDto( this UserObjectResponse player ) => new() {
        Id = player.Id,
        Username = player.Username,
        Credits = player.Credits,
        Figure = player.Figure,
        Sex = player.Sex == "M",
        Mission = player.Mission,
        Tickets = player.Tickets,
        PoolFigure = player.PoolFigure,
        Film = player.Film,
        ReceiveNews = player.ReceiveNews
    };

    public static UserObjectResponse ToProto( this PlayerDto player ) => new() {
        Id = player.Id,
        Username = player.Username,
        Credits = player.Credits,
        Figure = player.Figure,
        Sex = player.Sex ? "M" : "F",
        Mission = player.Mission,
        Tickets = player.Tickets,
        PoolFigure = player.PoolFigure,
        Film = player.Film,
        ReceiveNews = player.ReceiveNews
    };
}
