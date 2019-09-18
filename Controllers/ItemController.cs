using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using storeinventory.Models;

namespace storeinventory.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
  {
    [HttpPost]
    public ActionResult<Item> CreateEntry([FromBody]Item entry)
    {
      var db = new DatabaseContext();
      db.Items.Add(entry);
      db.SaveChanges();
      return entry;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Item>> GetAllItems()
    {
      var db = new DatabaseContext();
      var items = db.Items.OrderByDescending(item => item.DateOrdered);
      return items.ToList();
    }

    [HttpGet("{id}")]

    public ActionResult<Item> GetOneItem(int id)
    {
      var db = new DatabaseContext();
      var item = db.Items.FirstOrDefault(i => i.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

    [HttpGet("item/{sku}")]
    public ActionResult<Item> GetBySku(int sku)
    {
      var db = new DatabaseContext();
      var item = db.Items.FirstOrDefault(i => i.Sku == sku);
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

    [HttpGet("items/out")]
    public ActionResult<IEnumerable<Item>> NotInStock()
    {
      var db = new DatabaseContext();
      var items = db.Items.Where(item => item.NumberInStock == 0).OrderByDescending(item => item.DateOrdered);
      return items.ToList();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(int id)
    {
      var db = new DatabaseContext();
      var items = db.Items.FirstOrDefault(item => item.Id == id);
      db.Items.Remove(items);
      db.SaveChanges();
      return Ok();
    }

    [HttpPut("{id}")]

    public ActionResult<Item> UpdateEntry([FromBody]Item entry)
    {
      var db = new DatabaseContext();
      var itemToUpdate = db.Items.FirstOrDefault(item => item.Id == id);
      db.Items.Update(itemToUpdate);
      db.SaveChanges();
      return itemToUpdate;
    }
  }
}