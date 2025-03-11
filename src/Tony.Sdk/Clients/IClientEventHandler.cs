namespace Tony.Sdk.Clients;
public interface IClientEventHandler {
    Task OnClientRegistered( ITonyClient client );
    Task OnClientDeregistered( ITonyClient client );
}
