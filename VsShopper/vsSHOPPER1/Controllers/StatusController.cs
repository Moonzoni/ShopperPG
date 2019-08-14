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
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IBaseValida _baseValida;

        public StatusController(IStatusRepository statusRepository, IBaseValida baseValida)
        {
            _statusRepository = statusRepository;
            _baseValida = baseValida;
        }

        // GET: api/Status
        [HttpGet("/Busca_Todos_Status")]
        public IEnumerable<StatusEntity> Get()
        {
            return _statusRepository.GetAll();
        }

        // GET: api/Status/5
        [HttpGet("/Busca_Status/{id}")]
        public ActionResult<StatusEntity> Get(int id)
        {
            var statusExiste = _statusRepository.Get(id);
            if (statusExiste != null)
            {
                return new OkObjectResult(statusExiste);
            }else 
                return new BadRequestObjectResult("Nao existe esse ID");
        }

        // POST: api/Status
        [HttpPost("/Cadastro_Status/")]
        public ActionResult<StatusEntity> Post([FromBody] StatusEntity statusEntity)
        {
            statusEntity.NOME = statusEntity.NOME.Trim(' ');
            if (ValidacaoStatus(statusEntity))
            {
                return new BadRequestObjectResult("Campo Null");
            }else
                return new OkObjectResult(_statusRepository.Add(statusEntity));
        }

        private bool ValidacaoStatus(StatusEntity statusEntity)
        {
            //statusEntity.NOME = statusEntity.NOME.Trim(' ');
            if (_baseValida.ValidaCampoNull(statusEntity.NOME))
            {
                return true;
            }
            return false;
        }
        // PUT: api/Status/5
        [HttpPut("/Update_Status/")]
        public ActionResult<StatusEntity> Put([FromBody] StatusEntity statusEntity)
        {
            try
            {
                statusEntity.NOME = statusEntity.NOME.Trim(' ');
                if (ValidacaoStatus(statusEntity))
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
        [HttpDelete("/Delete_Status/{id}")]
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
                    return new BadRequestObjectResult("Nao Pode Excluir Esse Status");
            }
            else
                return new BadRequestObjectResult("NAO EXISTE ESSE CODIGO");
         return new OkResult();
        }
    }
}
