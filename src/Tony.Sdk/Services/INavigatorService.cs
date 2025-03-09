using Tony.Sdk.Dto;

namespace Tony.Sdk.Services;
public interface INavigatorService {
    Task<NavigatorCategoryDto?> GetCategory( int id );
    Task<IEnumerable<NavigatorCategoryDto>> GetCategoriesByParentId( int id );
    Task<IEnumerable<NavNodeDto>> GetNavNodesByCategoryId( int id );
}
