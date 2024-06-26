﻿
using FlavorCart.Models;
using FlavorCart.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Firestore.Controllers;
[ApiController]
[Route("[controller]")]
public class UserFirestoreController : ControllerBase
{
   

    private readonly ILogger<UserFirestoreController> _logger;
    private readonly UserRepository _userRepository = new();
    private ListsRepository _listsRepository = new();
    private UserTokenController _usertokenFirestoreController;

    public UserFirestoreController(ILogger<UserFirestoreController> logger)
    {
        _logger = logger;
        _usertokenFirestoreController = new UserTokenController(_logger);
    }


    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult<User>> GetUserAsync(string email)
    {
       try
        {
            //Before returning the data, we need to verify the token
            var ok = await _usertokenFirestoreController.Verify(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (ok != null)

            { 
                var user = new User()
                {
                    Email = email
                };

                return Ok(await _userRepository.GetUserByEmailAsync(user));
            
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Missing token");
        }
        return BadRequest("error");
           
    }

    [HttpPut]
    [Route("{email}")]
    public async Task<ActionResult<User>> UpdateUserAsync(string email, User user)
    {
        if (email != user.Email)
        {
            return BadRequest("Email must match.");
        }

        return Ok(await _userRepository.UpdateByEmailAsync(user));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteUserAsync(string id, [FromQuery] bool eliminarTodasLasListas)
    {
       User user = new User()
       {
            Id = id
        };

        //Delete all lists from user
        await _listsRepository.DeleteListByUser(id, eliminarTodasLasListas);
        //Delete user
        await _userRepository.DeleteAsync(user);
        
        return Ok("Deleted");
    }


    [HttpPost]
    public async Task<ActionResult<User>> AddUserAsync(User user)
    {
        return Ok(await _userRepository.AddAsync(user));
    }
  

}