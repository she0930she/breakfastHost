using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("breakfasts")] // add common path

public class BreakfastController : ControllerBase
{
    // dependency injection
    // @Autowired
    private readonly IBreakfastService iBreakfastService;

    // constructor for controller
    public BreakfastController(IBreakfastService breakfastService){
        iBreakfastService = breakfastService;
    }


    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        // mapping the request data to fixed model
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        // save in DB
        // create breakfast, by using interface breakfast
        iBreakfastService.CreateBreakfast(breakfast);
        

        // response
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return CreatedAtAction(
            actionName: nameof(GetBreakfast), // GET method
            routeValues: new { id = breakfast.Id }, // pass GET method id
            value: response);
    }

    [HttpGet("{id:guid}")]
    // IActionResult: an interface, we can create a custom response as a return
    public IActionResult GetBreakfast(Guid id)
    {
        Breakfast breakfast = iBreakfastService.GetBreakfast(id);
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        iBreakfastService.UpsertBreakfast(breakfast);

        // TODO: return 201 created if a new breakfast was created 
        return Ok("updated successfully");
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        iBreakfastService.DeleteBreakfast(id);
        
        return Ok("deleted successfully");
    }
}