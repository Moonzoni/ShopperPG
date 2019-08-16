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
        private readonly IStatusRepository _statusRepository;
        private readonly IBaseValida _baseValida;

        public statusController(IStatusRepository statusRepository, IBaseValida baseValida)
        {
            _statusRepository = statusRepository;
            _baseValida = baseValida;
        }

        // GET: api/status
        [HttpGet("/Busca_Todos_status")]
        public IEnumerable<StatusEntity> Get()
        {
            return _statusRepository.GetAll();
        }

        // GET: api/status/5
        [HttpGet("/Busca_status/{id}")]
        public ActionResult<StatusEntity> Get(int id)
        {
            var statusExiste = _statusRepository.Get(id);
            if (statusExiste != null)
            {
                return new OkObjectResult(statusExiste);
            }
            else
                return new BadRequestObjectResult("Nao existe esse ID");
        }

        // POST: api/status
        [HttpPost("/Cadastro_status/")]
        public ActionResult<StatusEntity> Post([FromBody] StatusEntity statusEntity)
        {
            if (!ValidaStatus(statusEntity))
            {
                statusEntity.nome = statusEntity.nome.Trim(' ');
                return new OkObjectResult(_statusRepository.Add(statusEntity));
            }
            else
            {
                return new BadRequestObjectResult("Nome inválido!");
            }
        }

        // PUT: api/status/5
        [HttpPut("/Update_status/")]
        public ActionResult<StatusEntity> Put([FromBody] StatusEntity statusEntity)
        {
            try
            {
                
                if (!ValidaStatus(statusEntity))
                {
                    statusEntity.nome = statusEntity.nome.Trim(' ');
                    return new OkObjectResult(_statusRepository.Update(statusEntity));
                }
                else
                {
                    return new BadRequestObjectResult("Erro no update!");
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e);
            }
        }

        private bool ValidaStatus(StatusEntity status)
        {
            int cont = 0;
            if (_baseValida.ValidaCampoNull(status.nome)
                || _baseValida.ValidaString(status.nome))
            {
                //statusEntity.nome = statusEntity.nome.Trim(' ');
                return true;
            }
            if (status.cod_status != 0)
            {
                var existe = _statusRepository.GetNoTracking(status.cod_status);
                if (existe == null)
                {
                    cont++;
                }
            }

            if (status.nome != null)
            {
                var unique = _statusRepository.GetStatusByName(status.nome);
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
                {
                    return new BadRequestObjectResult("Nao Pode Excluir Esse status");
                }
            }
            else
            {
                return new BadRequestObjectResult("NAO EXISTE ESSE CODIGO");
            }

            return new OkResult();
        }
    }
}
