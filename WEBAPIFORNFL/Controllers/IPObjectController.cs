using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting.Internal;
using Repositories.Implementations;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace WEBAPIFORNFL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPObjectController : Controller
    {
        private readonly Iipobject iipobject;
        IWebHostEnvironment webHostEnvironment;

        public IPObjectController(Iipobject iprepo, IWebHostEnvironment webHostEnvironment)
        {
            iipobject = iprepo;
            this.webHostEnvironment = webHostEnvironment;


        }
        [HttpGet("GetCountries")]
        public async Task<IActionResult> Getcountries() {
            var countrydd = await iipobject.GetCountries();

            DataModels.Response<List<countries>> r = new Response<List<countries>>
            {
                Success = true,
                Message = "fetched the countries.",
                Data = countrydd
            };
            return Ok(r);
        }
        [HttpGet("GetClassification")]
        public async Task<IActionResult> Getclassification()
        {
            var classdd = await iipobject.GetClassifications();

            DataModels.Response<List<classification>> r = new Response<List<classification>>
            {
                Success = true,
                Message = "fetched the classifications.",
                Data = classdd
            };

            return Ok(r);
        }
        [HttpGet("GetCurrency")]
        public async Task<IActionResult> Getcurrency()
        {
            var classdd = await iipobject.GetCurrency();

            DataModels.Response<List<Currency>> r = new Response<List<Currency>>
            {
                Success = true,
                Message = "fetched the classifications.",
                Data = classdd
            };

            return Ok(r);
        }

        [HttpGet("GetReportStatus")]
        public async Task<IActionResult> Getreportstatuses()
        {
            var classdd = await iipobject.Getreportstatus();

            DataModels.Response<List<reportstatus>> r = new Response<List<reportstatus>>
            {
                Success = true,
                Message = "fetched the classifications.",
                Data = classdd
            };

            return Ok(r);
        }


        [HttpGet("Index")]
        //[Route("")]
        public async Task<IActionResult> Index()
        {
            /*IPOBJVIEWMODEL k = new IPOBJVIEWMODEL()
            {
                countries = await iipobject.GetCountries()
                //  classifications = await iipobject.stAsync(),
                // class

            };*/
            var iplists = await iipobject.GetAll();
            iplists.Sort((x, y) => DateTime.Compare(y.date,x.date));



            DataModels.Response<List<Ipobj>> r = new Response<List<Ipobj>>
            {
                Success = true,
                Message = "got the tasks",
                Data = iplists
            };
            //var iplists = await iipobject.GetAll();

            return Ok(r);
        }



        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            Ipobj ipobject = await iipobject.GetbyId(id);
            if (ipobject != null)
            {
               var t=iipobject.Delete(id);
                t.Wait();
                DataModels.Response<int> r = new Response<int>
                {
                    Success = true,
                    Message = "got the tasks",
                    Data = t.Result
                };
                return Ok(r);
            }
            else
            {
                DataModels.Response<int> r = new Response<int>
                {
                    Success = false,
                    Message = "not got the tasks",
                    Data =   -1
                };
                return Ok(r);
            }

        }

        [HttpGet("Getcomment/{id}")]
        public async Task<IActionResult> GetComments(int id) { 
        var commentss = await iipobject.GetComments(id);
            /*            commentss.OrderBy((x, y) => DateTime.Compare(x.date, y.date ));
            */
            var newList = commentss.OrderByDescending(x => x.Id).ToList();
           // commentss.OrderByDescending((x)=> x.Id);
            DataModels.Response<List<commentsobj>> r = new Response<List<commentsobj>>
            {
                Success = true,
                Message = "fetched the countries.",
                Data = newList
            };

            return Ok(r);
        }

        [HttpPost, ActionName("Delete/{id}")]
        public async Task<IActionResult> DeleteComplete(int id)
        {

            return RedirectToAction("Index");
        }


        //if id is 0 then empty form for making if id not 0 then edit scene
        [HttpGet("CreateOrEdit/{id}")]
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                IPOBJVIEWMODEL model = new IPOBJVIEWMODEL()
                {
                    countries = await iipobject.GetCountries(),
                    commentslist = await iipobject.GetComments(id),
                    classifications = await iipobject.GetClassifications()
                };
                //for fetching all data together while loading a page. if 0 then get all. if number then get a particular ip.
                DataModels.Response<IPOBJVIEWMODEL> r = new Response<IPOBJVIEWMODEL>
                {
                    Success = true,
                    Message = "for fetching all data together while loading a page. if 0 then get all.",
                    Data = model
                };
                return Ok(r);
            }
            else
            {



                /*IPOBJVIEWMODEL model = new IPOBJVIEWMODEL()
                {
                    countries = await iipobject.GetCountries(),
                    Ipobj = await iipobject.GetbyId(id),
                    commentslist = await iipobject.GetComments(id),
                    classifications = await iipobject.GetClassifications(),

                };*/


                /*if (iipobject.GetbyId(id) != null)
                {*/
               
                Ipobjpic model = new Ipobjpic(){ 
                    ipobject=await iipobject.GetbyId(id)
                    //image=
                    };

                //var k = await DownloadFile(model.ipobject.logos);
               // model.image = k;


                /*
                 var query = await iipobject.UploadDocument(HostingEnvironment.WebRootPath,
                                     uploadDocumentRequest.AllowDuplication,
                                     new Document
                                     {
                                         Name = uploadDocumentRequest.Doc.FileName,
                                         //this is being changed later
                                         FileName = uploadDocumentRequest.Doc.FileName,
                                         Checksum = uploadDocumentRequest.Checksum,
                                         Source = uploadDocumentRequest.Source,
                                         Comments = uploadDocumentRequest.Comments

                                     }, uploadDocumentRequest.Doc);
                 if (query.Success)
                     return Ok(query);
                 return BadRequest(query);*/




                /*string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                var filename = model.ipobject.logos;
                string filePath = Path.Combine(uploadFolder, filename);
               // model.image.CopyTo(new FileStream(filePath, FileMode.Open));                /*string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                var filename = model.ipobject.logos;
                string filePath = Path.Combine(uploadFolder, filename);

                using (var stream = System.IO.File.OpenRead(filePath))
                {


                    model.image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    
                    
                }
*/




               


                DataModels.Response<Ipobjpic> r = new Response<Ipobjpic>
                    {
                        Success = true,
                        Message = "for fetching all data of one IP with other existing dropdown data as well",
                        Data = model
                    };
                    return Ok(r);
                //}



                return Ok("hello not found in db the ID");
            }

        

    }

       













        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(commentsobj ip)

        {

          
            //int fk = ip.Id;
            ip.date = DateTime.Today;
            var t =  await iipobject.Addcomment(ip);

            DataModels.Response<int> r = new Response<int> {
                Success = true,
                Message = "got the tasks",
                Data = t
            };


            //task.Wait();


            return Ok(r);

        }


        [HttpPost("UploadFile")]

        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationtoken )
        {

            var result = await WriteFile(file);
            return Ok(result);
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //filename = DateTime.Now.Ticks.ToString() + extension;
                filename = 1.ToString() + extension;
                //idhe 1 k bajai ipobj.id krdenge to auto replace
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }
            return filename;
        }



        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }








        [HttpPost("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(Ipobj ipa)
        {




            string filename = "";
            try
            {
                /*if (ipa.image != null)
                {

                    //model.ipobject.logos= await WriteFile(model);

                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "_" + ipa.ipobject.Id + "_" + ipa.image.FileName;
                    string filePath = Path.Combine(uploadFolder, filename);
                    ipa.image.CopyTo(new FileStream(filePath, FileMode.Create));
                    *//*if (true)
                    {
                        var s = ipa.image;
                        s = s;
                    }*//*

                 //   ipa.ipobject.logos = await WriteFile(ipa.image);



                }*/

               /* string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                filename = ipa.logos;
                string filePath = Path.Combine(uploadFolder, filename);*/
                //ipa.logos.CopyTo(new FileStream(filePath, FileMode.Create));

                if (ipa.Id == 0)
                {
                    var ipo = new Ipobj()
                    {
                        entity = ipa.entity,
                        entityname = ipa.entityname,
                        cost = ipa.cost,
                        country = ipa.country,
                        logos = filename,
                        renewaldate = DateTime.Today,
                        date = DateTime.Today,
                        lawyer = ipa.lawyer,
                        classification = ipa.classification,
                        status = ipa.status,
                        IPListname = ipa.IPListname,
                        Currency=ipa.Currency,
                        reportstatus= ipa.reportstatus
                    };

                    //   ipo.logos = ipo.logos;
                    //ipo.classification = "Legal";
                    ipo.logos = filename;

                    var k = ipo;
                    var task = await iipobject.Add(k);
                    DataModels.Response<int> r = new Response<int>
                    {
                        Success = true,
                        Message = "iplist added.",
                        Data = task
                    };

                    return Ok(r);
                }
                else
                {
                   // var t1 = await iipobject.GetbyId(ipa.Id);
                    //var t2 = await iipobject.Delete(ipa.Id);

                    var ipo = new Ipobj()
                    {
                        Id=ipa.Id,
                        entity = ipa.entity,
                        entityname = ipa.entityname,
                        cost = ipa.cost,
                        country = ipa.country,
                        logos = filename,
                        renewaldate = DateTime.Today,
                        date = DateTime.Today,
                        lawyer = ipa.lawyer,
                        classification = ipa.classification,
                        status = ipa.status,
                        IPListname = ipa.IPListname,
                         Currency = ipa.Currency,
                        reportstatus = ipa.reportstatus
                    };
                    ipo.lawyer = ipa.lawyer;
                    ipo.logos = filename;
                    ipo.country = ipa.country;
                    ipo.cost = ipa.cost;
                    ipo.classification = ipa.classification;
                    ipo.cost = ipa.cost;
                    var k = ipo;

                    var task = await iipobject.Update(k);


                    DataModels.Response<int> r = new Response<int>
                    {
                        Success = true,
                        Message = "got the tasks, and the returned DB ID is",
                        Data = task
                    };

                    return Ok(r);
                }


            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                DataModels.Response<int> r = new Response<int>
                {
                    Success = false,
                    Message = "got failed as " + ex.Message
                    
                };
                return Ok(r);
            }
        }

    }
}
