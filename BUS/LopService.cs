using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LopService
    {
        public List<Lop> GetAll()
        {
            Model model = new Model();
            return model.Lops.ToList();
        }

        public Lop Timmalop(string tenlop)
        {
            Model model = new Model();
            return model.Lops.FirstOrDefault(p => p.TenLop == tenlop);
        }

    }
}
