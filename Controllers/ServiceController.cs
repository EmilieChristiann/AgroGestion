using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroGestion.Datas;
using AgroGestion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgroGestion.Controllers;

public class ServiceController : Controller
{
    private readonly ApplicationDbContext db;

    public ServiceController(ApplicationDbContext db)
    {
        this.db = db;
    }

    // Liste
    /////////////////////////////////////

    public ActionResult Index()
    {
        return View(db.Services.ToList());
    }

    // Creation
    /////////////////////////////////////

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ServiceDTO serviceDTO)
    {
        try
        {
            Service service = new Service();
            service.Type = serviceDTO.Type;         // Service du modele = service du formulaire
            db.Services.Add(service);
            db.SaveChanges();

            return RedirectToAction(nameof(Index)); // Retour sur la liste
        }
        catch
        {
            return View();                          // On reste sur le formulaire
        }
    }

    // Modifier
    /////////////////////////////////////

    public ActionResult Edit(int id)
    {
        Service? service = db.Services.FirstOrDefault(service => service.Id == id);
        if (service == null)
        {
            // Si le service n'existe pas, retour sur la liste                    
            return RedirectToAction(nameof(Index));
        }

        // On affiche le formulaire
        return View(service);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(ServiceDTO serviceDTO)
    {
        Service? service = db.Services.FirstOrDefault(service => service.Id == serviceDTO.Id);
        if (service == null)
        {
            // Si le service n'existe pas, retour sur la liste  
            return RedirectToAction(nameof(Index));
        }

        try
        {
            service.Type = serviceDTO.Type;
            db.Services.Update(service);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // Suppression
    /////////////////////////////////////

    public ActionResult Delete(int id)
    {
        Service? service = db.Services.FirstOrDefault(service => service.Id == id);

        if (service != null)                    // Si le service existe
        {
            db.Services.Remove(service);
            db.SaveChanges();
        }

        return RedirectToAction(nameof(Index)); // Retour sur la liste
    }
}