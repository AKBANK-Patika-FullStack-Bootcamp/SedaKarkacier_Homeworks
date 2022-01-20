using DAL.Model;
using Entities;

namespace PharmacyStock.Controllers
{
    public class DBOperations
    {
        private MedicineContext _context = new MedicineContext();
        Logger logger = new Logger();

        //Database'e yeni ilaç bilgileri eklemek için komutlar.
        public bool AddModel(Medicine _medicine)
        {
            try
            {
                _context.Medicine.Add(_medicine);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }

        //Database'deki ilaç bilgilerini Id'sine göre silmek için komutlar.
        public bool DeleteModel(int MedicineId)
        {
            try
            {
                _context.Medicine.Remove(FindMedicine("", "", MedicineId));
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }

        //Database'deki ilaç bilgilerinin tamamını getiren komutlar.
        public List<Medicine> GetMedicines()
        {
            List<Medicine> medicines = new List<Medicine>();
            medicines = _context.Medicine.ToList();
            InnerJoinExample();
            return medicines;
        }
        public Medicine FindMedicine(string Company, string MedicineName, int MedicineId = 0)
        {
            Medicine medicines = new Medicine();
            if(!string.IsNullOrEmpty(Company) && !string.IsNullOrEmpty(MedicineName))
            {
                medicines = _context.Medicine.FirstOrDefault(x => x.Company == Company && x.MedicineName == MedicineName);
            }
            
            else if (MedicineId > 0)
            {
                medicines = _context.Medicine.FirstOrDefault(x => x.MedicineId == MedicineId);

            }
            return medicines;
        }

        //İki tablodaki birer değeri birleştirmek için
        public void InnerJoinExample()
        {
            //List<Medicine> medicines = new List<Medicine>();
            //medicines = _context.Medicine.Where(m => m.Stock >= 20).ToList();
            MedicineDetail? medicine = _context.Medicine.Join(_context.Features, a => a.FeatureId,
                u => u.Id,
                (medicine, feature) => new MedicineDetail{ MedicineName = medicine.MedicineName, Usage_Information = feature.Usage_Information }).FirstOrDefault();
        }
        public void CreateLogin(APIAuthority loginUser)
        {
            _context.APIAuthority.Add(loginUser);
            _context.SaveChanges();
        }
        public APIAuthority GetLogin(APIAuthority loginUser)
        {
            APIAuthority? user = new APIAuthority();
            if (!string.IsNullOrEmpty(loginUser.UserName) && !string.IsNullOrEmpty(loginUser.Password))
            {
                user = _context.APIAuthority.FirstOrDefault(m => m.UserName == loginUser.UserName && m.Password == loginUser.Password);
            }

            return user;

        }
    }
}
