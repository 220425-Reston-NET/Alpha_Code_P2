using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Medicine{
    private int _medID;
    public int medID
    {
        get { return _medID; }
        set
        {
            if (value > 0)
            {
                _medID = value;
            }
            else
            {
                throw new ValidationException("medID needs to be above 0.");

            }
        }
    }
    private string _medName;
    public string medName
    {
        get { return _medName; }
        set
        {

            if (Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
            {
                _medName = value;
            }
            else
            {
                throw new ValidationException("Can only have letters");
            }
        }
    }
    private int _medDose;
    public int medDose{
        get { return _medDose; }
        set
        {
            if (value > 0)
            {
                _medDose = value;
            }
            else
            {
                throw new ValidationException("Dose needs to be above 0.");

            }
        }
    }

    private int _quantity;
    public int Quantity 
    {
        get { return _quantity; }
        set
        {

            if (value > 0)
            {
                _quantity = value;
            }
            else
            {
                throw new ValidationException("Quantity needs to be above 0.");
            }
        }
    }

    public override string ToString()
    {
        return $"=======\nID: {medID}\nName: {medName}\nDose: {medDose}\nQuantity: {Quantity}\n=======";
    }
}