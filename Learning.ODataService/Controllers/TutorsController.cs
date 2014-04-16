using Learning.Data;
using Learning.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace Learning.ODataService.Controllers
{
    public class TutorsController : EntitySetController<Tutor, int>
    {
        LearningContext ctx = new LearningContext();
        
        [Queryable()]
        public override IQueryable<Tutor> Get()
        {
            return ctx.Tutors.AsQueryable();
        }

        protected override Tutor GetEntityByKey(int key)
        {
            return ctx.Tutors.Find(key);
        }

        protected override int GetKey(Tutor entity)
        {
            return entity.Id;
        }

        protected override Tutor CreateEntity(Tutor entity)
        {
            Tutor insertedTutor = entity;
            insertedTutor.UserName = string.Format("{0}.{1}",entity.FirstName, entity.LastName);
            insertedTutor.Password = Helpers.RandomString(8);
            ctx.Tutors.Add(insertedTutor);
            ctx.SaveChanges();
            return entity;
        }
        
        protected override Tutor PatchEntity(int key, Delta<Tutor> patch)
        {
            var tutor = ctx.Tutors.Find(key);
            if (tutor == null)
            {
                throw Helpers.ResourceNotFoundError(Request);
            }
                
            patch.Patch(tutor);
            ctx.SaveChanges();
            return tutor;
        }

        public override void Delete(int key)
        {
            var tutor = ctx.Tutors.Find(key);
            if (tutor == null) 
            {
                throw Helpers.ResourceNotFoundError(Request);
            }
            
            ctx.Tutors.Remove(tutor);
            ctx.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}