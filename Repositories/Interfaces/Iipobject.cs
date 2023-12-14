using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface Iipobject
    {

        Task<List<Ipobj>> GetAll();
      //  Task<Response<Document>> UploadDocument(string uploadBasePath,IFormFile formFile);

        Task<List<commentsobj>> GetComments(int id);
        Task<int> Addcomment(commentsobj comm);
        Task<List<reportstatus>> Getreportstatus();

        Task<List<Currency>> GetCurrency();


        Task<List<classification>> GetClassifications();
        Task<Ipobj> GetbyId(int id);
        Task<int> Add(Ipobj model);
        Task<int> Update(Ipobj model);
        Task<int> Delete(int id);

        

            Task<List<countries>> GetCountries();
       //Task<List<classification>> Getclassificaiton();


        // Task<List<classification>> GetClassifications();


    }
}
