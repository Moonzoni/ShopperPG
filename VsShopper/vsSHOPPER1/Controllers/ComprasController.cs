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
        private readonly IusuarioRepository _usuarioRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IBaseValida _baseValida;

        public ComprasController(IComprasRepository compras, IStatusRepository status,
                                    IusuarioRepository usuario, ICategoriaRepository categoria, 
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

        [HttpGet("Busca_Compras_status{idstatus}")]
        public IEnumerable<ComprasDTO> Busca_status(int idstatus)
        {
            var existe = _statusRepository.Get(idstatus);
            if (existe != null)
            {
                return Find(_comprasRepository.FindBystatus(idstatus));
            }
            return null;
        }

        [HttpGet("Busca_Compras_categoria{cod_categoria}")]
        public IEnumerable<ComprasDTO> Busca_categoria(int cod_categoria)
        {
            var existe = _categoriaRepository.Get(cod_categoria);
            if (existe != null)
            {
                return Find(_comprasRepository.FindByCategoria(cod_categoria));
            }
            return null;
        }

        [HttpGet("Busca_Compras_titulo{titulo}")]
        public IEnumerable<ComprasDTO> FindBytitulo(string titulo)
        {
            return Find(_comprasRepository.FindBytitulo(titulo));
        }

        [HttpGet("Busca_Compras_descricao{descricao}")]
        public IEnumerable<ComprasDTO> FindByDescricao(string descricao)
        {
            return Find(_comprasRepository.FindByDescricao(descricao));
        }

        [HttpGet("Busca_Compras_Aprovadas")]
        public IEnumerable<ComprasDTO> Busca_status_Aprovado()
        {
            var existe = _statusRepository.Get(2);
            if (existe != null)
                return Find(_comprasRepository.FindBystatus(2));

            return null;
        }
        [HttpGet("Busca_status_Finazilados")]
        public IEnumerable<ComprasDTO> Busca_status_Finazilados()
        {
            var existe = _statusRepository.Get(10);
            if (existe != null)
                return Find(_comprasRepository.FindBystatus(10));
            return null;
        }


        private IEnumerable<ComprasDTO> Find(IEnumerable<ComprasEntity> Listcompras)
        {
            return Listcompras.Select(x => new ComprasDTO()
            {
                status = _statusRepository.Get(x.cod_status),
                usuario = _usuarioRepository.Get(x.cod_usuario),
                categoria = _categoriaRepository.Get(x.cod_categoria),
                cod_compra = x.cod_compra,
                descricao = x.descricao,
                titulo = x.titulo,
                data_abertura = x.data_abertura,
                data_finalizada = x.data_finalizada,
                orcamentodtos = _orcamentoRepository.FindByCompra(x.cod_compra).ToList()
            });
        }

        // GET: api/Compras/5
        [HttpGet("Busca_Compras/{id}")]
        public ComprasDTO Get(int id)
        {
            var compras = _comprasRepository.Get(id);
            return new ComprasDTO()
            {
                status = _statusRepository.Get(compras.cod_status),
                usuario = _usuarioRepository.Get(compras.cod_usuario),
                categoria = _categoriaRepository.Get(compras.cod_categoria),
                cod_compra = compras.cod_compra,
                descricao = compras.descricao,
                titulo = compras.titulo,
                data_abertura = compras.data_abertura,
                data_finalizada = compras.data_finalizada,
                orcamentodtos = _orcamentoRepository.FindByCompra(compras.cod_compra).ToList()
            };
        }

        // POST: api/Compras
        [HttpPost("Cadastro_Compras")]
        public ActionResult<ComprasDTO> Post([FromBody] ComprasDTO comprasDTO)
        {
            comprasDTO.titulo = comprasDTO.titulo.Trim(' ');
            comprasDTO.descricao = comprasDTO.descricao.Trim(' ');
            if (!ValidaCompraCadastro(comprasDTO))
            {
                var Compra = new ComprasEntity()
                {
                    cod_categoria = comprasDTO.categoria.cod_categoria,
                    cod_status = 1,
                    cod_usuario = comprasDTO.usuario.cod_usuario,
                    titulo = comprasDTO.titulo,
                    descricao = comprasDTO.descricao,
                    data_abertura = DateTime.Now
                    
                };
                var NewCompra = _comprasRepository.Add(Compra);
                comprasDTO.cod_compra = NewCompra.cod_compra;
                for (int i = 0; i < comprasDTO.orcamentodtos.Count; i++)
                {
                    comprasDTO.orcamentodtos.ToArray()[i].cod_compra = NewCompra.cod_compra;
                    var orcamento = comprasDTO.orcamentodtos[i];
                    var NewOrcamento = _orcamentoRepository.Add(orcamento);
                    comprasDTO.orcamentodtos[i].cod_orcamento = NewOrcamento.cod_orcamento;
                }
                return new OkObjectResult(comprasDTO);

            }
            else
                return new BadRequestObjectResult("Erro no Cadastro");
            
        }

        private bool ValidaCompraCadastro(ComprasDTO compra)
        {
            int cont = 0;
            //var statusExistente = _statusRepository.Get(compra.status.cod_status);
            var categoriaExistente = _categoriaRepository.GetNoTracking(compra.categoria.cod_categoria);
            var usuarioExistente = _usuarioRepository.GetNoTracking(compra.usuario.cod_usuario);
            if (_baseValida.ValidaCampoNull(compra.descricao, compra.titulo)|| categoriaExistente == null|| usuarioExistente == null)
            {
                cont++;
            }

            for (int i = 0; i < compra.orcamentodtos.Count; i++)
            {
                if (_baseValida.ValidaCampoNull(compra.orcamentodtos[i].link, compra.orcamentodtos[i].nome)
                    | _baseValida.ValidaLink(compra.orcamentodtos[i].link))
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
                    NewCompra.titulo = compraRequest.titulo;
                    NewCompra.descricao = compraRequest.descricao;

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
            if (_baseValida.ValidaCampoNull(compraRequest.titulo, compraRequest.descricao)
                || CompraExistente == null)
            {
                return true;
            }
            return false;
        }

        [HttpPut("Troca_status/")]
        public ActionResult Mudastatus(int cod_usuario, int cod_compra, int cod_status)
        {
            var usuario = _usuarioRepository.Get(cod_usuario);
            var compra = _comprasRepository.Get(cod_compra);
            var status = _statusRepository.Get(cod_status);
            if (status != null & usuario != null & compra != null)
            {
                if (compra.cod_status != 10)
                {
                    if (cod_status == 2 && usuario.cod_perfil == 1 || usuario.cod_perfil == 2)
                    {
                        goto flag;
                    }
                    else if (cod_status == 10 && (usuario.cod_perfil == 1 || usuario.cod_perfil == 2 || usuario.cod_perfil == 3))
                    {
                        goto flag;
                    }
                    else if ((cod_status != 2 && cod_status != 10) && (usuario.cod_perfil != 1 & usuario.cod_perfil != 2 & usuario.cod_perfil != 3))
                    {
                        goto flag;
                    }
                    else
                        return new BadRequestObjectResult("Permissão inválida");
                }else
                    return new BadRequestObjectResult("Compra Finalizado, não pode alterar o status!");
            }
            else
                return new BadRequestObjectResult("Não existe");
            flag:
            _comprasRepository.Update(Updatestatus(compra, cod_status));

            return new OkResult();
        }

        private ComprasEntity Updatestatus(ComprasEntity comprasEntity, int cod_status)
        {
            var CompraEntitity = comprasEntity;
            if (cod_status == 10)
                comprasEntity.data_finalizada = DateTime.Now;            
            CompraEntitity.cod_status = cod_status;
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
