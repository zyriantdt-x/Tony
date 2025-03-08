using Tony.Revisions.Composers.Navigator;
using Tony.Revisions.Messages.Naivgator;
using Tony.Revisions.Parsers;
using Tony.Revisions.Services.Player;
using Tony.Revisions.Services.Rooms;
using Tony.Revisions.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Revisions.Handlers.Navigator;
[Header( 150 )]
internal class NavigateHandler : IHandler<NavigateMessage> {
    private readonly NavigatorService navigator;
    private readonly PlayerDataService player_data;

    public NavigateHandler( NavigatorService navigator, PlayerDataService player_data ) {
        this.navigator = navigator;
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, NavigateMessage message ) {
        CategoryDto? category = await this.navigator.GetCategory( message.CategoryId );
        IEnumerable<CategoryDto> subcategories = await this.navigator.GetCategoriesByParentId( message.CategoryId );
        IEnumerable<NavNodeDto> navnodes = await this.navigator.GetNavNodesByCategoryId( message.CategoryId );

        // this will probably be really fucking slow. we will sort this at some point
        List<NavNodeDto> navnodes_with_owner = [];
        foreach( NavNodeDto navnode in navnodes ) {
            navnode.OwnerName = await this.player_data.GetUsernameById( navnode.OwnerId );
            navnodes_with_owner.Add( navnode );
        }

        if( category is null )
            return;

        await client.SendAsync( new NavNodeInfoComposer() {
            ParentCategory = category,
            Subcategories = subcategories.ToList(),
            Rooms = navnodes_with_owner,
            HideFull = false
        } );
    }
}
