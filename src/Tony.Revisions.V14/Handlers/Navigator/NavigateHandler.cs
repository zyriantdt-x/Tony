using Tony.Revisions.V14.Messages.Naivgator;
using Tony.Revisions.V14.Composers.Navigator;
using Tony.Sdk.Revisions;
using Tony.Sdk.Clients;
using Tony.Sdk.Dto;
using Tony.Sdk.Services;
namespace Tony.Revisions.V14.Handlers.Navigator;
[Header( 150 )]
public class NavigateHandler : IHandler<NavigateMessage> {
    private readonly INavigatorService navigator;
    private readonly IPlayerService player_data;

    public NavigateHandler( INavigatorService navigator, IPlayerService player_data ) {
        this.navigator = navigator;
        this.player_data = player_data;
    }

    public async Task Handle( ITonyClient client, NavigateMessage message ) {
        NavigatorCategoryDto? category = await this.navigator.GetCategory( message.CategoryId );
        IEnumerable<NavigatorCategoryDto> subcategories = await this.navigator.GetCategoriesByParentId( message.CategoryId );
        IEnumerable<NavNodeDto> navnodes = await this.navigator.GetNavNodesByCategoryId( message.CategoryId );

        if( category is null )
            return;

        await client.SendAsync( new NavNodeInfoComposer() {
            ParentCategory = category,
            Subcategories = subcategories.ToList(),
            Rooms = navnodes.ToList(),
            HideFull = false
        } );
    }
}
