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
    public class categoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepositor;
        private readonly IBaseValida _baseValida;


        public categoriaController(ICategoriaRepository categoriaRepositor, IBaseValida baseValida)
        {
            _categoriaRepositor = categoriaRepositor;
            _baseValida = baseValida;
        }

        // GET: api/categoria
        [HttpGet("/categorias")]
        public IEnumerable<CategoriaEntity> GetAll()
        {
            return _categoriaRepositor.GetAll();
        }

        // GET: api/categoria/5
        [HttpGet("/Buscacategoria/{id}")]
        public ActionResult<CategoriaEntity> Get(int id)
        {
            var categoriaExistente = _categoriaRepositor.Get(id);

            if (categoriaExistente != null)
            {
                return new OkObjectResult(categoriaExistente);
            }
            else
            {
                return new BadRequestObjectResult("Nao existe esse id!");
            }
        }

        // POST: api/categoria
        [HttpPost("/Cadastro_categoria/")]
        public ActionResult<CategoriaEntity> Post([FromBody] CategoriaEntity categoria)
        {
            if (!ValidaCategoria(categoria))
            {                
                categoria.nome = categoria.nome.Trim(' ');
                return new OkObjectResult(_categoriaRepositor.Add(categoria));
            }
            else
            {
                return new BadRequestObjectResult("Nome inválido!");
            }
        }

        // PUT: api/categoria/5
        [HttpPut("/Update_categoria/")]
        public ActionResult<CategoriaEntity> Put([FromBody] CategoriaEntity categoria)
        {
            try
            {
                if (!ValidaCategoria(categoria))
                {
                    var categoriaEntity = new CategoriaEntity()
                    {
                        cod_categoria = categoria.cod_categoria,
                        nome = categoria.nome.Trim(' ')
                    };
                    var Updatecategoria = _categoriaRepositor.Update(categoriaEntity);
                    return new OkObjectResult(categoriaEntity);
                }
                else
                {
                    return new BadRequestObjectResult("Erro no Update");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("/Delete_categoria/{id}")]
        public ActionResult Delete(int id)
        {
            var existe = _categoriaRepositor.Get(id);

            if (existe != null)
            {
                var podeExcluir = _categoriaRepositor.PodeExcluir(id);
                if (podeExcluir)
                {
                    _categoriaRepositor.Delete(id);
                    return new OkResult();
                }
                
            }
            return new NotFoundResult();
        }

        private bool ValidaCategoria(CategoriaEntity categoria)
        {
            int cont = 0;
            if (_baseValida.ValidaCampoNull(categoria.nome)
                || _baseValida.ValidaString(categoria.nome))
            {
                //categoria.nome = _baseValida.ValidaEspaco(categoria.nome);
                cont++;
            }

            if (categoria.cod_categoria != 0)
            {
                var existe = _categoriaRepositor.GetNoTracking(categoria.cod_categoria);
                if (existe == null)
                {
                    cont++;
                }
            }

            if (categoria.nome != null)
            {
                var unique = _categoriaRepositor.GetCategoriaByName(categoria.nome);
                if (unique != null)
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
    }
}
