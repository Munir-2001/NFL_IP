using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repositories.Interfaces;
using System.Drawing;
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
            return Ok(countrydd);
        }
        [HttpGet("GetClassification")]
        public async Task<IActionResult> Getclassification()
        {
            var classdd = await iipobject.GetClassifications();
            return Ok(classdd);
        }
        [HttpGet("Index")]
        //[Route("")]
        public async Task<IActionResult> Index()
        {
            IPOBJVIEWMODEL k = new IPOBJVIEWMODEL()
            {
                countries = await iipobject.GetCountries()
                //  classifications = await iipobject.stAsync(),
                // class

            };
            var iplists = await iipobject.GetAll();

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
                    classifications = await iipobject.GetClassifications(),


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



                IPOBJVIEWMODEL model = new IPOBJVIEWMODEL()
                {
                    countries = await iipobject.GetCountries(),
                    Ipobj = await iipobject.GetbyId(id),
                    commentslist = await iipobject.GetComments(id),
                    classifications = await iipobject.GetClassifications(),

                };
                if (model != null)
                {
                    model.entity = model.Ipobj.entity;
                    model.country = model.Ipobj.country;
                    model.date = DateTime.Today;
                    model.status = model.Ipobj.status;
                    model.IPListname = model.Ipobj.IPListname;
                    model.classifications = model.classifications;
                    model.logos = model.logos;
                    model.cost = model.Ipobj.cost;
                    model.entityname = model.Ipobj.entityname;
                    model.Id = model.Ipobj.Id;
                    model.lawyer = model.Ipobj.lawyer;


                    DataModels.Response<IPOBJVIEWMODEL> r = new Response<IPOBJVIEWMODEL>
                    {
                        Success = true,
                        Message = "for fetching all data of one IP with other existing dropdown data as well",
                        Data = model
                    };
                    return Ok(r);
                }



                return Ok("hello");
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

        [HttpPost("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(Ipobjpic ipa)
        {

            string filename = "";
            try
            {
                if (ipa.image != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "_" + ipa.ipobject.Id + "_" + ipa.image.FileName;
                    string filePath = Path.Combine(uploadFolder, filename);
                    ipa.image.CopyTo(new FileStream(filePath, FileMode.Create));
                    /*if (true)
                    {
                        var s = ipa.image;
                        s = s;
                    }*/



                }
               
                if (ipa.ipobject.Id == 0)
                {
                    var ipo = new Ipobj()
                    {
                        entity = ipa.ipobject.entity,
                        entityname = ipa.ipobject.entityname,
                        cost = ipa.ipobject.cost,
                        country = ipa.ipobject.country,
                        logos = filename,
                        renewaldate = DateTime.Today,
                        date = DateTime.Today,
                        lawyer = ipa.ipobject.lawyer,
                        status = ipa.ipobject.status,
                        IPListname = ipa.ipobject.IPListname,
                        classification= ipa.ipobject.classification,

                    };
                    ipo.logos = filename;
                    var k = ipo;
                    var task = iipobject.Add(k);
                    task.Wait();
                    return Ok(task);
                }
                else
                {
                    var task = await iipobject.Update(ipa.ipobject);

                    DataModels.Response<int> r = new Response<int>
                    {
                        Success = true,
                        Message = "got the tasks",
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
