namespace MedTrakModel;
public class User
{
    private int _userID;
    public int userID
    {
        get { return _userID; }
        set
        {
            if (value > 0)
            {
                _userID = value;
            }
            else
            {
                throw new ValidationException("userID needs to be above 0.");

            }
        }
    }
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {

            if (Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
            {
                _name = value;
            }
            else
            {
                throw new ValidationException("Can only have letters");
            }
        }
    }
    public string Address { get; set; }

    public string Email { get; set; }
    public List<Medicine> Medicine { get; set; }
    public Customer()
    {
        userID = 1;
        Name = "Daniel Pagan";
        Address = "1234 farway";
        Email = "dannyrand@gmail.com ";
        Medicine = new List<Medicine>();
    }



    public override string ToString()
    {



        return $"===Customer info===/ncustomerID: {userID}Name: {Name}/nAddress: {Address}/nEmail: {Email}/nMedicine: {Medicine}/n==========================";

    }

}


