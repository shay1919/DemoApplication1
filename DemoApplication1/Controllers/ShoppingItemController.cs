using DemoApplication1.Models;
using DemoApplication1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemController : ControllerBase
    {
        private readonly ShoppingItemService _shoppingItemService;

        public static List<ShoppingItem> shoppingItemList = new List<ShoppingItem>();

        public ShoppingItemController(ShoppingItemService shoppingItemService)
        {
            _shoppingItemService = shoppingItemService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(shoppingItemList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                ShoppingItem item = shoppingItemList.Where(item => item.Id == id).First();
                return Ok(item);
            }
            catch
            {
                return BadRequest($"Shopping item with ID: {id} doesn't exist.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(ShoppingItem item)
        {
            ShoppingItem storedItem = null;
            try
            {
                storedItem = shoppingItemList.Where(thisItem => thisItem.Id == item.Id).First();
            }
            catch
            {
                storedItem = null;
            }
            if (storedItem != null)
            {
                return BadRequest("An item with this ID already exists");
            }
            item.PricePerKg = _shoppingItemService.GetPricePerKg(item);
            shoppingItemList.Add(item);
            return Created(Request.Path.Value, item);
        }
        

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(ShoppingItem item, int id)
        {
            ShoppingItem storedItem = null;
            try
            {
                storedItem = shoppingItemList.Where(thisShoppingItem => thisShoppingItem.Id == id).First();
                storedItem.Name = item.Name;
                storedItem.Weight = item.Weight;
                storedItem.Price = item.Price;
            }
            catch
            {
                storedItem = item;
                shoppingItemList.Add(storedItem);
                return Created(Request.Path.Value, item);
            }
            return Ok(storedItem);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(ShoppingItem item)
        {
            shoppingItemList.Remove(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            shoppingItemList.RemoveAll(item => item.Id == id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult JsonPatchWithModelState([FromBody] JsonPatchDocument<ShoppingItem> patchDoc, int id)
        {
            ShoppingItem storedItem = null;
            try
            {
                storedItem = shoppingItemList.Where(item => item.Id == id).First();
            }
            catch
            {
                return BadRequest(ModelState);
            }
            patchDoc.ApplyTo(storedItem, ModelState);
            return Ok(storedItem);

        }
    }
}
