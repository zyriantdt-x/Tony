using Microsoft.EntityFrameworkCore;
using Tony.Sdk.Dto;
using Tony.Sdk.Enums;
using Tony.Server.Storage;
using Tony.Server.Storage.Entities;

namespace Tony.Server.Repositories;
internal class RoomDataRepository {
    private readonly IDbContextFactory<TonyStorage> storage;

    public RoomDataRepository( IDbContextFactory<TonyStorage> storage_factory ) {
        this.storage = storage_factory;
    }

    public async Task<RoomDataDto?> GetRoomDataById( int id ) {
        using TonyStorage storage_ctx = await this.storage.CreateDbContextAsync();
        RoomData? room = await storage_ctx.RoomData
            .Include( r => r.Owner )
            .Include( r => r.Model )
            .FirstOrDefaultAsync( r => r.Id == id );
        if( room is null )
            return null;

        return this.MapRoomToDto( room );
    }

    private RoomDataDto MapRoomToDto( RoomData room )
        => new() {
            Id = room.Id,
            Name = room.Name,
            Description = room.Description,
            VisitorsMax = room.VisitorsMax,
            VisitorsNow = room.VisitorsNow,
            Category = room.Category,
            Ccts = room.Ccts,
            OwnerId = room.OwnerId,
            OwnerName = room.Owner.Username,
            AccessType = ( AccessType )room.AccessType,
            ModelId = room.ModelId,
            Model = this.MapModelToDto( room.Model )
        };

    private RoomModelDto MapModelToDto( RoomModel model )
        => new() {
            Id = model.Id,
            DoorX = model.DoorX,
            DoorY = model.DoorY,
            DoorZ = model.DoorZ,
            DoorDir = model.DoorDir,
            Heightmap = model.Heightmap,
            TriggerClass = model.TriggerClass,
        };
}
