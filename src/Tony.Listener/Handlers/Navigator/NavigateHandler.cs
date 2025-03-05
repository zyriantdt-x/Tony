using Tony.Listener.Composers.Navigator;
using Tony.Listener.Messages.Naivgator;
using Tony.Listener.Parsers;
using Tony.Listener.Services.Rooms;
using Tony.Listener.Tcp.Clients;
using Tony.Shared.Dto;

namespace Tony.Listener.Handlers.Navigator;
[Header( 150 )]
internal class NavigateHandler : IHandler<NavigateMessage> {
    private readonly NavigatorService navigator;

    public NavigateHandler( NavigatorService navigator ) {
        this.navigator = navigator;
    }

    public async Task Handle( TonyClient client, NavigateMessage message ) {
        CategoryDto? category = await this.navigator.GetCategory( message.CategoryId );
        IEnumerable<CategoryDto> subcategories = await this.navigator.GetCategoriesByParentId( message.CategoryId );
        IEnumerable<NavNodeDto> navnodes = await this.navigator.GetNavNodesByCategoryId( message.CategoryId );

        await client.SendAsync( new NavNodeInfoComposer() {
            ParentCategory = category,
            Subcategories = subcategories.ToList(),
            Rooms = navnodes.ToList(),
            HideFull = false
        } );
    }
}
