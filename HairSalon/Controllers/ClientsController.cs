using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;

    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

   
    public ActionResult Create (int stylistId)
    {   
        if()                    
        Stylist chosenStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == stylistId);
        ViewBag.StylistName = chosenStylist.Name;
        ViewBag.StylistId = stylistId;      
        
        
        return View();      
    }  
    

    [HttpPost]
    public ActionResult Create(Client client)
    {
      _db.Clients.Add(client);
      _db.SaveChanges();
      Stylist chosenStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == client.StylistId);
      return RedirectToAction("Details", "Stylists", new { id = client.StylistId});
    }
  }
    
}

