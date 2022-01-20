using DAL.Model;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PharmacyStock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : ControllerBase
    {
        List<Medicine> medicinesList = new List<Medicine>();
        Result _result = new Result();
        DBOperations dbOperations = new DBOperations();

        [Authorize]
        [HttpGet]
        public List<Medicine> GetMedicines()
        {
            //Listedeki ilaçlar getirilecek
            return dbOperations.GetMedicines();
        }

        [HttpGet("/Medicine/GetMedicinePaging")]
        public IActionResult GetMedicinePaging([FromQuery] OwnerParameters ownerParameters)
        {
            var owners = dbOperations.GetMedicines() //Db de users tablosundaki herşey
                                                     //.OrderBy(on => on.Name) //Sıralama
           .Skip(ownerParameters.PageNumber) //kaçıncı kayıttn itibaren veri gelecek
           .Take(ownerParameters.PageSize) //Belirttiğimiz sayıda kayıt getirir. Örn : 3, 2 
           .ToList();

            return Ok(owners);
        }

        [HttpGet("{id}")]
        public Medicine GetMedicines(int id)
        {
            //Id'ye göre ilaç getirilecek
            List<Medicine> medicinesList = new List<Medicine>();

            Medicine? resultObject = new Medicine();
            resultObject = medicinesList.Find(x => x.MedicineId == id);
            return resultObject;
        }

        [HttpPost]
        public Result Post(Medicine medicine)
        {
            Medicine mdc = dbOperations.FindMedicine(medicine.Company, medicine.MedicineName);
            //Yeni ilaç listede var mı?
            bool medicineCheck = (mdc != null) ? true : false;

            //Liseye yeni ilaç eklenecek
            if (medicineCheck == false)
            {
                if(dbOperations.AddModel(medicine) == true)
                {
                    _result.status = 1;
                    _result.message = "New medicine added to the list.";
                }
                else
                {
                    _result.status = 0;
                    _result.message = "Error, user could not be added.";
                }

            }
            else
            {
                _result.status = 0;
                _result.message = "The medicine to be added is already in the list";
            }
            
            return _result;
        }

        [HttpPut("{MedicineId}")]
        public Result Update(int MedicineId, Medicine newValue)
        {
            //Listede ilaç güncelleme işlemi yapılacak.
            Medicine? _oldValue = medicinesList.Find(m => m.MedicineId == MedicineId);
            if (_oldValue != null)
            {
                medicinesList.Add(newValue);
                medicinesList.Remove(_oldValue);

                _result.status = 1;
                _result.message = "Successfully updated";
            }
            else
            {
                _result.status = 0;
                _result.message = "No medicine found.";

            }
            return _result;
        }
        [HttpDelete("{MedicineId}")]
        public Result Delete(int MedicineId)
        {
            //Listeden Id'ye göre ilaç silinecek.
            
            if (dbOperations.DeleteModel(MedicineId))
            {
                _result.status = 1;
                _result.message = "Medication information has been deleted.";
            }
            else
            {
                _result.status = 0;
                _result.message = "Medication not found or deleted.";
            }
            return _result;
        }
    };
}
