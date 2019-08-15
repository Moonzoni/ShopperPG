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
    [Route("api/[controller]")]
    [ApiController]
    public class statusController : ControllerBase
    {
        private readonly IstatusRepository _statusRepository;
        private readonly IBaseValida _baseValida;

        public statusController(IstatusRepository statusRepository, IBaseValida baseValida)
        {
            _statusRepository = statusRepository;
            _baseValida = baseValida;
        }

        // GET: api/status
        [HttpGet("/Busca_Todos_status")]
        public IEnumerable<statusEntity> Get()
        {
            return _statusRepository.GetAll();
        }

        // GET: api/status/5
        [HttpGet("/Busca_status/{id}")]
        public ActionResult<statusEntity> Get(int id)
        {
            var statusExiste = _statusRepository.Get(id);
            if (statusExiste != null)
            {
                return new OkObjectResult(statusExiste);
            }else 
                return new BadRequestObjectResult("Nao existe esse ID");
        }

        // POST: api/status
        [HttpPost("/Cadastro_status/")]
        public ActionResult<statusEntity> Post([FromBody] statusEntity statusEntity)
        {
            statusEntity.nome = statusEntity.nome.Trim(' ');
            if (Validacaostatus(statusEntity))
            {
                return new BadRequestObjectResult("Campo Null");
            }else
                return new OkObjectResult(_statusRepository.Add(statusEntity));
        }

        private bool Validacaostatus(statusEntity statusEntity)
        {
            //statusEntity.nome = statusEntity.nome.Trim(' ');
            if (_baseValida.ValidaCampoNull(statusEntity.nome))
            {
                return true;
            }
            return false;
        }
        // PUT: api/status/5
        [HttpPut("/Update_status/")]
        public ActionResult<statusEntity> Put([FromBody] statusEntity statusEntity)
        {
            try
            {
                statusEntity.nome = statusEntity.nome.Trim(' ');
                if (Validacaostatus(statusEntity))
                {
                    return new OkObjectResult(_statusRepository.Update(statusEntity));
                }
                else
                    return new BadRequestObjectResult("Erro no Update");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("/Delete_status/{id}")]
        public ActionResult Delete(int id)
        {
            var existe = _statusRepository.Get(id);
            if (existe != null)
            {
                var podeExcluir = _statusRepository.PodeExcluir(id);
                if (podeExcluir)
                {
                    _statusRepository.Delete(id);
                }
                else
                    return new BadRequestObjectResult("Nao Pode Excluir Esse status");
            }
            else
                return new BadRequestObjectResult("NAO EXISTE ESSE CODIGO");
         return new OkResult();
        }
    }
}
