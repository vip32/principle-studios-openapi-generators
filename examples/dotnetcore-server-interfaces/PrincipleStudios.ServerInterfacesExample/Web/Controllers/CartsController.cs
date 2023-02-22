using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrincipleStudios.ServerInterfacesExample.Web.Controllers
{
    public class CartsController : ServerInterfacesExample.Web.Controllers.CartsControllerBase
    {
        protected override Task<AddItemActionResult> AddItem(string identity, CartItemDto addItemBody)
        {
            throw new NotImplementedException();
        }

        protected override Task<DeleteItemActionResult> DeleteItem(string identity, string sku)
        {
            throw new NotImplementedException();
        }

        protected override Task<GetByIdentityActionResult> GetByIdentity(string identity)
        {
            return Task.FromResult(GetByIdentityActionResult.Ok(
                    new ResultOfCartDto(Enumerable.Empty<string>(), true, new CartDto(
                        Guid.NewGuid(),
                        "identity",
                        123,
                        Enumerable.Empty<CartItemDto>()))));
        }
    }
}