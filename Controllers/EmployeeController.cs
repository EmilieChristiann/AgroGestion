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

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext db;

    public EmployeeController(ApplicationDbContext db)
    {
        this.db = db;
    }

    // Liste
    /////////////////////////////////////

    public ActionResult Index()
    {
        List<Employee> employees =
            db.Employees.Include(m => m.Site).Include(m => m.Service).ToList();

        return View(employees);
    }
    
    // Creation
    /////////////////////////////////////

    public ActionResult Create()
    {
        ViewData["Sites"] = db.Sites.ToList();
        ViewData["Services"] = db.Services.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(EmployeeDTO employeeDTO)
    {
        try
        {
            Site? site = db.Sites.FirstOrDefault(site => site.Id == employeeDTO.SiteId);
            Service? service = db.Services.FirstOrDefault(service => service.Id == employeeDTO.ServiceId);

            if (site == null || service == null)
            {                   
                return RedirectToAction(nameof(Index));
            }

            Employee employee = new Employee();
            employee.Name = employeeDTO.Name;            
            employee.Surname = employeeDTO.Surname;            
            employee.Phone = employeeDTO.Phone;
            employee.Email = employeeDTO.Email;
            employee.Site = site;
            employee.Service = service;

            db.Employees.Add(employee);
            db.SaveChanges();

            return RedirectToAction(nameof(Index)); // Retour sur la liste
        }
        catch
        {
            ViewData["Sites"] = db.Sites.ToList();
            ViewData["Services"] = db.Services.ToList();
            return View();                          // On reste sur le formulaire
        }
    }

    // Modifier
    /////////////////////////////////////

    public ActionResult Edit(int id)
    {
        Employee? employee = db.Employees.FirstOrDefault(employee => employee.Id == id);
        if (employee == null)
        {
            // Si le site n'existe pas, retour sur la liste                    
            return RedirectToAction(nameof(Index));
        }

        ViewData["Sites"] = db.Sites.ToList();
        ViewData["Services"] = db.Services.ToList();

        // On affiche le formulaire
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(EmployeeDTO employeeDTO)
    {
        Employee? employee = db.Employees.FirstOrDefault(employee => employee.Id == employeeDTO.Id);
        Site? site = db.Sites.FirstOrDefault(m => m.Id == employeeDTO.SiteId);
        Service? service = db.Services.FirstOrDefault(m => m.Id == employeeDTO.ServiceId);

        if (employee == null || site == null || service == null)
        {                
            return RedirectToAction(nameof(Index));
        }

        try
        {
            employee.Name = employeeDTO.Name;
            employee.Surname = employeeDTO.Surname;
            employee.Phone = employeeDTO.Phone;
            employee.Email = employeeDTO.Email;
            employee.Site = site;
            employee.Service = service;

            db.Employees.Update(employee);
            db.SaveChanges();

            return RedirectToAction(nameof(Index)); // Retour sur la liste
        }
        catch
        {
            ViewData["Sites"] = db.Sites.ToList();
            ViewData["Services"] = db.Services.ToList();
            return View();                          // On reste sur le formulaire
        }
    }

    // Suppression
    /////////////////////////////////////

    public ActionResult Delete(int id)
    {
        Employee? employee = db.Employees.FirstOrDefault(employee => employee.Id == id);

        if (employee != null)                   // Si l'employé existe
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        return RedirectToAction(nameof(Index)); // Retour sur la liste
    }

    // Consulter
    /////////////////////////////////////

    public ActionResult Consult(int id)
    {
        Employee? employee = db.Employees.Include(m => m.Site).Include(m => m.Service).FirstOrDefault(employee => employee.Id == id);
        if (employee == null)
        {
            // Si le site n'existe pas, retour sur la liste                    
            return RedirectToAction(nameof(Index));
        }

        // On affiche le formulaire
        return View(employee);
    }

    // Rechercher
    /////////////////////////////////////

    public ActionResult Search(SearchDTO searchDTO)
    {
        List<Employee> employees = new List<Employee>();

        if (searchDTO.Name != null)             // Recherche par nom
        {
            employees = db.Employees.Where(employee => employee.Name.Contains(searchDTO.Name)).Include(m => m.Site).Include(m => m.Service).ToList();
        }
        else if (searchDTO.SiteId > 0)          // Recherche par site
        {
            employees = db.Employees.Where(employee => employee.SiteId == searchDTO.SiteId).Include(m => m.Site).Include(m => m.Service).ToList();
        }
        else if (searchDTO.ServiceId > 0)       // Recherche par service
        {
            employees = db.Employees.Where(employee => employee.ServiceId == searchDTO.ServiceId).Include(m => m.Site).Include(m => m.Service).ToList();
        }

        List<Site> sites = db.Sites.ToList();
        Site siteNull = new Site();
        sites.Insert(0, siteNull);              // Ajoute un site vide
        ViewData["Sites"] = sites;

        List<Service> services = db.Services.ToList();
        Service serviceNull = new Service();
        services.Insert(0, serviceNull);            
        ViewData["Services"] = services;

        return View(employees);
    }
}