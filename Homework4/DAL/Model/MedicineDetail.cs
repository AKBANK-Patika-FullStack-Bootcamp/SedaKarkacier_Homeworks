using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MedicineDetail
    {
        //Inner join ile iki tablodan birer değeri birleştirmek için
        public string? Usage_Information { get; set; }
        public string? MedicineName { get; set; }
    }
}
