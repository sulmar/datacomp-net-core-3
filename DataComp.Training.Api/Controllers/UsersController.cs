using DataComp.Training.IServices;
using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Controllers
{

    // WebAPI -> REST API 

    [Route("api/users")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
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
        public IActionResult GetById(Guid id)
        {
            var user = userService.Get(id);

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
            var user = userService.Get(pesel);

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        // GET api/users?FullName=Jacek&IsRemoved=false

        [HttpGet]
        public IActionResult Get([FromQuery] UserSearchCriteria searchCriteria)
        {
            var users = userService.Get(searchCriteria);

            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            userService.Add(user);

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
        public IActionResult Delete(Guid id)
        {
            var user = userService.Get(id);

            if (user == null)
                return NotFound();

            userService.Remove(id);

            return NoContent();
        }


    }

}
  