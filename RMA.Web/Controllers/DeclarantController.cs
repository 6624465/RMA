
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.RMAS.Contract;
using EZY.RMAS.BusinessFactory;

namespace RMA.Web.Controllers
{
	[WebSsnFilter]
	public class DeclarantController : BaseController
	{
		// GET: Declarant
		public ActionResult Index()
		{
			var branchID = BRANCH_ID;
			var declarantList = new DeclarantBO().GetList(branchID);
			return View("Index", declarantList);
			//return View();
		}


		public PartialViewResult ADDDeclarant(string DeclarantName = "")
		{
			if (DeclarantName == "")
			{
				ViewBag.Title = "New Declarant";
				return PartialView();
			}
			else
			{
				ViewBag.Title = "Edit Declarant";
				var branchID = BRANCH_ID;
				//var declarantList = new DeclarantBO().GetList(branchID).Where(x => x.DeclarantName == DeclarantName).FirstOrDefault();
				var declarant = new DeclarantBO().GetList(branchID).Where(x => x.DeclarantName == DeclarantName).FirstOrDefault();
				return PartialView(new Declarant { DeclarantName = declarant.DeclarantName, Designation = declarant.Designation });
			}
		}

		[HttpPost]
		public ActionResult SaveDeclarant(Declarant declarant)
		{
			declarant.BranchID = BRANCH_ID;
			declarant.IsActive = true;
			var result = new DeclarantBO().SaveDeclarant(declarant);
			TempData["DeclarantSaved"] = result;
			return RedirectToAction("Index");
		}


		[HttpGet]
		public ActionResult DeleteDeclarant(string declarantName)
		{
			var result = new DeclarantBO().DeleteDeclarant(new Declarant { DeclarantName = declarantName, BranchID = BRANCH_ID });

			var declarantList = new DeclarantBO().GetList(BRANCH_ID);
			return View("Index", declarantList);

		}

		public ActionResult GetDeclarantDetails(string declarantName)
		{
			var declarant = new DeclarantBO().GetDeclarant(new Declarant { DeclarantName = declarantName, BranchID = BRANCH_ID });
			return View(declarant);

		}

		[HttpPost]
		public JsonResult GetDeclarantbyName(string declarantName)
		{
			var declarant = new DeclarantBO().GetDeclarant(new Declarant { DeclarantName = declarantName, BranchID = BRANCH_ID });
			return Json(declarant,JsonRequestBehavior.AllowGet);

		}

		[HttpPost]
		public JsonResult GetDeclarants(DataTableObject Obj)
		{
			var declarnats = new DeclarantBO().GetList(BRANCH_ID);
			var totalRecords = new DeclarantBO().GetList(BRANCH_ID).Count();

			return Json(new { declarnats = declarnats, totalRecords = totalRecords }, JsonRequestBehavior.AllowGet);
		}

	}
}