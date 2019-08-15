using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace vsSHOPPER1.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        public readonly IPerfilRepository _perfilRepository;
        public readonly IBaseValida _baseValida;

        public PerfilController(IPerfilRepository perfilRepository, IBaseValida baseValida)
        {
            _perfilRepository = perfilRepository;
            _baseValida = baseValida;
        }
        // GET: api/Perfil
        [HttpGet("Busca_Todos_Perfil")]
        public IEnumerable<PerfilEntity> Get()
        {
            return _perfilRepository.GetAll();
        }

        // GET: api/Perfil/5
        [HttpGet("Busca_Perfil/{id}")]
        public  ActionResult<PerfilEntity> Get(int id)
        {
            var TestExiste = _perfilRepository.GetNoTracking(id);
            if (TestExiste != null)
            {
                return new OkObjectResult(_perfilRepository.Get(id));
            }
            else
                return new BadRequestObjectResult("Nao Existe Esse perfil");
        }

        // POST: api/Perfil
        [HttpPost("Cadastro_perfil")]
        public ActionResult<PerfilEntity> Post([FromBody] PerfilEntity perfilEntity)
        {
            try
            {
                perfilEntity.nome = perfilEntity.nome.Trim(' ');
                if (ValidaPerfil(perfilEntity))
                {
                    return new BadRequestObjectResult("Erro Cadastro Perfil");
                }
                else
                    return new OkObjectResult(_perfilRepository.Add(perfilEntity));
            }
            catch (Exception)
            {
                throw ;
            }
        }
        
        private bool ValidaPerfil(PerfilEntity perfilEntity)
        {
            if (_baseValida.ValidaString(perfilEntity.nome) || _baseValida.ValidaCampoNull(perfilEntity.nome))
                return true; 
            return false;
        }

        // PUT: api/Perfil/5
        [HttpPut("Update_Perfil")]
        public ActionResult<PerfilEntity> Put([FromBody] PerfilEntity perfilEntity)
        {
            try
            {
                perfilEntity.nome = perfilEntity.nome.Trim(' ');
                var TesteExiste = _perfilRepository.GetNoTracking(perfilEntity.cod_perfil);
                if (TesteExiste != null)
                {
                    if (ValidaPerfil(perfilEntity))
                    {
                        return new BadRequestObjectResult("Erro Update Perfil");
                    }
                    else
                      return new OkObjectResult(_perfilRepository.Update(perfilEntity));
                }
                else
                {
                    return new BadRequestObjectResult("Nao Existe esse perfil");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Delete_Perfil/{id}")]
        public ActionResult Delete(int id)
        {
            var existe = _perfilRepository.Get(id);
            if (existe != null)
            {
                var podeExcluir = _perfilRepository.PodeExcluir(id);
                if (podeExcluir)
                {
                    _perfilRepository.Delete(id);
                }
                else
                    return new BadRequestObjectResult("Nao Pode Excluir esse perfil");
            }
            else
                return new BadRequestObjectResult("Nao Existe esse codigo de Perfil");
            return new OkResult();
        }
    }
}
