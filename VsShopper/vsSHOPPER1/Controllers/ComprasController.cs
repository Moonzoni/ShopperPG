using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VsShopper_Infra.DTO;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;
using VsShopper_Infra.Model;

namespace vsSHOPPER1.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly IComprasRepository _comprasRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IBaseValida _baseValida;

        public ComprasController(IComprasRepository compras, IStatusRepository status,
                                    IUsuarioRepository usuario, ICategoriaRepository categoria, 
                                    IOrcamentoRepository orcamento, IBaseValida baseValida)
        {
            _comprasRepository = compras;
            _statusRepository = status;
            _usuarioRepository = usuario;
            _categoriaRepository = categoria;
            _orcamentoRepository = orcamento;
            _baseValida = baseValida;
        }

        // GET: api/Compras
        [HttpGet("Busca_Todas_Compras")]
        public IEnumerable<ComprasDTO> Get()
        {
            return Find(_comprasRepository.GetAll());
        }

        [HttpGet("Busca_Compras_Status{idStatus}")]
        public IEnumerable<ComprasDTO> Busca_status(int idStatus)
        {
            var existe = _statusRepository.Get(idStatus);
            if (existe != null)
            {
                return Find(_comprasRepository.FindByStatus(idStatus));
            }
            return null;
        }

        [HttpGet("Busca_Compras_Categoria{Cod_Categoria}")]
        public IEnumerable<ComprasDTO> Busca_Categoria(int Cod_Categoria)
        {
            var existe = _categoriaRepository.Get(Cod_Categoria);
            if (existe != null)
            {
                return Find(_comprasRepository.FindByCategoria(Cod_Categoria));
            }
            return null;
        }

        [HttpGet("Busca_Compras_Titulo{Titulo}")]
        public IEnumerable<ComprasDTO> FindByTitulo(string Titulo)
        {
            return Find(_comprasRepository.FindByTitulo(Titulo));
        }

        [HttpGet("Busca_Compras_Descricao{Descricao}")]
        public IEnumerable<ComprasDTO> FindByDescricao(string Descricao)
        {
            return Find(_comprasRepository.FindByDescricao(Descricao));
        }

        [HttpGet("Busca_Compras_Aprovadas")]
        public IEnumerable<ComprasDTO> Busca_status_Aprovado()
        {
            var existe = _statusRepository.Get(2);
            if (existe != null)
                return Find(_comprasRepository.FindByStatus(2));

            return null;
        }
        [HttpGet("Busca_status_Finazilados")]
        public IEnumerable<ComprasDTO> Busca_status_Finazilados()
        {
            var existe = _statusRepository.Get(10);
            if (existe != null)
                return Find(_comprasRepository.FindByStatus(10));
            return null;
        }


        private IEnumerable<ComprasDTO> Find(IEnumerable<ComprasEntity> Listcompras)
        {
            return Listcompras.Select(x => new ComprasDTO()
            {
                STATUS = _statusRepository.Get(x.COD_STATUS),
                USUARIO = _usuarioRepository.Get(x.COD_USUARIO),
                CATEGORIA = _categoriaRepository.Get(x.COD_CATEGORIA),
                COD_COMPRAS = x.COD_COMPRAS,
                DESCRICAO = x.DESCRICAO,
                TITULO = x.TITULO,
                Data_Abertura = x.DATA_ABERTURA,
                Data_Finalizado = x.DATA_FINALIZACAO,
                OrcamentoDTOs = _orcamentoRepository.FindByCompra(x.COD_COMPRAS).ToList()
            });
        }

        // GET: api/Compras/5
        [HttpGet("Busca_Compras/{id}")]
        public ComprasDTO Get(int id)
        {
            var compras = _comprasRepository.Get(id);
            return new ComprasDTO()
            {
                STATUS = _statusRepository.Get(compras.COD_STATUS),
                USUARIO = _usuarioRepository.Get(compras.COD_USUARIO),
                CATEGORIA = _categoriaRepository.Get(compras.COD_CATEGORIA),
                COD_COMPRAS = compras.COD_COMPRAS,
                DESCRICAO = compras.DESCRICAO,
                TITULO = compras.TITULO,
                Data_Abertura = compras.DATA_ABERTURA,
                Data_Finalizado = compras.DATA_FINALIZACAO,
                OrcamentoDTOs = _orcamentoRepository.FindByCompra(compras.COD_COMPRAS).ToList()
            };
        }

        // POST: api/Compras
        [HttpPost("Cadastro_Compras")]
        public ActionResult<ComprasDTO> Post([FromBody] ComprasDTO comprasDTO)
        {
            comprasDTO.TITULO = comprasDTO.TITULO.Trim(' ');
            comprasDTO.DESCRICAO = comprasDTO.DESCRICAO.Trim(' ');
            if (!ValidaCompraCadastro(comprasDTO))
            {
                var Compra = new ComprasEntity()
                {
                    COD_CATEGORIA = comprasDTO.CATEGORIA.COD_CATEGORIA,
                    COD_STATUS = 1,
                    COD_USUARIO = comprasDTO.USUARIO.COD_USUARIO,
                    TITULO = comprasDTO.TITULO,
                    DESCRICAO = comprasDTO.DESCRICAO,
                    DATA_ABERTURA = DateTime.Now
                    
                };
                var NewCompra = _comprasRepository.Add(Compra);
                comprasDTO.COD_COMPRAS = NewCompra.COD_COMPRAS;
                for (int i = 0; i < comprasDTO.OrcamentoDTOs.Count; i++)
                {
                    comprasDTO.OrcamentoDTOs.ToArray()[i].COD_COMPRAS = NewCompra.COD_COMPRAS;
                    var orcamento = comprasDTO.OrcamentoDTOs[i];
                    var NewOrcamento = _orcamentoRepository.Add(orcamento);
                    comprasDTO.OrcamentoDTOs[i].COD_ORCAMENTO = NewOrcamento.COD_ORCAMENTO;
                }
                return new OkObjectResult(comprasDTO);

            }
            else
                return new BadRequestObjectResult("Erro no Cadastro");
            
        }

        private bool ValidaCompraCadastro(ComprasDTO compra)
        {
            int cont = 0;
            //var StatusExistente = _statusRepository.Get(compra.STATUS.COD_STATUS);
            var CategoriaExistente = _categoriaRepository.GetNoTracking(compra.CATEGORIA.COD_CATEGORIA);
            var UsuarioExistente = _usuarioRepository.GetNoTracking(compra.USUARIO.COD_USUARIO);
            if (_baseValida.ValidaCampoNull(compra.DESCRICAO, compra.TITULO)|| CategoriaExistente == null|| UsuarioExistente == null)
            {
                cont++;
            }

            for (int i = 0; i < compra.OrcamentoDTOs.Count; i++)
            {
                if (_baseValida.ValidaCampoNull(compra.OrcamentoDTOs[i].LINK, compra.OrcamentoDTOs[i].NOME)
                    | _baseValida.ValidaLink(compra.OrcamentoDTOs[i].LINK))
                {
                    cont++;
                }
            }
            if(cont>0)
            {
                return true;
            }
            return false;
        }

        // PUT: api/Compras/5
        [HttpPut("Update_Compras")]
        public ActionResult<CompraRequest> Put([FromBody] CompraRequest compraRequest)
        {
            var NewCompra = _comprasRepository.Get(compraRequest.Cod_compra);
            try
            {
                if (ValidaCompraUpdate(compraRequest))
                {
                    NewCompra.TITULO = compraRequest.Titulo;
                    NewCompra.DESCRICAO = compraRequest.Descricao;

                    var Compra = _comprasRepository.Update(NewCompra);
                    return new OkObjectResult(compraRequest);
                }else
                    return new BadRequestObjectResult("Erro Update");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e);
            }
        }

        private bool ValidaCompraUpdate(CompraRequest compraRequest)
        {
            var CompraExistente = _comprasRepository.Get(compraRequest.Cod_compra);
            if (_baseValida.ValidaCampoNull(compraRequest.Titulo, compraRequest.Descricao)
                || CompraExistente == null)
            {
                return true;
            }
            return false;
        }

        [HttpPut("Troca_Status/")]
        public ActionResult MudaStatus(int cod_usuario, int cod_compra, int cod_Status)
        {
            var usuario = _usuarioRepository.Get(cod_usuario);
            var compra = _comprasRepository.Get(cod_compra);
            var status = _statusRepository.Get(cod_Status);
            if (status != null & usuario != null & compra != null)
            {
                if (compra.COD_STATUS != 10)
                {
                    if (cod_Status == 2 && usuario.COD_PERFIL == 1 || usuario.COD_PERFIL == 2)
                    {
                        goto flag;
                    }
                    else if (cod_Status == 10 && (usuario.COD_PERFIL == 1 || usuario.COD_PERFIL == 2 || usuario.COD_PERFIL == 3))
                    {
                        goto flag;
                    }
                    else if ((cod_Status != 2 && cod_Status != 10) && (usuario.COD_PERFIL != 1 & usuario.COD_PERFIL != 2 & usuario.COD_PERFIL != 3))
                    {
                        goto flag;
                    }
                    else
                        return new BadRequestObjectResult("Permissão inválida");
                }else
                    return new BadRequestObjectResult("Compra Finalizado, não pode alterar o Status!");
            }
            else
                return new BadRequestObjectResult("Não existe");
            flag:
            _comprasRepository.Update(UpdateStatus(compra, cod_Status));

            return new OkResult();
        }

        private ComprasEntity UpdateStatus(ComprasEntity comprasEntity, int cod_status)
        {
            var CompraEntitity = comprasEntity;
            if (cod_status == 10)
                comprasEntity.DATA_FINALIZACAO = DateTime.Now;            
            CompraEntitity.COD_STATUS = cod_status;
            return CompraEntitity;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Delete_compra/{id}")]
        public ActionResult Delete(int id)
        {
            var existe = _comprasRepository.Get(id);
            if (existe != null)
            {
                var podeExcluir = _comprasRepository.PodeExcluir(id);
                if (podeExcluir)
                {
                    _comprasRepository.Delete(id);
                }
                else
                    return new BadRequestObjectResult("Nao Pode Ser Exlcuido");
            }
            else
                return new BadRequestObjectResult("Nao Existe Esse codigo de compra");
            return new OkResult();
        }
    }
}
