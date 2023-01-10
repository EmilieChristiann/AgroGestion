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

public class SiteController : Controller
{
    private readonly ApplicationDbContext db;

    public SiteController(ApplicationDbContext db)
    {
        this.db = db;
    }

    // Liste
    /////////////////////////////////////

    public ActionResult Index()
    {
        return View(db.Sites.ToList());
    }

    // Creation
    /////////////////////////////////////
    
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(SiteDTO siteDTO)
    {
        try
        {
            Site site = new Site();
            site.Ville = siteDTO.Ville;             // Ville du modele = ville du formulaire
            db.Sites.Add(site);
            db.SaveChanges();

            return RedirectToAction(nameof(Index)); // Retour sur la liste
        }
        catch
        {
            return View();                          // On reste sur le formulaire
        }
    }

    // Edition
    /////////////////////////////////////

    public ActionResult Edit(int id)
    {
        Site? site = db.Sites.FirstOrDefault(site => site.Id == id);
        if (site == null)
        {
            // Si le site n'existe pas, retour sur la liste                    
            return RedirectToAction(nameof(Index));                
        }

        // On affiche le formulaire
        return View(site);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(SiteDTO siteDTO)
    {
        Site? site = db.Sites.FirstOrDefault(site => site.Id == siteDTO.Id);
        if (site == null)
        {
            // Si le site n'existe pas, retour sur la liste  
            return RedirectToAction(nameof(Index));
        }

        try
        {
            site.Ville = siteDTO.Ville;
            Console.Write(siteDTO.Ville + "\n");
            db.Sites.Update(site);
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
        Site? site = db.Sites.FirstOrDefault(site => site.Id == id);

        if (site != null)                       // Si le site existe
        {                     
            db.Sites.Remove(site);
            db.SaveChanges();
        }

        return RedirectToAction(nameof(Index)); // Retour sur la liste
    }
}