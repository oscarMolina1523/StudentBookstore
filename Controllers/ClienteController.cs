using Microsoft.AspNetCore.Mvc;
using IntegrationFirebaseApi.Services;
using IntegrationFirebaseApi.Models.Dtos;
using IntegrationFirebaseApi.Models.Entity;

namespace IntegrationFirebaseApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ClienteController : ControllerBase
  {
    private readonly FirestoreService<Cliente> _firestoreService;

    public ClienteController(){
      _firestoreService=new FirestoreService<Cliente>("Cliente");
    }
      [HttpGet]
      public async Task<IActionResult> GetData()
      {
        try{
          var clientes = await _firestoreService.GetAllDocuments();
           if (clientes == null || clientes.Count == 0)
            {
              return NotFound("No se encontraron clientes.");
            }
          return Ok(clientes);

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al obtener la lista de clientes");
        }
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetClienteById([FromRoute] string id){
        try{
          var cliente=await _firestoreService.GetDocumentById(id);
          return Ok(cliente);
        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al obtener la cliente");
        }
      }

      [HttpPost]
      public async Task<IActionResult> CreateCliente([FromBody] ClienteDto clienteDto){
        Cliente cliente = new Cliente
        {
          Nombres=clienteDto.Nombres,
          Cedula=clienteDto.Cedula,
          Telefono=clienteDto.Telefono,
        };
        Console.WriteLine(cliente);

        try{

          await _firestoreService.CreateDocument(cliente);
          return StatusCode(201,"cliente creado correctamente ");

        }catch(Exception ex){

          Console.WriteLine(ex);
          return StatusCode(500, "Error al crear el categoria");

        }
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update([FromRoute] string id,[FromBody] ClienteDto clienteDto){
        try{
          var updates = new Dictionary<string, object>
            {
              { "Nombres", clienteDto.Nombres},
              { "Cedula", clienteDto.Cedula },
              {"Telefono", clienteDto.Telefono}
            };
          await _firestoreService.UpdateDocument(id, updates);
          return Ok("Cliente actualizado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al actualizar el cliente");
        }
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteData(string id){
        try{
          await _firestoreService.DeleteDocument(id);
          return Ok("el cliente fue eliminado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al eliminar el cliente");
        }
      }
  }

}
