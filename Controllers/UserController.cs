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
    private readonly FirestoreService<UserEntity> _firestoreService;

    public UserController(){
      _firestoreService=new FirestoreService<UserEntity>("User");
    }
      [HttpGet]
      public async Task<IActionResult> GetData()
      {
        try{
          var users = await _firestoreService.GetAllDocuments();
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
          var user=await _firestoreService.GetDocumentById(id);
          return Ok(user);
        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al obtener el usuario");
        }
      }

      [HttpPost]
      public async Task<IActionResult> CreateUser([FromBody] UserDto userDto){
        string id =Guid.NewGuid().ToString();
        UserEntity user = new UserEntity
        {
          Id= id,
          FullName=userDto.FullName,
          Email=userDto.Email,
        };
        Console.WriteLine(user);

        try{

          await _firestoreService.CreateDocument(user);
          return StatusCode(201,"usuario creado correctamente ");

        }catch(Exception ex){

          Console.WriteLine(ex);
          return StatusCode(500, "Error al crear el usuario");

        }
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update([FromRoute] string id,[FromBody] UserDto userDto){
        try{
          var updates = new Dictionary<string, object>
            {
              { "FullName", userDto.FullName },
              { "Email", userDto.Email }
            };
          await _firestoreService.UpdateDocument(id, updates);
          return Ok("Usuario actualizado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al actualizar el usuario");
        }
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteData(string id){
        try{
          await _firestoreService.DeleteDocument(id);
          return Ok("el usuario fue eliminado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al eliminar el usuario");
        }
      }
  }

}
