using Microsoft.AspNetCore.Mvc;
using IntegrationFirebaseApi.Services;
using IntegrationFirebaseApi.Models.Dtos;
using IntegrationFirebaseApi.Models.Entity;

namespace IntegrationFirebaseApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly FirestoreService _firestoreService;

    public UserController(FirestoreService firestoreService){
      _firestoreService=firestoreService;
    }
      [HttpGet]
      public async Task<IActionResult> GetData()
      {
        try{
          var users = await _firestoreService.GetAllUsers();
           if (users == null || users.Count == 0)
            {
              return NotFound("No se encontraron usuarios.");
            }
          return Ok(users);

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al obtener la lista de usuario");
        }
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetUserById([FromRoute] string id){
        try{
          var user=await _firestoreService.GetUserById(id);
          return Ok(user);
        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al obtener el usuario");
        }
      }

      [HttpPost]
      public async Task<IActionResult> CreateUser([FromBody] UserDto userDto){

        UserEntity user = new UserEntity
        {
          FullName=userDto.FullName,
          Email=userDto.Email,
        };

        try{

          await _firestoreService.CreateUser(user);
          return StatusCode(201,"usuario creado correctamente ");

        }catch(Exception ex){

          Console.WriteLine(ex);
          return StatusCode(500, "Error al crear el usuario");

        }
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update([FromRoute] string id,[FromBody] UserDto userDto){
        try{
          await _firestoreService.UpdateUser(id, userDto);
          return Ok("Usuario actualizado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al actualizar el usuario");
        }
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteData(string id){
        try{
          await _firestoreService.DeleteUser(id);
          return Ok("el usuario fue eliminado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al eliminar el usuario");
        }
      }
  }

}
