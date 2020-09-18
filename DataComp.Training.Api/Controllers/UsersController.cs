using DataComp.Training.Api.Events;
using DataComp.Training.Api.Filters;
using DataComp.Training.Api.Models;
using DataComp.Training.Api.Requests;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Controllers
{

    // WebAPI -> REST API 

    [Authorize(Roles = "Trainer, Developer")]
    [Route("api/users")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUserService userService;

        public UsersController(IUserService userService, IMediator mediator)
        {
            this.userService = userService;
            this.mediator = mediator;
        }

        // GET api/users
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var users = userService.Get();

        //    return Ok(users);
        //}

        // GET api/users/a747b564-3109-1fb7-5469-8343d21a8289
        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(Guid id)
        {
            // var user = userService.Get(id);

            var user = await mediator.Send(new GetUserRequest(id));

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Route constraint reference
        // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/routing?view=aspnetcore-3.1#route-constraint-reference

        // GET api/users/90565545454

        [HttpGet("{pesel:length(13)}")]
        public IActionResult Get(string pesel)
        {
            if (User.IsInRole("Developer"))
            {

            }

            var user = userService.Get(pesel);

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        // GET api/users?FullName=Jacek&IsRemoved=false

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearchCriteria searchCriteria)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}

            string user = this.User.FindFirst("User").Value;

            var users = userService.Get(searchCriteria);

            return Ok(users);
        }

        [HttpPost]
        [Authorize(Policy = "AtLeast18")]
        public async Task<IActionResult> Post([FromBody] User user, [FromServices] IMessageService messageService) // wstrzykiwanie bezpośrednio do metody zamiast poprzez konstruktor
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = this.User.FindAll("Stanowisko");

            foreach (var claim in claims)
            {
                
            }


            user = await mediator.Send(new AddUserRequest(user));

            //userService.Add(user);

            //messageService.Send($"Dodano użytkownika {user.FullName} id = {user.Id}");

            // zła praktyka
            // return Created($"http://localhost:5000/api/users/{user.Id}", user);

            // dobra praktyka
            return CreatedAtRoute(nameof(GetById), new { Id = user.Id }, user);
        }


        // PUT /api/users/10
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            userService.Update(user);

            // return NoContent();

            return Ok(user);
        }

        // PATCH  /api/users/10
        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            // http://jsonpatch.com/

            // userService.Update(user);

            // return NoContent();

            return Ok(user);
        }

        // DELETE /api/users/10
        [HttpDelete("{id}")]
        [UserExists]
        public IActionResult Delete(Guid id)
        {
            //var user = userService.Get(id);

            //if (user == null)
            //    return NotFound();

            //userService.Remove(id);
            //messageService.Send($"Usunięto użytkownika {user.FullName}");

           // mediator.Publish(new UserRemovedEvent(user));

            return NoContent();
        }


        // POST /api/users/upload
        //[HttpPost("upload")]
        //public async Task<IActionResult> Upload([FromBody] ICollection<User> users)
        //{
        //    foreach (var user in users)
        //    {
        //        await Task.Delay(TimeSpan.FromMinutes(5));
        //    }

        //    return Ok();
        //}

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] ICollection<User> users)
        {
            //foreach (var user in users)
            //{
            //    await Task.Delay(TimeSpan.FromMinutes(5));
            //}

            return Accepted();
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult GenerateToken(
            [FromServices] IAuthenticateService authenticateService,
            [FromServices] ITokenService tokenService,
            [FromForm] UserParam userParam)
        {
            if (authenticateService.TryAuthenticate(userParam.Username, userParam.HashedPassword, out User user))
            {
                string token = tokenService.CreateToken(user);

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }


    }

}
  