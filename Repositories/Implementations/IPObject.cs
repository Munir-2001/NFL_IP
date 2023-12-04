using DataModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class IPObject : Iipobject
    {
        private readonly myappcontext _context;
        public IPObject(myappcontext c)
        {
            _context = c;
        }
        public async Task<List<countries>> GetCountries()
        {
            var k = await _context.countries.ToListAsync();
            return k;
        }
        public async Task<List<classification>> GetClassifications()
        {
            var l = await _context.classifications.ToListAsync();
            return l;
        }
        public async Task<int> Add(Ipobj model)
        {

            await _context.Iplists.AddAsync(model);


            Save();

            return model.Id;
        }

        public async Task<int> Addcomment(commentsobj comm)
        {

            await _context.comments.AddAsync(comm);
            await _context.SaveChangesAsync();

            return comm.Id;

        }


        private async Task Save()
        {
            int k = await _context.SaveChangesAsync();
            k = k + 0;


        }

        public async Task<int> Delete(int id)
        {
            var obj = await _context.Iplists.FindAsync(id);
            if (obj != null)
            {
                _context.Remove(obj);
                await Save();
            }
            
            return (id);

        }

       /* Task<List<classification>> Getclassificaiton() { 
        
        }*/


        public async Task<List<Ipobj>> GetAll()
        {
            var obj = await _context.Iplists.ToListAsync();
            return obj;
        }
        public async Task<List<commentsobj>> GetComments(int id)
        {

            //transaction.Where(x => x.ent.Contains(str)).ToListAsync();
            //   var k = await _context.comments.Where(x => commentsobj.Contains(id) );

            var k = await _context.comments.ToListAsync();
            var i = k.Where(x => x.IPid == id).ToList();
            return i;


            /* var str = 
          var   searched = await _context.Iplists.Where(x => x.Id.Contains(str)).ToListAsync();
             return View(searched);
             var commentlists = await _context.comments.FindAsync();
             return commentlists;*/



        }
        public async Task<Ipobj> GetbyId(int id)
        {
            var k = await _context.Iplists.FindAsync(id);
            return k;


        }




        public async Task<int> Update(Ipobj model)
        {

            var iplist = await _context.Iplists.FindAsync(model.Id);
            if (iplist != null)
            {
                iplist.status = model.status;
                iplist.date = model.date;
                iplist.entity = model.entity;
                iplist.cost = model.cost;
                iplist.renewaldate = model.renewaldate;
                iplist.IPListname = model.IPListname;
                iplist.country = model.country;
                iplist.logos = model.logos;
                _context.Update(iplist);
                await Save();


            }
                return (model.Id);

        }
    }
}

