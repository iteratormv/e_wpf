using EX.Model.DbLayer;
using EX.Model.Exel;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories
{
    public class VisitorRepository
    {
        EContext context;
        ExelData exelData;

        public VisitorRepository()
        {
            context = new EContext();
        }

        public VisitorRepository(string file)
        {
            context = new EContext();
            if (!context.Database.Exists())
            {
                context.Database.Delete();
                context.SaveChanges();
                exelData = new ExelData(file);
                exelData.setDataToCollection(context.Visitors);
                context.SaveChanges();
            }
        }

        public Visitor AddOrUpdateVisitor(Visitor visitor)
        {
            if(context.Visitors.Where(s => s.Column1 == visitor.Column1 &&
                                               s.Column2 == visitor.Column2 &&
                                               s.Column3 == visitor.Column3 &&
                                               s.Column4 == visitor.Column4 &&
                                               s.Column5 == visitor.Column5 &&
                                               s.Column6 == visitor.Column6 &&
                                               s.Column7 == visitor.Column7 &&
                                               s.Column8 == visitor.Column8 &&
                                               s.Column9 == visitor.Column9 &&
                                               s.Column10 == visitor.Column10 &&
                                               s.Column11 == visitor.Column11 &&
                                               s.Column12 == visitor.Column12 &&
                                               s.Column13 == visitor.Column13 &&
                                               s.Column14 == visitor.Column14 &&
                                               s.Column15 == visitor.Column15).Count() == 0)
            {
                context.Visitors.AddOrUpdate(visitor);
                context.SaveChanges();
            }           
            return context.Visitors.Where(s => s.Column1 == visitor.Column1 &&
                                               s.Column2 == visitor.Column2 &&
                                               s.Column3 == visitor.Column3 &&
                                               s.Column4 == visitor.Column4 &&
                                               s.Column5 == visitor.Column5 &&
                                               s.Column6 == visitor.Column6 &&
                                               s.Column7 == visitor.Column7 &&
                                               s.Column8 == visitor.Column8 &&
                                               s.Column9 == visitor.Column9 &&
                                               s.Column10 == visitor.Column10 &&
                                               s.Column11 == visitor.Column11 &&
                                               s.Column12 == visitor.Column12 &&
                                               s.Column13 == visitor.Column13 &&
                                               s.Column14 == visitor.Column14 &&
                                               s.Column15 == visitor.Column15).FirstOrDefault();
        }

        public IEnumerable<Visitor> GetAllVisitors()
        {
            var v = context.Visitors.ToList();
            return v;
        }

        public Visitor GetVisitorById(int Id)
        {
            return context.Visitors.Where(c => c.Id == Id).FirstOrDefault();
        }

        public void RemoveVisitor(Visitor visitor)
        {
            context.Visitors.Remove(visitor);
            context.SaveChanges();
        }

        public void RemoveVisitorById(int Id)
        {
            context.Visitors.Remove(context.Visitors.Where(c => c.Id == Id).FirstOrDefault());
            context.SaveChanges();
        }

    }
}
