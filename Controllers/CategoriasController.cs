using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }






        //[HttpGet("produtos")]
        //public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        //{
        //    return _repository.GetCategorias().Include(p=>p.Produtos).AsNoTracking().ToList();
        //}




        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categirias = _repository.GetCategorias();        
            
            return Ok (categirias);
        }






        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _repository.GetCategoria(id);

            if (categoria == null)
            {
                return NotFound($"Categoria com id= {id} não encontrada...");
            }

            return Ok(categoria);
        }








        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados inválidos");
            }

            var categoriaCriada = _repository.Create(categoria);

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }








        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _repository.Update(categoria);

            return Ok(categoria);
        }








        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _repository.GetCategoria(id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada...");
            }

            var categoriaExcluida = _repository.Delete(id);

            return Ok(categoriaExcluida);
        }

    }
}
