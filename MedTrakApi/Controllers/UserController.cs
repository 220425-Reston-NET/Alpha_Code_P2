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
    public IActionResult ReplenishQuantity([FromBody] User p_user, Medicine m_med)
    {
        User found = _userBL.SearchUserByEmail(p_user.Email);

        if (found == null)
        {
            return NotFound("Store was not found!");
        }

        else
        {
            try
            {
                 _medicineBL.ReplenishMedicineQuantity(m_med.medID, p_user.userID, m_med.Quantity);

                return Ok();
            }
            catch (SqlException)
            {
                return Conflict();
            }
        }
    }

    [HttpGet("SearchCustomerByName")]
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

    [HttpGet("GetAllCustomers")]
    public IActionResult GetAllCustomers()
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