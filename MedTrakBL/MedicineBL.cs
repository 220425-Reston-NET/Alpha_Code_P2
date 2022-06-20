using MedTrakDL;
using MedTrakModel;

namespace MedTrakBL
{
    public class MedicineBL : IMedicineBL
    {
        // ================================================
        private readonly IRepository<Medicine> _medRepo;
        public MedicineBL(IRepository<Medicine> p_medRepo)
        {
            _medRepo = p_medRepo;
        }
        // ================================================

        public void AddMedicine(Medicine m_med)
        {
            _medRepo.Add(m_med);
        }

        public List<Medicine> GetAllMedicine()
        {
            return _medRepo.GetAll();
        }

        public void ReplenishMedicineQuantity(int p_medID, int p_medDose, int p_Quantity)
        {
            Medicine medTable = new Medicine();
            medTable.medID = p_medID;
            medTable.medDose = p_medDose;
            medTable.Quantity = p_Quantity;

            _medRepo.Update(medTable);
        }

        public Medicine SearchMedicineByName(string p_medName)
        {
            return _medRepo.GetAll().First(Medicine => Medicine.medName == p_medName);
        }
    }
}