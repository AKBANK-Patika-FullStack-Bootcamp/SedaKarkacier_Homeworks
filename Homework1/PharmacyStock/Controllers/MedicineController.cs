using Microsoft.AspNetCore.Mvc;
using PharmacyStock.Model;

namespace PharmacyStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        public List<Medicine> AddMedicines()
        {
            //İlaç özelllikleri ile yeni bir liste oluşturuluyor.
            List<Medicine> mdcn = new List<Medicine>();

            mdcn.Add(new Model.Medicine { Id = 1, MedicineName = "ABILIFY 30 mg Tablet", Company = "Koçak", InfluenceGroup = "Antibiotic", Price = 13, Stock = 21 });
            mdcn.Add(new Model.Medicine { Id = 2, MedicineName = "ARVELES 25 mg Tablet", Company = "Menarini International", InfluenceGroup = "Painkiller", Price = 17, Stock = 25 });
            mdcn.Add(new Model.Medicine { Id = 3, MedicineName = "BENICAL COLD 20 Film Tablet", Company = "Bayer", InfluenceGroup = "Common Cold", Price = 12, Stock = 11 });

            return mdcn;
        } 

        List<Medicine> medicinesList = new List<Medicine>();
        Result _result = new Result();

        [HttpGet]
        public List<Medicine> GetMedicines()
        {
            //Listedeki ilaçlar getirilecek
            medicinesList = AddMedicines().OrderBy(m => m.Id).ToList();
            return medicinesList;
        }

        [HttpGet("{id}")]
        public Medicine GetMedicine(int id)
        {
            //Id'ye göre ilaç getirilecek
            List<Medicine> medicinesList = new List<Medicine>();
            medicinesList = AddMedicines();

            Medicine resultObject = new Medicine();
            resultObject = medicinesList.FirstOrDefault(resultObject => resultObject.Id == id);

            return resultObject;
        }

        [HttpPost]
        public Result Post(Medicine medicine)
        {
            //Liste dolduruluyor.
            medicinesList = AddMedicines();

            //Yeni ilaç listede var mı?
            bool medicineCheck = medicinesList.Select(m => m.Id == medicine.Id && m.MedicineName == medicine.MedicineName).FirstOrDefault();

            //Liseye yeni ilaç eklenecek
            if (medicineCheck == false)
            {
                medicinesList.Add(medicine);
                _result.status = 1;
                _result.message = "Yeni ilaç listeye eklendi.";
                _result.MedicinesList = medicinesList;
            }
            else
            {
                _result.status = 0;
                _result.message = "Eklenmeye çalışılan ilaç listede zaten mevcut";
            }
            
            return _result;
        }

        [HttpPut("{medicineId}")]
        public Result Update(int medicineId, Medicine newValue)
        {
            //Liste dolduruluyor.
            medicinesList = AddMedicines();

            //Listede ilaç güncelleme işlemi yapılacak.
            Medicine? _oldValue = medicinesList.Find(m => m.Id == medicineId);
            if (_oldValue != null)
            {
                medicinesList.Add(newValue);
                medicinesList.Remove(_oldValue);

                _result.status = 1;
                _result.message = "Başarıyla güncellendi";
                _result.MedicinesList = medicinesList;
            }
            else
            {
                _result.status = 0;
                _result.message = "İlaç bulunamadı.";

            }
            return _result;
        }
        [HttpDelete("{medicineId}")]
        public Result Delete(int medicineId)
        {
            //Liste dolduruluyor.
            medicinesList = AddMedicines();
            //Listeden Id'ye göre ilaç silinecek.
            Medicine? _oldValue = medicinesList.Find(m => m.Id == medicineId);
            if (_oldValue != null)
            {
                medicinesList.Remove(_oldValue);

                _result.status = 1;
                _result.message = "İlaç bilgileri silindi";
                _result.MedicinesList = medicinesList;
            }
            else
            {
                _result.status = 0;
                _result.message = "İlaç bulunamadı veya silinmiş.";
            }
            return _result;
        }
    };
}
