using FileManagementSystem.Models;
using FileManagementSystem.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FileManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult WorkOrders()
        {
            try
            {
                ViewBag.Title = "WorkOrders";
                ViewData["WoList"] = new WorkOrderViewModel();
                ViewData["BoxList"] = new BoxesViewModel();
                ViewData["FileList"] = new FileViewModel();
                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Data Entry")]
        public RedirectToRouteResult FilterWorkOrderSummary(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                return RedirectToAction("WorkOrderSummary", new { From = FromDate, To = ToDate });

            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize(Roles = "Admin, Data Entry")]
        public ActionResult WorkOrderSummary(DateTime? From, DateTime? To)
        {
            try
            {

                DateTime? dt1 = null;
                DateTime? dt2 = null;
                DateTime? ToFilterDate = null;
                string WorkOrderstartTime = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy") + " " + "00:00:00";
                string WorkOrderEndTime = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy") + " " + "23:59:59";
                dt1 = Convert.ToDateTime(WorkOrderstartTime);
                dt2 = Convert.ToDateTime(WorkOrderEndTime);
                string ToFilter = "";

                List<WorkOrders> WorkOrders = null;
                if (From != null && To != null)
                {
                    ToFilter = To.Value.Month + "/" + To.Value.Day + "/" + To.Value.Year + " " + "23:59:59";
                    ToFilterDate = Convert.ToDateTime(ToFilter);
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == false && x.AddedTime >= From && x.AddedTime <= ToFilterDate && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();
                }
                else if (From == null && To == null)
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == false && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();
                else if (From == null)
                {
                    ToFilter = To.Value.Month + "/" + To.Value.Day + "/" + To.Value.Year + " " + "23:59:59";
                    ToFilterDate = Convert.ToDateTime(ToFilter);
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == false && x.AddedTime <= ToFilterDate && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();
                }
                else if (To == null)
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == false && x.AddedTime >= From && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();

                ViewBag.WorkOrders = WorkOrders;


                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Data Entry")]
        public RedirectToRouteResult FilterCompletedWorkOrderSummary(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                return RedirectToAction("CompletedWorkOrderSummary", new { From = FromDate, To = ToDate });

            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = "Admin, Data Entry")]
        public PartialViewResult CompletedWorkOrderSummary(DateTime? From, DateTime? To)
        {
            try
            {

                DateTime? dt1 = null;
                DateTime? dt2 = null;
                DateTime? ToFilterDate = null;
                string WorkOrderstartTime = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy") + " " + "00:00:00";
                string WorkOrderEndTime = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy") + " " + "23:59:59";
                dt1 = Convert.ToDateTime(WorkOrderstartTime);
                dt2 = Convert.ToDateTime(WorkOrderEndTime);
                string ToFilter = "";

                List<WorkOrders> WorkOrders = null;
                if (From != null && To != null)
                {
                    ToFilter = To.Value.Month + "/" + To.Value.Day + "/" + To.Value.Year + " " + "23:59:59";
                    ToFilterDate = Convert.ToDateTime(ToFilter);
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == true && x.AddedTime >= From && x.AddedTime <= ToFilterDate && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();
                }
                else if (From == null && To == null)
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == true && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();
                else if (From == null)
                {
                    ToFilter = To.Value.Month + "/" + To.Value.Day + "/" + To.Value.Year + " " + "23:59:59";
                    ToFilterDate = Convert.ToDateTime(ToFilter);
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == true && x.AddedTime <= ToFilterDate && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();
                }
                else if (To == null)
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.IsCompleted == true && x.AddedTime >= From && x.IsFinished == true).OrderByDescending(x => x.Id).ToList();

                ViewBag.CompletedWorkOrders = WorkOrders;


                return PartialView("~/Views/Shared/_CompletedWorkOrders.cshtml");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = "Admin, Data Entry")]
        public ActionResult WorkOrderDataEntry(int WorkOrderId, string WorkOrderNumber)
        {
            try
            {
                var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == WorkOrderId).FirstOrDefault();

                ViewBag.WorkOrderId = WorkOrderId;
                ViewBag.WorkOrderNumber = WorkOrderNumber;
                ViewBag.WorkOrder = WorkOrder;

                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult SaveWorkOrderDataEntry(int WorkOrderId, string WorkOrderNumber)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == WorkOrderId).FirstOrDefault();
                    if (WorkOrder != null)
                    {
                        WorkOrder.WorkOrderNumber = WorkOrderNumber;
                        WorkOrder.ModifiedBy = CurrentUserName;
                        WorkOrder.ModifiedTime = DateTime.Now;
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                        return Json(false);
                }
                else
                    return Json(false);


            }
            catch (Exception)
            {

                throw;
            }
        }



        [Authorize(Roles = "Admin, Data Entry")]
        public ActionResult BoxDataEntry(int Number, int WorkOrderId, string WorkOrderNumber)
        {
            try
            {
                var Box = db.Boxes.Where(x => x.IsDeleted == false && x.WorkOrderId == WorkOrderId).Select(x => new { Id = x.Id, BoxBarcode = x.BoxBarcode, ImgPath = x.ImgPath, IsDataEntry = x.IsDataEntry }).ToList();

                if (Box.Count > Number)
                {
                    var TheBox = Box[Number];
                    ViewBag.NoBox = false;
                    ViewBag.Box = TheBox;
                    ViewBag.WorkOrderId = WorkOrderId;
                    ViewBag.WorkOrderNumber = WorkOrderNumber;
                    ViewBag.BoxCount = Box.Count;
                }
                else
                    return RedirectToAction("WorkOrderSummary");


                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult SaveBoxDataEntry(int BoxId, string BoxBarcode)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var Box = db.Boxes.Where(x => x.IsDeleted == false && x.Id == BoxId).FirstOrDefault();
                    if (Box != null)
                    {
                        Box.BoxBarcode = BoxBarcode;
                        Box.IsDataEntry = true;
                        Box.ModifiedBy = CurrentUserName;
                        Box.ModifiedTime = DateTime.Now;
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                        return Json(false);
                }
                else
                    return Json(false);


            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = "Admin, Data Entry")]
        public ActionResult FileDataEntry(int Number, int BoxId, int WorkOrderId, string BoxBarcode, string WorkOrderNumber)
        {
            try
            {
                var BoxCount = db.Boxes.Where(x => x.IsDeleted == false && x.WorkOrderId == WorkOrderId).Count();

                var File = (from f in db.Files
                            join fi in db.FilesImages
                            on f.Id equals fi.FileId
                            where f.IsDeleted == false
                            && fi.IsDeleted == false
                            where f.BoxId == BoxId
                            select new { f.Id, f.FileBarcode, f.Description, f.Department, f.AddedTime, fi.ImgPath, f.IsDataEntry }).ToList();


                if (File.Count > Number)
                {
                    var TheFile = File[Number];
                    ViewBag.NoFile = false;
                    ViewBag.File = TheFile;
                    ViewBag.FileCount = File.Count;
                    ViewBag.BoxCount = BoxCount;
                }
                else
                {
                    ViewBag.NoFile = true;
                    ViewBag.File = null;
                    ViewBag.FileCount = File.Count;
                    ViewBag.BoxCount = BoxCount;
                }

                ViewBag.BoxId = BoxId;
                ViewBag.WorkOrderId = WorkOrderId;
                ViewBag.BoxBarcode = BoxBarcode;
                ViewBag.WorkOrderNumber = WorkOrderNumber;

                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult SaveFileDataEntry(int FileId, string FileBarcode, string Description, DateTime? AddedTime, string Department)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var File = db.Files.Where(x => x.IsDeleted == false && x.Id == FileId).FirstOrDefault();
                    if (File != null)
                    {
                        File.FileBarcode = FileBarcode;
                        File.ModifiedBy = CurrentUserName;
                        File.Description = Description;
                        File.Department = Department;
                        File.IsDataEntry = true;
                        if (AddedTime == null)
                            File.AddedTime = DateTime.Now;
                        else
                            File.AddedTime = Convert.ToDateTime(AddedTime);
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                        return Json(false);
                }
                else
                    return Json(false);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult CompleteDataEntry(int WorkOrderId)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == WorkOrderId).FirstOrDefault();
                    if (WorkOrder != null)
                    {
                        WorkOrder.IsCompleted = true;
                        WorkOrder.ModifiedBy = CurrentUserName;
                        WorkOrder.ModifiedTime = DateTime.Now;
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                        return Json(false);
                }
                else
                    return Json(false);


            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public PartialViewResult FilterWorkOrders(DateTime? From, DateTime? To)
        {
            try
            {

                DateTime? dt1 = null;
                DateTime? dt2 = null;
                DateTime? ToFilterDate = null;
                string WorkOrderstartTime = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy") + " " + "00:00:00";
                string WorkOrderEndTime = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy") + " " + "23:59:59";
                dt1 = Convert.ToDateTime(WorkOrderstartTime);
                dt2 = Convert.ToDateTime(WorkOrderEndTime);
                string ToFilter = "";

                List<WorkOrders> WorkOrders = null;
                if (From != null && To != null)
                {
                    ToFilter = To.Value.Month + "/" + To.Value.Day + "/" + To.Value.Year + " " + "23:59:59";
                    ToFilterDate = Convert.ToDateTime(ToFilter);
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.AddedTime >= From && x.AddedTime <= ToFilterDate).OrderByDescending(x => x.Id).ToList();
                }
                else if (From == null && To == null)
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.AddedTime >= dt1 && x.AddedTime <= dt2).OrderByDescending(x => x.Id).ToList();
                else if (From == null)
                {
                    ToFilter = To.Value.Month + "/" + To.Value.Day + "/" + To.Value.Year + " " + "23:59:59";
                    ToFilterDate = Convert.ToDateTime(ToFilter);
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.AddedTime <= ToFilterDate).OrderByDescending(x => x.Id).ToList();
                }
                else if (To == null)
                    WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.AddedTime >= From).OrderByDescending(x => x.Id).ToList();

                WorkOrderViewModel W = null;
                WorkOrderList Wo = null;
                List<WorkOrderList> WoList = new List<WorkOrderList>();
                if (WorkOrders.Count > 0)
                {
                    foreach (var objWo in WorkOrders)
                    {
                        Wo = new WorkOrderList();
                        Wo.Id = objWo.Id;
                        Wo.WorkOrderNumber = objWo.WorkOrderNumber;
                        Wo.AddedBy = objWo.AddedBy;
                        Wo.AddedTime = objWo.AddedTime;
                        Wo.ModifiedBy = objWo.ModifiedBy;
                        Wo.ModifiedTime = objWo.ModifiedTime;
                        Wo.IsFinished = objWo.IsFinished;
                        var Boxes = db.Boxes.Where(x => x.IsDeleted == false && x.WorkOrderId == Wo.Id).Distinct().ToList();
                        Wo.BoxCount = Boxes.Count();
                        foreach (var objBox in Boxes)
                        {
                            var Files = db.Files.Where(x => x.IsDeleted == false && x.BoxId == objBox.Id).Distinct().ToList();
                            Wo.FileCount += Files.Count();
                        }
                        WoList.Add(Wo);
                    }
                    W = new WorkOrderViewModel();
                    W.WorkOrderList = WoList;
                    ViewData["WoList"] = W;
                    return PartialView("~/Views/Shared/_WOs.cshtml", W);
                }
                else
                {
                    W = new WorkOrderViewModel();
                    W.WorkOrderList = WoList;
                    ViewData["WoList"] = W;
                    return PartialView("~/Views/Shared/_WOs.cshtml", W);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPost]
        public JsonResult UpdateWoNumber(int WorkOrderId, string WorkOrderNumber)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == WorkOrderId).FirstOrDefault();
                    if (WorkOrder != null)
                    {
                        WorkOrder.WorkOrderNumber = WorkOrderNumber;
                        WorkOrder.ModifiedBy = CurrentUserName;
                        WorkOrder.ModifiedTime = DateTime.Now;
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                        return Json(false);
                }
                else
                    return Json(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult CheckWorkOrder(string WorkOrderNumber)
        {
            try
            {
                var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.WorkOrderNumber == WorkOrderNumber).FirstOrDefault();
                if (WorkOrder != null)
                    return Json(WorkOrder.WorkOrderNumber);
                else
                    return Json(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult AddWorkOrder(string WorkOrderNumber)
        {
            try
            {

                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var WorkOrder = new WorkOrders();

                    WorkOrder.WorkOrderNumber = WorkOrderNumber;
                    WorkOrder.AddedBy = CurrentUserName;
                    WorkOrder.AddedTime = DateTime.Now;
                    WorkOrder.ModifiedBy = CurrentUserName;
                    WorkOrder.ModifiedTime = DateTime.Now;
                    WorkOrder.IsDeleted = false;
                    WorkOrder.IsFinished = false;

                    db.WorkOrders.Add(WorkOrder);
                    db.SaveChanges();

                    return Json(new { WorkOrderId = WorkOrder.Id, WorkOrderNumber = WorkOrder.WorkOrderNumber });
                }
                else
                    return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AddWorkOrderImage()
        {
            try
            {

                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);
                var Data = db.WorkOrders.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).FirstOrDefault();

                if (Request.Files.Count == 0)
                    return Json(new { WorkOrderId = Data.Id, WorkOrderNumber = Data.WorkOrderNumber });

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    try
                    {
                        string UploadRoot = Convert.ToString(WebConfigurationManager.AppSettings["WoUploadRoot"]);
                        string WorkOrderFolder = @"" + UploadRoot + Data.WorkOrderNumber;
                        if (!Directory.Exists(WorkOrderFolder))
                        {
                            Directory.CreateDirectory(WorkOrderFolder);
                        }
                        if (Request.Files.Count > 0)
                        {
                            HttpFileCollectionBase files = Request.Files;
                            for (int i = 0; i < files.Count; i++)
                            {
                                HttpPostedFileBase file = files[i];
                                string fname;
                                string FileName;
                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                {
                                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                    FileName = testfiles[testfiles.Length - 1];
                                }
                                else
                                    FileName = file.FileName;

                                fname = Path.Combine(Server.MapPath("~/Uploads/WO-" + Data.WorkOrderNumber + "/"), FileName);

                                file.SaveAs(fname);

                                Stream strm = file.InputStream;
                                var targetFile = fname;
                                ReduceImageSize(0.7, strm, fname);

                                Data.ImgPath = "/Uploads/WO-" + Data.WorkOrderNumber + "/" + FileName;
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                return Json(new { WorkOrderId = Data.Id, WorkOrderNumber = Data.WorkOrderNumber });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult AddBox(AddBox Data)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var bx = db.Boxes.Where(x => x.IsDeleted == false && x.BoxBarcode == Data.BoxNumber && x.WorkOrderId == Data.WorkOrderId).FirstOrDefault();
                    if (bx == null)
                    {
                        var Box = new Boxes();
                        Box.BoxBarcode = Data.BoxNumber;
                        Box.WorkOrderId = Data.WorkOrderId;
                        Box.AddedBy = CurrentUserName;
                        Box.AddedTime = DateTime.Now;
                        Box.ModifiedBy = CurrentUserName;
                        Box.ModifiedTime = DateTime.Now;
                        Box.IsDeleted = false;

                        db.Boxes.Add(Box);
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                        return Json(false);

                }
                else
                    return Json(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult AddBoxImage()
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                var Data = db.Boxes.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).FirstOrDefault();
                var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == Data.WorkOrderId).FirstOrDefault();
                if (Request.Files.Count > 0)
                {
                    if (CurrentUserName != null && CurrentUserName != "")
                    {
                        try
                        {
                            string UploadRoot = Convert.ToString(WebConfigurationManager.AppSettings["WoUploadRoot"]);
                            string BoxFolder = @"" + UploadRoot + WorkOrder.WorkOrderNumber + "\\Box-" + Data.BoxBarcode;
                            if (!Directory.Exists(BoxFolder))
                            {
                                Directory.CreateDirectory(BoxFolder);
                            }

                            //  Get all files from Request object  
                            HttpFileCollectionBase files = Request.Files;
                            for (int i = 0; i < files.Count; i++)
                            {
                                HttpPostedFileBase file = files[i];
                                string fname;
                                string FileName;
                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                {
                                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                    FileName = testfiles[testfiles.Length - 1];
                                }
                                else
                                    FileName = file.FileName;

                                fname = Path.Combine(Server.MapPath("~/Uploads/WO-" + WorkOrder.WorkOrderNumber + "/Box-" + Data.BoxBarcode + "/"), FileName);
                                file.SaveAs(fname);

                                Stream strm = file.InputStream;
                                var targetFile = fname;
                                ReduceImageSize(0.8, strm, fname);

                                Data.ImgPath = "/Uploads/WO-" + WorkOrder.WorkOrderNumber + "/Box-" + Data.BoxBarcode + "/" + FileName;
                                db.SaveChanges();
                            }
                            var wo = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == Data.WorkOrderId).FirstOrDefault();

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
                return Json(new { BoxId = Data.Id, BoxNumber = Data.BoxBarcode });
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpPost]
        public JsonResult AddFile(int BoxId, string FileNumber)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    if (FileNumber == "")
                    {

                        var File = new Files();

                        File.FileBarcode = FileNumber;
                        File.BoxId = BoxId;
                        File.AddedBy = CurrentUserName;
                        File.AddedTime = DateTime.Now;
                        File.ModifiedBy = CurrentUserName;
                        File.ModifiedTime = DateTime.Now;
                        File.IsDeleted = false;

                        db.Files.Add(File);
                        db.SaveChanges();
                        return Json(true);
                    }
                    else
                    {
                        var f = db.Files.Where(x => x.IsDeleted == false && x.FileBarcode == FileNumber && x.BoxId == BoxId).FirstOrDefault();

                        if (f == null)
                        {
                            var File = new Files();

                            File.FileBarcode = FileNumber;
                            File.BoxId = BoxId;
                            File.AddedBy = CurrentUserName;
                            File.AddedTime = DateTime.Now;
                            File.ModifiedBy = CurrentUserName;
                            File.ModifiedTime = DateTime.Now;
                            File.IsDeleted = false;

                            db.Files.Add(File);
                            db.SaveChanges();
                            return Json(true);
                        }
                        else
                            return Json(false);
                    }
                }
                else
                    return Json(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult AddFileImage()
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                var Data = db.Files.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).FirstOrDefault();
                var BoxData = db.Boxes.Where(x => x.IsDeleted == false && x.Id == Data.BoxId).FirstOrDefault();
                var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == BoxData.WorkOrderId).FirstOrDefault();
                if (Request.Files.Count > 0)
                {
                    if (CurrentUserName != null && CurrentUserName != "")
                    {

                        string UploadRoot = Convert.ToString(WebConfigurationManager.AppSettings["WoUploadRoot"]);
                        string FileFolder = @"" + UploadRoot + WorkOrder.WorkOrderNumber + "\\Box-" + BoxData.BoxBarcode + "\\File-" + Data.FileBarcode;
                        if (!Directory.Exists(FileFolder))
                        {
                            Directory.CreateDirectory(FileFolder);
                        }
                        FilesImages FI = null;
                        try
                        {
                            //  Get all files from Request object  
                            HttpFileCollectionBase files = Request.Files;
                            for (int i = 0; i < files.Count; i++)
                            {
                                FI = new FilesImages();
                                HttpPostedFileBase file = files[i];
                                string fname;
                                string FileName;
                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                {
                                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                    FileName = testfiles[testfiles.Length - 1];
                                }
                                else
                                    FileName = file.FileName;

                                fname = Path.Combine(Server.MapPath("~/Uploads/WO-" + WorkOrder.WorkOrderNumber + "/Box-" + BoxData.BoxBarcode + "/File-" + Data.FileBarcode + "/"), FileName);
                                file.SaveAs(fname);

                                Stream strm = file.InputStream;
                                var targetFile = fname;
                                ReduceImageSize(0.7, strm, fname);

                                FI.FileId = Data.Id;
                                FI.ImgPath = "/Uploads/WO-" + WorkOrder.WorkOrderNumber + "/Box-" + BoxData.BoxBarcode + "/File-" + Data.FileBarcode + "/" + FileName;
                                FI.AddedBy = CurrentUserName;
                                FI.AddedTime = DateTime.Now;
                                FI.ModifiedBy = CurrentUserName;
                                FI.ModifiedTime = DateTime.Now;
                                db.FilesImages.Add(FI);
                                db.SaveChanges();

                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
                var fc = db.Files.Where(x => x.IsDeleted == false && x.BoxId == Data.BoxId).Distinct().Count();
                return Json(new { FileCount = fc });

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ReduceImageSize(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }

        [HttpPost]
        public JsonResult DeleteWorkOrder(int WorkOrderId)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var WorkOrder = db.WorkOrders.Where(x => x.Id == WorkOrderId && x.IsDeleted == false).FirstOrDefault();

                    WorkOrder.IsDeleted = true;
                    WorkOrder.ModifiedTime = DateTime.Now;
                    WorkOrder.ModifiedBy = CurrentUserName;

                    db.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteBox(int BoxId)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var Box = db.Boxes.Where(x => x.Id == BoxId && x.IsDeleted == false).FirstOrDefault();

                    Box.IsDeleted = true;
                    Box.ModifiedTime = DateTime.Now;
                    Box.ModifiedBy = CurrentUserName;

                    db.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteFile(int FileId)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var File = db.Files.Where(x => x.Id == FileId && x.IsDeleted == false).FirstOrDefault();

                    File.IsDeleted = true;
                    File.ModifiedTime = DateTime.Now;
                    File.ModifiedBy = CurrentUserName;
                    db.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteFileImage(int FileImage)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var File = db.FilesImages.Where(x => x.Id == FileImage && x.IsDeleted == false).FirstOrDefault();

                    File.IsDeleted = true;
                    File.ModifiedTime = DateTime.Now;
                    File.ModifiedBy = CurrentUserName;
                    db.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult FinishWorkOrder(int WorkOrderId)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var WorkOrder = db.WorkOrders.Where(x => x.Id == WorkOrderId && x.IsDeleted == false).FirstOrDefault();

                    WorkOrder.IsFinished = true;
                    WorkOrder.ModifiedTime = DateTime.Now;
                    WorkOrder.ModifiedBy = CurrentUserName;
                    db.SaveChanges();

                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public PartialViewResult WorkOrderDetail(int WorkOrderId)
        {
            try
            {
                var WorkOrders = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == WorkOrderId).FirstOrDefault();
                BoxesViewModel B = null;
                Box Bo = null;
                List<Box> BoxList = new List<Box>();
                if (WorkOrders != null)
                {
                    var Boxes = db.Boxes.Where(x => x.IsDeleted == false && x.WorkOrderId == WorkOrders.Id).Distinct().ToList();
                    foreach (var obj in Boxes)
                    {
                        Bo = new Box();
                        Bo.Id = obj.Id;
                        Bo.BoxBarcode = obj.BoxBarcode;
                        Bo.AddedBy = obj.AddedBy;
                        Bo.AddedTime = obj.AddedTime;
                        Bo.ModifiedBy = obj.ModifiedBy;
                        Bo.ModifiedTime = obj.ModifiedTime;
                        var Files = db.Files.Where(x => x.IsDeleted == false && x.BoxId == Bo.Id).Distinct().ToList();
                        Bo.FileCount = Files.Count();
                        BoxList.Add(Bo);
                    }
                }
                B = new BoxesViewModel();
                B.BoxesList = BoxList;
                ViewData["BoxList"] = B;
                return PartialView("~/Views/Shared/_WoDetail.cshtml", B);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PartialViewResult BoxDetail(int BoxId)
        {
            try
            {

                var Box = db.Boxes.Where(x => x.IsDeleted == false && x.Id == BoxId).FirstOrDefault();
                FileViewModel F = null;
                BFile BF = null;
                List<BFile> FileList = new List<BFile>();
                if (Box != null)
                {
                    var Files = db.Files.Where(x => x.IsDeleted == false && x.BoxId == Box.Id).Distinct().ToList();
                    foreach (var obj in Files)
                    {
                        var FileImages = db.FilesImages.Where(x => x.FileId == obj.Id).Distinct().ToList();
                        if (FileImages.Count > 0)
                        {
                            foreach (var FileImg in FileImages)
                            {
                                BF = new BFile();
                                BF.Id = obj.Id;
                                BF.FileBarcode = obj.FileBarcode;
                                BF.AddedBy = obj.AddedBy;
                                BF.AddedTime = obj.AddedTime;
                                BF.ModifiedBy = obj.ModifiedBy;
                                BF.ModifiedTime = obj.ModifiedTime;
                                BF.ImagePath = FileImg.ImgPath;
                                FileList.Add(BF);
                            }
                        }
                        else
                        {
                            BF = new BFile();
                            BF.Id = obj.Id;
                            BF.FileBarcode = obj.FileBarcode;
                            BF.AddedBy = obj.AddedBy;
                            BF.AddedTime = obj.AddedTime;
                            BF.ModifiedBy = obj.ModifiedBy;
                            BF.ModifiedTime = obj.ModifiedTime;
                            FileList.Add(BF);
                        }
                    }
                }
                F = new FileViewModel();
                F.FilesList = FileList;
                ViewData["FileList"] = F;
                return PartialView("~/Views/Shared/_BoxDetail.cshtml", FileList);

            }
            catch (Exception)
            {
                throw;
            }
        }




        [HttpPost]
        public JsonResult GetWorkOrderImage(int WorkOrderid)
        {
            try
            {
                var WorkOrder = db.WorkOrders.Where(x => x.IsDeleted == false && x.Id == WorkOrderid).Select(x => new { x.Id, x.WorkOrderNumber, x.ImgPath }).FirstOrDefault();
                if (WorkOrder != null)
                    return Json(WorkOrder);
                else
                    return Json(false);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult GetBoxImage(int BoxId)
        {
            try
            {
                var Box = db.Boxes.Where(x => x.IsDeleted == false && x.Id == BoxId).Select(x => new { x.Id, x.BoxBarcode, x.ImgPath }).FirstOrDefault();
                if (Box != null)
                    return Json(Box);
                else
                    return Json(false);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult GetFileImage(int FileId)
        {
            try
            {
                var File = (from f in db.Files
                            join fi in db.FilesImages
                            on f.Id equals fi.FileId
                            where f.IsDeleted == false
                            && fi.IsDeleted == false
                            where f.Id == FileId
                            select new { fi.Id, f.FileBarcode, fi.ImgPath }).FirstOrDefault();
                if (File != null)
                    return Json(File);
                else
                    return Json(false);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult EditBox(int BoxId)
        {
            try
            {
                var SessionUserName = User.Identity.Name;
                string CurrentUserName = Convert.ToString(SessionUserName);

                if (CurrentUserName != null && CurrentUserName != "")
                {
                    var Box = db.Boxes.Where(x => x.IsDeleted == false && x.Id == BoxId).FirstOrDefault();
                    var Files = db.Files.Where(x => x.IsDeleted == false && x.BoxId == Box.Id).Distinct().ToList();

                    EditBoxViewModel EditBox = new EditBoxViewModel();
                    List<BFile> Flist = new List<BFile>();
                    Box B = new Box();
                    B.Id = Box.Id;
                    B.BoxBarcode = Box.BoxBarcode;
                    B.ImagePath = Box.ImgPath;
                    B.AddedTime = Box.AddedTime;
                    B.AddedBy = Box.AddedBy;
                    B.ModifiedBy = CurrentUserName;
                    B.ModifiedTime = DateTime.Now;
                    EditBox.Box = B;

                    foreach (var obj in Files)
                    {
                        BFile F = new BFile();
                        F.Id = obj.Id;
                        F.FileBarcode = obj.FileBarcode;
                        F.AddedBy = obj.AddedBy;
                        F.AddedTime = obj.AddedTime;
                        F.ModifiedBy = CurrentUserName;
                        F.ModifiedTime = DateTime.Now;

                        var FileImages = db.FilesImages.Where(x => x.FileId == obj.Id).Distinct().ToList();
                        List<string> images = new List<string>();
                        foreach (var FileImg in FileImages)
                        {
                            images.Add(FileImg.ImgPath);
                        }
                        F.ImagePathList = images;

                        Flist.Add(F);
                    }

                    EditBox.FileList = Flist;

                    ViewBag.EditBox = EditBox;

                }
                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult UpdateBox(int BoxId, string BoxBarcode, int FileImageId/*, File Image */)
        {
            var SessionUserName = User.Identity.Name;
            string CurrentUserName = Convert.ToString(SessionUserName);

            if (CurrentUserName != null && CurrentUserName != "")
            {
                var Box = db.Boxes.Where(x => x.IsDeleted == false && x.Id == BoxId).FirstOrDefault();

                Box.BoxBarcode = BoxBarcode;
                Box.ModifiedTime = DateTime.Now;
                Box.ModifiedBy = CurrentUserName;

                //if (image.length > 0)
                //{                   
                //      Box.ImgPath = Image;
                //}
                //}

                db.SaveChanges();
                return Json(true);
            }
            else
                return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateFiles(int FileId, string FileBarcode, int FileImageId/*, File Image */)
        {
            var SessionUserName = User.Identity.Name;
            string CurrentUserName = Convert.ToString(SessionUserName);

            if (CurrentUserName != null && CurrentUserName != "")
            {
                var File = db.Files.Where(x => x.IsDeleted == false && x.Id == FileId).FirstOrDefault();

                File.FileBarcode = FileBarcode;
                File.ModifiedTime = DateTime.Now;
                File.ModifiedBy = CurrentUserName;

                //if (image.length > 0)
                //{                   
                //        var FileImages = db.FilesImages.Where(x => x.Id == FileImageId).FirstOrDefault();
                //        FileImages.ImgPath = Image;
                //        FileImages.ModifiedTime = DateTime.Now;
                //        FileImages.ModifiedBy = CurrentUserName;
                //}
                //}

                db.SaveChanges();

                return Json(true);
            }
            else
                return Json(false);
        }



        public ActionResult Boxes(int WorkOrderId, string WorkOrderNumber)
        {
            ViewBag.Title = "Boxes";
            ViewBag.WorkOrderId = WorkOrderId;
            ViewBag.WorkOrderNumber = WorkOrderNumber;
            int FileCount = 0;
            var BoxCount = db.Boxes.Where(x => x.IsDeleted == false && x.WorkOrderId == WorkOrderId).Distinct().Count();
            var Boxes = db.Boxes.Where(x => x.IsDeleted == false && x.WorkOrderId == WorkOrderId).Distinct().ToList();
            foreach (var objBox in Boxes)
            {
                var Files = db.Files.Where(x => x.IsDeleted == false && x.BoxId == objBox.Id).Distinct().ToList();
                FileCount += Files.Count();
            }
            ViewBag.BoxCount = BoxCount;
            ViewBag.FileCount = FileCount;
            return View();
        }

        public ActionResult Files(int WorkOrderId, string WorkOrderNumber, int BoxId, string BoxNumber)
        {
            ViewBag.Title = "Files";
            ViewBag.WorkOrderId = WorkOrderId;
            ViewBag.WorkOrderNumber = WorkOrderNumber;
            ViewBag.BoxId = BoxId;
            ViewBag.BoxNumber = BoxNumber;
            var FileCount = db.Files.Where(x => x.IsDeleted == false && x.BoxId == BoxId).Distinct().Count();
            ViewBag.FileCount = FileCount;
            return View();
        }
    }
}