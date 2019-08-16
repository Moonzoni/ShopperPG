using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IBaseValida _baseValida;
        private readonly IStatusRepository _statusRepository;
        private readonly IComprasRepository _comprasRepository;
        private readonly IOrcamentoRepository _orcamentoRepository;

        public ComprasController(IComprasRepository compras, IStatusRepository status,
                                IUsuarioRepository usuario, ICategoriaRepository categoria, 
                                IOrcamentoRepository orcamento, IBaseValida baseValida,
                                IPerfilRepository perfil)
        {
            _perfilRepository = perfil;
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
        public ActionResult<IEnumerable<ComprasDTO>> Busca_status(int idstatus)
        {
            var existe = _statusRepository.Get(idstatus);
            if (existe != null)
            {
                return new OkObjectResult(Find(_comprasRepository.FindBystatus(idstatus)));
            }
            return new NotFoundResult();
        }

        [HttpGet("Busca_Compras_categoria{cod_categoria}")]
        public ActionResult<object> Busca_categoria(int cod_categoria)
        {
            var existe = _categoriaRepository.Get(cod_categoria);
            if (existe != null)
            {
                return new OkObjectResult(Find(_comprasRepository.FindByCategoria(cod_categoria)));
            }
            return new NotFoundResult();
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
            var existe = _statusRepository.GetStatusByName("Aprovado");
            if (existe != null)
            {
                return Find(_comprasRepository.FindBystatus(existe.cod_status));
            }
            return null;
        }
        [HttpGet("Busca_status_Finalizados")]
        public IEnumerable<ComprasDTO> Busca_status_Finazilados()
        {
            var existe = _statusRepository.GetStatusByName("Finalizado");
            if (existe != null)
            {
                return Find(_comprasRepository.FindBystatus(existe.cod_status));
            }
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
        public ActionResult<ComprasDTO> Get(int id)
        {
            var compras = _comprasRepository.Get(id);
            if (compras != null)
            {
                ComprasDTO comprasDTO =  new ComprasDTO()
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
                return new OkObjectResult(comprasDTO);
            }
            else
            {
                return new NotFoundObjectResult(null);
            }
        }

        // POST: api/Compras
        [HttpPost("Cadastro_Compras")]
        public ActionResult<ComprasDTO> Post([FromBody] CompraModel comprasModel)
        {
            comprasModel.titulo = comprasModel.titulo.Trim(' ');
            comprasModel.descricao = comprasModel.descricao.Trim(' ');
            if (!ValidaCompraCadastro(comprasModel))
            {
                var Compra = new ComprasEntity()
                {
                    cod_categoria = comprasModel.cod_categoria,
                    cod_status = 1,
                    cod_usuario = comprasModel.cod_usuario,
                    titulo = comprasModel.titulo,
                    descricao = comprasModel.descricao,
                    data_abertura = DateTime.Now
                };
                var NewCompra = _comprasRepository.Add(Compra);
                comprasModel.Cod_compra = NewCompra.cod_compra;
                for (int i = 0; i < comprasModel.orcamentodtos.Count; i++)
                {
                    comprasModel.orcamentodtos.ToArray()[i].cod_compra = NewCompra.cod_compra;
                    var orcamento = comprasModel.orcamentodtos[i];
                    var NewOrcamento = _orcamentoRepository.Add(orcamento);
                    comprasModel.orcamentodtos[i].cod_orcamento = NewOrcamento.cod_orcamento;
                }
                return new OkObjectResult(comprasModel);
            }
            else
            {
                return new BadRequestObjectResult("Erro no cadastro, campos inválidos.");
            }
        }

        private bool ValidaCompraCadastro(CompraModel compra)
        {
            int cont = 0;
            var categoriaExistente = _categoriaRepository.GetNoTracking(compra.cod_categoria);
            var usuarioExistente = _usuarioRepository.GetNoTracking(compra.cod_usuario);
            if (_baseValida.ValidaCampoNull(compra.descricao, compra.titulo) || categoriaExistente == null || usuarioExistente == null)
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
            if (cont > 0)
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
                if (!ValidaCompraUpdate(compraRequest))
                {
                    NewCompra.titulo = compraRequest.titulo;
                    NewCompra.descricao = compraRequest.descricao;

                    var Compra = _comprasRepository.Update(NewCompra);
                    return new OkObjectResult(compraRequest);
                }
                else
                {
                    return new BadRequestObjectResult("Erro Update");
                }
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
            var compra = _comprasRepository.Get(cod_compra);
            var statusAtual = _statusRepository.Get(compra.cod_status);
            var user = _usuarioRepository.Get(cod_usuario);
            var perfil = _perfilRepository.Get(user.cod_perfil);
            var statusNovo = _statusRepository.Get(cod_status);
            
            if (compra != null & user != null & statusNovo != null)
            {
                if (statusAtual.nome != "Finalizado")
                {
                    if (statusNovo.nome == "Aprovado" && (perfil.nome == "Admin" || perfil.nome == "Gerente"))
                    {
                        goto flag;
                    }
                    else if (statusNovo.nome == "Finalizado" && (perfil.nome == "Admin" || perfil.nome == "Gerente" || perfil.nome == "Analista de Compras"))
                    {
                        goto flag;
                    }
                    else if ((statusNovo.nome != "Finalizado" && statusNovo.nome != "Aprovado") && (perfil.nome != "Admin" & perfil.nome != "Gerente" & perfil.nome != "Analista de Compras"))
                    {
                        goto flag;
                    }
                    else
                    {
                        return new BadRequestObjectResult("Permissão inválida!");
                    }
                }
                else
                {
                    return new BadRequestObjectResult("Compra Finalizado, não pode alterar o status!");
                }
            }
            else
            {
                return new BadRequestObjectResult("Não existe");
            }
        flag:
            _comprasRepository.Update(Updatestatus(compra, cod_status));

            return new OkResult();
        }

        private ComprasEntity Updatestatus(ComprasEntity comprasEntity, int cod_status)
        {
            var CompraEntitity = comprasEntity;
            if (cod_status == 10)
            {
                comprasEntity.data_finalizada = DateTime.Now;
                CompraEntitity.cod_status = cod_status;
            }
            return CompraEntitity;
        }
    }
}
