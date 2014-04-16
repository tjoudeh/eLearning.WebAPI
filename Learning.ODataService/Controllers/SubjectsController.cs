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
    public class SubjectsController : EntitySetController<Subject, int>
    {
        LearningContext ctx = new LearningContext();

        [Queryable()]
        public override IQueryable<Subject> Get()
        {
            return ctx.Subjects.AsQueryable();
        }

        protected override Subject GetEntityByKey(int key)
        {
            return ctx.Subjects.Find(key);
        }

        protected override int GetKey(Subject entity)
        {
            return entity.Id;
        }

        protected override Subject CreateEntity(Subject entity)
        {
            ctx.Subjects.Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        protected override Subject UpdateEntity(int key, Subject update)
        {
            if (!ctx.Subjects.Any(o => o.Id == key))
            {
                throw Helpers.ResourceNotFoundError(Request);
            }
            update.Id = key;
            ctx.Subjects.Attach(update);
            ctx.Entry(update).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return update;
        }

        protected override void Dispose(bool disposing)
        {
            ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}