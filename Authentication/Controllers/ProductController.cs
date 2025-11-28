using Authentication.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers;

public class ProductController : BaseApiController
{
    [Authorize(Policy = "ProductFullAccess")]
    [HttpPost]
    public IActionResult CreateProduct() => Ok("Created");

    [Authorize(Policy = "ProductEditAccess")]
    [HttpPut]
    public IActionResult Update(int id) => Ok("Updated");

    [Authorize(Policy = "ProductReadAccess")]
    [HttpGet]
    public IActionResult GetProducts() => Ok("Products");

}
