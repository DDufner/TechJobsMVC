using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        [HttpPost]
        [Route("/Search/Results")]
        public IActionResult Results(string searchType, string searchTerm)
        {
            ViewBag.columns = ListController.columnChoices;
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>(); //takes a list of dictionaries (aka joblist) from JobData.cs
            if (searchType.Equals("all"))
            {
                jobs = JobData.FindByValue(searchTerm);//change to 'find by value' 
                ViewBag.jobs = jobs; //maybe not nessery????? 
                ViewBag.title = "All Jobs";  //sets a viewbag for titles and names it 'all jobs'
                return View("Index"); //displays in view all items matching searchTerm in JobData
            }

            else 
            {
               jobs = JobData.FindByColumnAndValue(searchType, searchTerm); 

                ViewBag.title = "All " +  searchTerm + " Values"; //makes a viewbag of all searchTerms found in column
                ViewBag.jobs = jobs;  //creates viewbag for columns with matching search term. 
                return View("Index"); //displays at view 
                }
            }

            }
            //searches results via the JobData class
            //after searching, pass them into the Views/Search/Index.cshtml view. Note that this is not the default view for this action.
            //You'll also need to pass ListController.columnChoices to the view, as is done in the Index method.
        }

    


