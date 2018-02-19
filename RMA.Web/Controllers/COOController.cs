using EZY.RMAS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using EZY.RMAS.BusinessFactory;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Net.Http.Headers;

namespace RMA.Web.Controllers
{
	[WebSsnFilter]
	public class COOController : BaseController
	{

		//public PartialViewResult ADDCOO(string cooNumber = "")
		//{
		//	if (cooNumber == "")
		//	{
		//		ViewBag.Title = "New COO";
		//		return PartialView();
		//	}
		//	else
		//	{
		//		ViewBag.Title = "Edit COO";
		//		//var coo = GetCOOList().Where(x => x.COONumber == cooNumber).FirstOrDefault();
		//		return PartialView();
		//	}
		//}

		public ActionResult COOHeaderList()
		{
			var cooHeaderList = new COOHeaderBO().GetList(BRANCH_ID);
			return View(cooHeaderList);
		}

		public ActionResult COOHeader(string DocumentNo = "")
		{
			var cooHeader = new COOHeader();

			var branchCode = new BranchBO().GetList().Where(x => x.BranchID == BRANCH_ID).Select(x => x).FirstOrDefault();


			var branchprofile = new BranchBO().GetBranch(new Branch { BranchCode = branchCode.BranchCode });


			cooHeader.ExporterName = branchprofile.CompanyCode;
			//cooHeader.ExporterAddress = branchprofile.BranchAddress.Address1 + "\n" + branchprofile.BranchAddress.Address2 + "\n" + branchprofile.BranchAddress.CityName + "\n" + branchprofile.BranchAddress.StateName + "\n" + branchprofile.BranchAddress.ZipCode + "\n" + branchprofile.BranchName;
			cooHeader.ExporterAddress = branchprofile.BranchAddress.FullAddress.Replace(",", "\n");

			if (DocumentNo == "")
			{
				cooHeader.COODetails = new List<COODetail>();
				cooHeader.InvoiceDate = DateTime.UtcNow;
				cooHeader.DepartureDate = DateTime.UtcNow;
				cooHeader.CreatedOn = DateTime.UtcNow;

			}
			else
			{
				cooHeader.BranchID = BRANCH_ID;
				cooHeader.DocumentNo = DocumentNo;
				cooHeader = new COOHeaderBO().GetCOOHeader(cooHeader);
			}


			return View(cooHeader);
		}

		public PartialViewResult ADDCOODetails()
		{
			ViewBag.Title = "New COO Details";
			return PartialView();
		}

		[HttpPost]
		public ActionResult SaveCOODetails(COODetail cooDetails)
		{
			cooDetails.BranchID = BRANCH_ID;
			var result = new COODetailBO().SaveCOODetail(cooDetails);
			return RedirectToAction("COODetailsList", "COO");
		}

		[HttpPost]
		public ActionResult SaveCOOHeader(COOHeader cooHeader)
		{
			cooHeader.BranchID = BRANCH_ID;
			var result = new COOHeaderBO().SaveCOOHeader(cooHeader);
			return RedirectToAction("COOHeaderList", "COO");
		}

