namespace MedTrakBL
{
    public interface IMedicineBL
    {
        void AddMedicine(Medicine m_med);
        Medicine SearchMedicineeByName(string p_medName);
        void ReplenishMedicineQuantity(int p_medID, int p_medDose, int p_Quantity);
        List<Medicine> GetAllMedicine();
    }
}