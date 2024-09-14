using Microsoft.AspNetCore.Mvc;
using IntegrationFirebaseApi.Services;
using IntegrationFirebaseApi.Models.Dtos;
using IntegrationFirebaseApi.Models.Entity;

namespace IntegrationFirebaseApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriaController : ControllerBase
  {
    private readonly FirestoreService<Categoria> _firestoreService;

    public CategoriaController(){
      _firestoreService=new FirestoreService<Categoria>("Categoria");
    }
      [HttpGet]
      public async Task<IActionResult> GetData()
      {
        try{
          var categories = await _firestoreService.GetAllDocuments();
           if (categories == null || categories.Count == 0)
            {
              return NotFound("No se encontraron categorias.");
            }
          return Ok(categories);

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al obtener la lista de categorias");
        }
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetCategoriaById([FromRoute] string id){
        try{
          var categoria=await _firestoreService.GetDocumentById(id);
          return Ok(categoria);
        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al obtener la categoria");
        }
      }

      [HttpPost]
      public async Task<IActionResult> CreateCategoria([FromBody] CategoriaDto categoriaDto){
        Categoria categoria = new Categoria
        {
          Descripcion=categoriaDto.Descripcion,
          Estado=true,
        };
        Console.WriteLine(categoria);

        try{

          await _firestoreService.CreateDocument(categoria);
          return StatusCode(201,"categoria creado correctamente ");

        }catch(Exception ex){

          Console.WriteLine(ex);
          return StatusCode(500, "Error al crear el categoria");

        }
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update([FromRoute] string id,[FromBody] CategoriaDto categoriaDto){
        try{
          var updates = new Dictionary<string, object>
            {
              { "Descripcion", categoriaDto.Descripcion},
              { "Estado", categoriaDto.Estado }
            };
          await _firestoreService.UpdateDocument(id, updates);
          return Ok("Categoria actualizado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al actualizar el categoria");
        }
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteData(string id){
        try{
          await _firestoreService.DeleteDocument(id);
          return Ok("el categoria fue eliminado de manera correcta");

        }catch(Exception ex){
          Console.WriteLine(ex);
          return StatusCode(500, "Error al eliminar el categoria");
        }
      }
  }

}
