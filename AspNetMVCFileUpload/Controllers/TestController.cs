using AspNetMVCFileUpload.Entities;
using AspNetMVCFileUpload.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVCFileUpload.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            var context = new MainContext();
            var test = context.Test.Select(s => s);
            var model = Mapper.Map<IEnumerable<TestViewModel>>(test);
            return View(model);
        }


        public ActionResult Process()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Process(TestViewModel model, HttpPostedFileBase file, int number)
        {
            if (file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Files/"), fileName);
                file.SaveAs(filePath);
            }
            var entity = Mapper.Map<Test>(model);
            entity.Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");
            entity.NumberEmployee = number.ToString();
            entity.File_Name = file.FileName;
            entity.File_Content = file.ContentType;
            var context = new MainContext();
            context.Test.Add(entity);
            context.SaveChanges();
            return RedirectToAction("ProcessFile", "Test", new { @number = number });
        }

        public ActionResult ProcessFile(string number)
        {
            var result = "";
            Array userData = null;
            char[] delimiterChar = { ',' };

            var context = new MainContext();
            var test = context.Test.FirstOrDefault((s => s.NumberEmployee == number));
            var dataFile = Server.MapPath("~/Files/" + test.File_Name);
            var filePath = Server.MapPath("~/Files/" + "file_output.txt");
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            if (System.IO.File.Exists(dataFile))
            {
                userData = System.IO.File.ReadAllLines(dataFile);
                if (userData == null)
                {
                    // Empty file.
                    result = "The file is empty.";
                }
                else
                {
                    int day = 0;
                    
                    Random rnd = new Random();
                    
                    using (StreamWriter writer = System.IO.File.CreateText(filePath))
                    {
                        
                       for(int i=0;i<userData.Length;i++)
                        {
                            day += 1;
                            int viajes = 0;
                            int peso = 0;
                            while (peso <= 100)
                            {
                                int Random = rnd.Next(1, userData.Length);
                                peso += Convert.ToInt32(userData.GetValue(Random));
                                viajes += 1;
                            }
                            writer.WriteLine("case #" + day + ": " + viajes);

                        }
                          

                     }
                    
                }
            }

            return RedirectToAction("DownloadFile", "Test", new { @FileOutput = filePath });
        }

        public ActionResult DownloadFile(string FileOutput)
        {
           

            byte[] fileBytes = System.IO.File.ReadAllBytes(FileOutput);
            string fileName = FileOutput;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            
        }


    }
}