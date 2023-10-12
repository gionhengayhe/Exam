using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class StudentService
    {
        public List<SinhVien> GetAll()
        {
            Model model = new Model(); 
            return model.SinhViens.ToList();
        }

        public SinhVien FindByID(string id)
        {
            Model model = new Model();
            return model.SinhViens.FirstOrDefault(x => x.MaSV == id);
        }

        public void InsertUpdate(SinhVien s)
        {
            Model model = new Model();
            model.SinhViens.AddOrUpdate(s);
            model.SaveChanges();
        }

        public void Delete(string id)
        {
            Model context = new Model();
            SinhVien s = context.SinhViens.FirstOrDefault(p => p.MaSV == id);
            if (s != null)
            {
                context.SinhViens.Remove(s);
            }
            context.SaveChanges();
        }

    }
}
