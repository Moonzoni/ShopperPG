using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;
using VsShopper_Infra.Repository;

namespace vsSHOPPER1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepositor;
        private readonly IBaseValida _baseValida;


        public CategoriaController(ICategoriaRepository categoriaRepositor, IBaseValida baseValida)
        {
            _categoriaRepositor = categoriaRepositor;
            _baseValida = baseValida;

        }

        // GET: api/Categoria
        [HttpGet("/Categorias")]
        public IEnumerable<CategoriaEntity> GetAll()
        {
            return _categoriaRepositor.GetAll();
        }

        // GET: api/Categoria/5
        [HttpGet("/BuscaCategoria/{id}")]
        public ActionResult<CategoriaEntity> Get(int id)
        {
            var CategoriaExistente = _categoriaRepositor.Get(id);
            if (CategoriaExistente != null)
            {
                return new OkObjectResult(CategoriaExistente);
            }
            else
                return new BadRequestObjectResult("Nao Existe esse ID");
        }

        // POST: api/Categoria
        [HttpPost("/Cadastro_Categoria/")]
        public ActionResult<CategoriaEntity> Post([FromBody] CategoriaEntity categoria)
        {
            if (!ValidaCategoria(categoria))
            {
                return new OkObjectResult(_categoriaRepositor.Add(categoria));
            }
            else
                return new BadRequestObjectResult("Erro");
        }

        private bool ValidaCategoria(CategoriaEntity categoria)
        {
            int cont = 0;
            if (_baseValida.ValidaCampoNull(categoria.NOME)
                || _baseValida.ValidaNome(categoria.NOME))
            {
                cont++;
            }

            if (categoria.COD_CATEGORIA != 0)
            {
                var existe = _categoriaRepositor.GetNoTracking(categoria.COD_CATEGORIA);
                if (existe == null)
                {
                    cont++;
                }
            }

            if (cont > 0)
            {
                return true;
            }
            return false;
        }

        // PUT: api/Categoria/5
        [HttpPut("/Update_Categoria/")]
        public ActionResult<CategoriaEntity> Put([FromBody] CategoriaEntity categoria)
        {
            try
            {
                if (!ValidaCategoria(categoria))
                {
                    var categoriaEntity = new CategoriaEntity()
                    {
                        COD_CATEGORIA = categoria.COD_CATEGORIA,
                        NOME = categoria.NOME
                    };
                    var UpdateCategoria = _categoriaRepositor.Update(categoriaEntity);
                    return new OkObjectResult(categoria);
                
            }
                else
                return new BadRequestObjectResult("Erro Update");
        }

        catch (Exception)
            {
                throw;
            }
}

    // DELETE: api/ApiWithActions/5
    [HttpDelete("/Delete_Categoria/{id}")]
    public void Delete(int id)
    {

        var existe = _categoriaRepositor.Get(id);
        if (existe != null)
        {
            var podeExcluir = _categoriaRepositor.PodeExcluir(id);
            if (podeExcluir)
            {
                _categoriaRepositor.Delete(id);
            }
        }

    }
}
}
