using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VsShopper_Infra.DTO;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace vsSHOPPER1.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class OrcamentoController : ControllerBase
    {

        private readonly IOrcamentoRepository _orcamentoRepository;

        public OrcamentoController(IOrcamentoRepository orcamentoRepository)
        {
            _orcamentoRepository = orcamentoRepository;

        }

        // GET: api/ListaOrcamento
        [HttpGet("Busca_Todos_ListaCompras")]
        public IEnumerable<OrcamentoEntity> GetAll()
        {
            return _orcamentoRepository.GetAll();
        }

        [HttpGet("Busca_PorCompra")]
        public IEnumerable<OrcamentoEntity> FinbyCompra(int id)
        {
            return _orcamentoRepository.FindByCompra(id);
        }

        // GET: api/ListaOrcamento/5
        [HttpGet("Busca_Por_ID{id}")]
        public OrcamentoEntity Get(int id)
        {
            var orcamento = _orcamentoRepository.Get(id);
            return orcamento;
        }

        // POST: api/ListaOrcamento
        [HttpPost("Cadastro_Orcamento")]
        public OrcamentoEntity Post([FromBody] OrcamentoEntity Orcamento)
        {

            var NewOrcamento = _orcamentoRepository.Add(Orcamento);
            return NewOrcamento;
        }

        // PUT: api/ListaOrcamento/5
        [HttpPut("Update_Orcamento")]
        public OrcamentoEntity Put([FromBody] OrcamentoEntity Orcamento)
        {
            try
            {
                var UpdateOrcamento = _orcamentoRepository.Update(Orcamento);
                return UpdateOrcamento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Deleta_Orcamento{id}")]
        public void Delete(int id)
        {
            _orcamentoRepository.Delete(id);
        }
        }
    }
