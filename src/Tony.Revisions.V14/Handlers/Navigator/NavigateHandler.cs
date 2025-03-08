using Tony.Revisions.V14.ClientMessages.Naivgator;
using Tony.Revisions.V14.Composers.Navigator;
using Tony.Sdk.Revisions;
namespace Tony.Revisions.V14.Handlers.Navigator;
[Header( 150 )]
public class NavigateHandler : IHandler<NavigateClientMessage> {
    private readonly NavigatorService navigator;
    private readonly PlayerDataService player_data;

    public NavigateHandler( NavigatorService navigator, PlayerDataService player_data ) {
        this.navigator = navigator;
        this.player_data = player_data;
    }

    public async Task Handle( TonyClient client, NavigateClientMessage ClientMessage ) {
        CategoryDto? category = await this.navigator.GetCategory( ClientMessage.CategoryId );
        IEnumerable<CategoryDto> subcategories = await this.navigator.GetCategoriesByParentId( ClientMessage.CategoryId );
        IEnumerable<NavNodeDto> navnodes = await this.navigator.GetNavNodesByCategoryId( ClientMessage.CategoryId );

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