		[HttpPost]
		public JsonResult GetCOOHeaderbySearch(COOSearch coo)
		{
			var result = new COOHeaderBO().SearchList(BRANCH_ID, coo.VesselName, coo.ConsigneeName, coo.FromDate, coo.ToDate);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult EditCOOHeader(string DocumentNumber)
		{
			var cooHeader = new COOHeader();
			cooHeader.BranchID = BRANCH_ID;
			cooHeader.DocumentNo = DocumentNumber;
			var cooHeaderDetails = new COOHeaderBO().GetCOOHeader(cooHeader);
			return View("COOHeader", cooHeaderDetails);
		}

		public ActionResult DeleteCOOHeader(string DocumentNumber)
		{
			var cooHeader = new COOHeader();
			cooHeader.BranchID = BRANCH_ID;
			cooHeader.DocumentNo = DocumentNumber;
			var cooHeaderDetails = new COOHeaderBO().DeleteCOOHeader(cooHeader);
			return RedirectToAction("COOHeaderList", "COO");
		}


		public ActionResult GetCOODetails(string DocumentNumber)
		{
			var cooDetail = new COODetail();
			cooDetail.BranchID = BRANCH_ID;
			cooDetail.DocumentNo = DocumentNumber;
			var result = new COODetailBO().GetCOODetail(cooDetail);
			return View(result);
		}



		[HttpPost]
		public JsonResult GetCOOs(DataTableObject Obj)
		{
			var cooList = new COOHeaderBO().GetList(BRANCH_ID);
			var totalRecords = new COOHeaderBO().GetList(BRANCH_ID).Count();

			return Json(new { coos = cooList, totalRecords = totalRecords }, JsonRequestBehavior.AllowGet);
		}


		public FileResult PrintCOOHeaderReport(string branchID, string documentNo)
		{
			try
			{
				var outputPdfStream = new MemoryStream();
				using (Document document = new Document())
				{
					using (PdfSmartCopy copy = new PdfSmartCopy(document, outputPdfStream))
					{
						document.Open();
						AddDataSheets(copy, branchID, documentNo);
					}
				}

				byte[] bytesInStream = outputPdfStream.ToArray(); // simpler way of converting to array
				outputPdfStream.Close();

				Response.Clear();
				Response.ContentType = "application/pdf";
				Response.AddHeader("content-disposition", "attachment;filename=" + documentNo + ".pdf");
				Response.Buffer = true;
				Response.BinaryWrite(bytesInStream);
				Response.End();

				return File(bytesInStream, "application/pdf");
			}
			catch (Exception)
			{
				throw;
			}


		}


		public void AddDataSheets(PdfCopy copy, string branchID, string documentNo)
		{
			COOHeader cooHeader = new COOHeader();
			cooHeader.BranchID = BRANCH_ID;
			cooHeader.DocumentNo = documentNo;
			var cooHeaderItem = new COOHeaderBO().GetCOOHeader(cooHeader);


			var path = "";
			int cooDetailsCount = cooHeaderItem.COODetails.Count();
			int value = 0;
			for (int i = 0; i < cooHeaderItem.COODetails.Count();)
			{

				path = System.Web.Hosting.HostingEnvironment.MapPath("~/COOHeaderTemplate/DOC250118.pdf");

				PdfReader reader = new PdfReader(path);

				value = (cooDetailsCount - i) % 15;

				if (value > 0)
				{
					using (var ms = new MemoryStream())
					{
						using (PdfStamper stamper = new PdfStamper(reader, ms))
						{
							Fill(stamper.AcroFields, cooHeaderItem.COODetails, i, 15, cooHeaderItem);
							stamper.FormFlattening = true;
						}
						reader = new PdfReader(ms.ToArray());
						copy.AddPage(copy.GetImportedPage(reader, 1));
					}
					i = i + 15;
				}
			}
		}

		public static void Fill(AcroFields pdfFormFields, List<COODetail> cOODetails, int dcount, int validcount, COOHeader cooHeader)
		{
			try
			{
				pdfFormFields.SetField("ExporterName", cooHeader.ExporterName + "\n" + cooHeader.ExporterAddress);  //\nExch: " + declarationHeader.CurrencyExRate.ToString("#0.0000")
				pdfFormFields.SetField("ConsigneeName", cooHeader.ConsigneeName + (cooHeader.ConsigneeAddress != null ? "\n" + cooHeader.ConsigneeAddress : ""));

				pdfFormFields.SetField("DepartureDate", cooHeader.DepartureDate.ToShortDateString());
				pdfFormFields.SetField("VesselName", cooHeader.VesselName);
				pdfFormFields.SetField("PortOfDischarge", cooHeader.DestinationPort);
				pdfFormFields.SetField("CountryOfFinalDestination", cooHeader.DestinationCountry);
				pdfFormFields.SetField("CountryOfOriginOfGoods", cooHeader.DestinationPort);
				pdfFormFields.SetField("Name", cooHeader.DeclarantName);
				pdfFormFields.SetField("Designation", cooHeader.Designation);
				pdfFormFields.SetField("Date", cooHeader.CreatedOn.ToShortDateString());
				pdfFormFields.SetField("InvoiceNo", cooHeader.InvoiceNo != null ? cooHeader.InvoiceNo.ToString() : "");
				pdfFormFields.SetField("InvoiceDate", cooHeader.InvoiceDate != null ? cooHeader.InvoiceDate.ToShortDateString() : "");
				pdfFormFields.SetField("MODEL", "MODEL");
				pdfFormFields.SetField("DESCRIPTION", "DESCRIPTION");
				pdfFormFields.SetField("QUANTITY", "QUANTITY");
				pdfFormFields.SetField("ORIGIN", "ORIGIN");

				int count = dcount;
				for (int i = 0; i < validcount; i++)
				{
					if (count < cooHeader.COODetails.Count)
					{
						pdfFormFields.SetField("model" + i + "", cooHeader.COODetails[count].ModelNo.ToString());
						pdfFormFields.SetField("desc" + i + "", cooHeader.COODetails[count].Description.ToString());
						pdfFormFields.SetField("qty" + i + "", cooHeader.COODetails[count].Qty.ToString());
						pdfFormFields.SetField("org" + i + "", cooHeader.COODetails[count].Origin.ToString());
					}
					count++;
				}

			}
			catch (Exception ex)
			{
				throw;
			}

		}

		[HttpPost]
		public JsonResult GetConsigneeNameAddress(string consigneeName)
		{
			var customerList = new CustomerBO().GetList(BRANCH_ID).Where(x => x.CustomerName.Contains(consigneeName)).Select(x => x).FirstOrDefault();
			var customerDetails = new CustomerBO().GetCustomer(new Customer { BranchID = BRANCH_ID, CustomerCode = customerList.CustomerCode });

			return Json(customerDetails, JsonRequestBehavior.AllowGet);
		}


		[HttpGet]
		public JsonResult GetConsigneeNames(string term)
		{
			//Note : you can bind same list from database  
			var customerNameList = new CustomerBO().GetList(BRANCH_ID).Where(x => x.CustomerType == 103).ToList();// .Where(x => x.CustomerName.StartsWith(term)).ToList();
			var customerNames = (from N in customerNameList
								 where N.CustomerName.ToUpper().StartsWith(term.ToUpper())
								 select new { CustomerName = N.CustomerName }).ToList();//customerNameList.Where(x => x.CustomerName.StartsWith(term)).ToList();
			return Json(customerNames, JsonRequestBehavior.AllowGet);
		}

	}

	public class COOSearch
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string VesselName { get; set; }
		public string ConsigneeName { get; set; }

	}

}