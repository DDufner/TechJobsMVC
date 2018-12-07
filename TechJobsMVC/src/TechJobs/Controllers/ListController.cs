
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class ListController : Controller  //creates 'ListController' class 
    {
        internal static Dictionary<string, string> columnChoices = new Dictionary<string, string>(); //allows for static fields to be initialized without having to call a regular constructor

        // This is a "static constructor" which can be used
        // to initialize static members of a class
        static ListController() 
        {
            
            columnChoices.Add("core competency", "Skill"); //adds info to an instance of ListController, info is keyvalue pairs 
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");
        }

        public IActionResult Index() //index controller, shows column choices in ViewBag
        {
            ViewBag.columns = columnChoices;
            return View();
        }

        public IActionResult Values(string column)  //values controller, shows values by column, which on web page are 'skill, employer, location, etc'
        {
            if (column.Equals("all")) //if selected column is all....
            {
                List<Dictionary<string, string>> jobs = JobData.FindAll(); //takes a list of dictionaries (aka jobs) 
                //mising 'foreach'?
                ViewBag.title =  "All Jobs";  //sets a viewbag for titles and names it 'all jobs'
                ViewBag.jobs = jobs; //sets a viewbag for jobs and names it 'jobs' 
                return View("Jobs"); //displays in 'jobs' view 
            }
            else //if selected column (aka 'skill, employer') is not all...
            {
                List<string> items = JobData.FindAll(column);  //makes a list of all items per column 
                ViewBag.title =  "All " + columnChoices[column] + " Values"; //makes a viewbag of 'all' and column chosen by column key, and adds values
                ViewBag.column = column;  //sets a viewback for column chosen
                ViewBag.items = items;  //sets a viewbag for the list of items
                return View();  //displays at 'view' 
            }
        }

        public IActionResult Jobs(string column, string value)
        {
            List<Dictionary<String, String>> jobs = JobData.FindByColumnAndValue(column, value); //makes a list of dictionaries (aka jobs) that 
            //match the chosen columan and value
            ViewBag.title = "Jobs with " + columnChoices[column] + ": " + value;  //creates a viewbag for jobs matching column and value choices 
            ViewBag.jobs = jobs;  //creates a viewbag for jobs, which is a list of dictionaries 

            return View();  //displays results at 'view' 
        }
    }
}
