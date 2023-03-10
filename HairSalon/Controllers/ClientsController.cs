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

    public ActionResult Index()
    {
      List<Client> model = _db.Clients.Include(client => client.Stylist).ToList();
      return View(model);
    }
   
    public ActionResult Create (int stylistId)
    {   
        if(stylistId != 0)
        {                    
        Stylist chosenStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == stylistId);
        ViewBag.StylistName = chosenStylist.Name;
        ViewBag.StylistIdSpecific = stylistId; 
        }
        ViewBag.Stylists =  _db.Stylists.ToList();
        ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");

        
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

