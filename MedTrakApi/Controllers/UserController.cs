using MedTrakBL;
using MedTrakModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace MedTrakApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase{
    private readonly IUserBL _userBL;
    private readonly IMedicineBL _medicineBL;
    public UserController(IUserBL p_userBL, IMedicineBL p_medicineBL){
        _userBL = p_userBL;
        _medicineBL = p_medicineBL;
    }


    [HttpPost("AddUser")]
    public IActionResult AddUser([FromBody] User u_use)
    {
        try
        {
            _userBL.AddUser(u_use);
            return Created("Customer was added!", u_use);
        }
        catch (SqlException)
        {
            return Conflict();
        }
    }

    [HttpGet("ViewAllMedicine")]
    public IActionResult ViewAllMedicine([FromQuery] int p_userID)
    {
        try
        {
            return Ok(_userBL.ViewAllMedicine(p_userID));
        }
        catch (SqlException)
        {
            return NotFound("No Medicines were found.");
        }
    }

    [HttpPost("AddMedicine")]
    public IActionResult AddMedicine([FromBody] Medicine m_med)
    {
        try
        {
            _medicineBL.AddMedicine(m_med);
            return Created("Medicine was added!", m_med);
        }
        catch (SqlException)
        {
            return Conflict();
        }
    }

    [HttpPut("ReplenishQuantity")]
    public IActionResult ReplenishQuantity([FromQuery] int medID, [FromQuery] int userID, [FromQuery] int Quantity, [FromQuery] string Email, [FromQuery] string Password )
    {
        User found = _userBL.SearchUserByEmailAndPassword(Email, Password);

        if (found == null)
        {
            return NotFound("Store was not found!");
        }

        else
        {
            try
            {
                 _medicineBL.ReplenishMedicineQuantity(medID, userID, Quantity);

                return Ok();
            }
            catch (SqlException)
            {
                return Conflict();
            }
        }
    }

    [HttpGet("SearchMedicineByName")]
    public IActionResult SearchMedicineByName([FromQuery] string medName)
    {
        try
        {
            return Ok(_medicineBL.SearchMedicineByName(medName));
        }
        catch (SqlException)
        {
            return Conflict();
        }
    }

    [HttpGet("SearchUserByName")]
    public IActionResult SearchUserByName([FromQuery] string userName)
    {
        try
        {
            return Ok(_userBL.SearchUserByName(userName));
        }
        catch (SqlException)
        {
            return Conflict();
        }
    }
    // Search customer here and return it

    [HttpGet("GetAllMedicine")]
    public IActionResult GetAllMedicine()
    {
        try
        {
            List<Medicine> listofCurrentMedicine = _medicineBL.GetAllMedicine();
            return Ok(listofCurrentMedicine);
        }
        catch (SqlException)
        {
            return NotFound("No Customer was found or exists.");
        }
    }
}