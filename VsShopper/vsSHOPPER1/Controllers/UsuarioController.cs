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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IBaseValida _baseValida;


        public UsuarioController(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository, IBaseValida baseValida)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _baseValida = baseValida;
        }

        // GET: api/Usuario
        [HttpGet("Busca_Todos_Usuarios/")]
        public IEnumerable<UsuarioDTO> Get()
        {
            return _usuarioRepository.GetAll().Select(x => new UsuarioDTO(){
                PERFIL = _perfilRepository.Get(x.COD_PERFIL),
                COD_USUARIO = x.COD_USUARIO,
                NOME = x.NOME,
                EMAIL = x.EMAIL
            });
        }

        // GET: api/Usuario/5
        [HttpGet("Busca_Usuario/{id}")]
        public ActionResult<UsuarioDTO> Get(int id)
        {
            var usuario = _usuarioRepository.Get(id);
            if (usuario != null)
            {
                return new OkObjectResult(new UsuarioDTO()
                {
                    PERFIL = _perfilRepository.Get(usuario.COD_PERFIL),
                    COD_USUARIO = usuario.COD_USUARIO,
                    NOME = usuario.NOME,
                    EMAIL = usuario.EMAIL
                });
            }
            else
                return new BadRequestObjectResult("Nao Existe Esse Id");
            
        }

        // POST: api/Usuario
        [HttpPost("Cadastro_Usuario")]
        public ActionResult<UsuarioDTO> Post([FromBody] UsuarioDTO usuarioDTO)
        {
            usuarioDTO.NOME = usuarioDTO.NOME.Trim(' ');//nao esta perfeito
            usuarioDTO.EMAIL = usuarioDTO.EMAIL.Trim(' ');
            if (!ValidaUsuario(usuarioDTO))
            {
                var usuarioEntity = new UsuarioEntity()
                {
                    COD_PERFIL = usuarioDTO.PERFIL.COD_PERFIL,
                    EMAIL = usuarioDTO.EMAIL,
                    NOME = usuarioDTO.NOME
                };
                var NewUsuario = _usuarioRepository.Add(usuarioEntity);
                usuarioDTO.COD_USUARIO = NewUsuario.COD_USUARIO;
                return new OkObjectResult(usuarioDTO);
            }
            else
                return new BadRequestObjectResult("Erro no cadastro");
        }

        private bool ValidaUsuario(UsuarioDTO usuario)
        {
            int cont = 0;
            var PerfilExistente = _perfilRepository.GetNoTracking(usuario.PERFIL.COD_PERFIL);
            var ValidaEmailExistente = _usuarioRepository.FindByEmail(usuario.EMAIL);
            
            if (_baseValida.ValidaCampoNull(usuario.NOME, usuario.EMAIL, usuario.COD_USUARIO.ToString()) 
                | _baseValida.ValidaEmail(usuario.EMAIL)
                | _baseValida.ValidaNome(usuario.NOME)
                | PerfilExistente == null)//Arrumar o email 
            {
                cont++;
            }

            if (usuario.COD_USUARIO == 0)
            {
                if (ValidaEmailExistente != null)
                {
                    cont++;
                }
            }


            if (usuario.COD_USUARIO != 0)
            {
                var UsuarioExistente = _usuarioRepository.GetNoTracking(usuario.COD_USUARIO);
                if (UsuarioExistente != null)
                {
                    if (ValidaEmailExistente != null && usuario.EMAIL != UsuarioExistente.EMAIL) 
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
        // PUT: api/Usuario/5
        [HttpPut("Update_Usuario")]
        public ActionResult<UsuarioDTO> Put([FromBody] UsuarioDTO usuarioDTO)
        {
           try
            {
                if (!ValidaUsuario(usuarioDTO))
                {
                    var usuarioEntity = new UsuarioEntity()
                    {
                        COD_USUARIO = usuarioDTO.COD_USUARIO,
                        COD_PERFIL = usuarioDTO.PERFIL.COD_PERFIL,
                        EMAIL = usuarioDTO.EMAIL,
                        NOME = usuarioDTO.NOME
                    };
                    var UpdateUsuario = _usuarioRepository.Update(usuarioEntity);
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
        [HttpDelete("Delete_Usuario{id}")]
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
