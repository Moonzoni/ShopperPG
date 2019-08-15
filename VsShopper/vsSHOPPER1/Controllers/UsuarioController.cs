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
    public class usuarioController : ControllerBase
    {
        private readonly IusuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IBaseValida _baseValida;


        public usuarioController(IusuarioRepository usuarioRepository, IPerfilRepository perfilRepository, IBaseValida baseValida)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _baseValida = baseValida;
        }

        // GET: api/usuario
        [HttpGet("Busca_Todos_usuarios/")]
        public IEnumerable<usuarioDTO> Get()
        {
            return _usuarioRepository.GetAll().Select(x => new usuarioDTO(){
                perfil = _perfilRepository.Get(x.cod_perfil),
                cod_usuario = x.cod_usuario,
                nome = x.nome,
                email = x.email
            });
        }

        // GET: api/usuario/5
        [HttpGet("Busca_usuario/{id}")]
        public ActionResult<usuarioDTO> Get(int id)
        {
            var usuario = _usuarioRepository.Get(id);
            if (usuario != null)
            {
                return new OkObjectResult(new usuarioDTO()
                {
                    perfil = _perfilRepository.Get(usuario.cod_perfil),
                    cod_usuario = usuario.cod_usuario,
                    nome = usuario.nome,
                    email = usuario.email
                });
            }
            else
                return new BadRequestObjectResult("Nao Existe Esse Id");
            
        }

        // POST: api/usuario
        [HttpPost("Cadastro_usuario")]
        public ActionResult<usuarioDTO> Post([FromBody] usuarioDTO usuarioDTO)
        {
            usuarioDTO.nome = usuarioDTO.nome.Trim(' ');//nao esta perfeito
            usuarioDTO.email = usuarioDTO.email.Trim(' ');
            if (!Validausuario(usuarioDTO))
            {
                var usuarioEntity = new usuarioEntity()
                {
                    cod_perfil = usuarioDTO.perfil.cod_perfil,
                    email = usuarioDTO.email,
                    nome = usuarioDTO.nome
                };
                var Newusuario = _usuarioRepository.Add(usuarioEntity);
                usuarioDTO.cod_usuario = Newusuario.cod_usuario;
                return new OkObjectResult(usuarioDTO);
            }
            else
                return new BadRequestObjectResult("Erro no cadastro");
        }

        private bool Validausuario(usuarioDTO usuario)
        {
            int cont = 0;
            var PerfilExistente = _perfilRepository.GetNoTracking(usuario.perfil.cod_perfil);
            var ValidaEmailExistente = _usuarioRepository.FindByEmail(usuario.email);
            
            if (_baseValida.ValidaCampoNull(usuario.nome, usuario.email, usuario.cod_usuario.ToString()) 
                | _baseValida.ValidaEmail(usuario.email)
                | _baseValida.ValidaNome(usuario.nome)
                | PerfilExistente == null)//Arrumar o email 
            {
                cont++;
            }

            if (usuario.cod_usuario == 0)
            {
                if (ValidaEmailExistente != null)
                {
                    cont++;
                }
            }


            if (usuario.cod_usuario != 0)
            {
                var usuarioExistente = _usuarioRepository.GetNoTracking(usuario.cod_usuario);
                if (usuarioExistente != null)
                {
                    if (ValidaEmailExistente != null && usuario.email != usuarioExistente.email) 
                    {
                        cont++;
                    }
                }
                else
                    cont++;
               
            }            
            if (cont>0)
            {
                return true;
            }
            return false;
        }
        // PUT: api/usuario/5
        [HttpPut("Update_usuario")]
        public ActionResult<usuarioDTO> Put([FromBody] usuarioDTO usuarioDTO)
        {
           try
            {
                if (!Validausuario(usuarioDTO))
                {
                    var usuarioEntity = new usuarioEntity()
                    {
                        cod_usuario = usuarioDTO.cod_usuario,
                        cod_perfil = usuarioDTO.perfil.cod_perfil,
                        email = usuarioDTO.email,
                        nome = usuarioDTO.nome
                    };
                    var Updateusuario = _usuarioRepository.Update(usuarioEntity);
                    return new OkObjectResult(usuarioDTO);
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
        [HttpDelete("Delete_usuario{id}")]
        public ActionResult Delete(int id)
        {
            var existe = _usuarioRepository.Get(id);
            if (existe != null)
            {
                var podeExcluir = _usuarioRepository.PodeExcluir(id);
                if (podeExcluir)
                {
                    _usuarioRepository.Delete(id);
                }
                else
                    return new BadRequestObjectResult("Nao Pode excluir esse usuario");
            }
            else
                return new BadRequestObjectResult("Nao existe esse Codigo");
            return new OkResult();
        }
    }
}
