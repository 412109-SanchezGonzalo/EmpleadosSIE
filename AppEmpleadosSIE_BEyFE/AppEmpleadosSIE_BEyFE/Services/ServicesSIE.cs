using AppEmpleadosSIE_BEyFE.Data.Interfaces;
using AppEmpleadosSIE_BEyFE.Models;

namespace AppEmpleadosSIE_BEyFE.Services
{
    public class ServicesSIE : IServicesSIE
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioXServicioRepository _usuarioXActividadRepository;

        public ServicesSIE(IUsuarioRepository usuarioRepository, IUsuarioXServicioRepository usuarioXActividadRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioXActividadRepository = usuarioXActividadRepository;
        }



        // USUARIO
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _usuarioRepository.GetAll();
        }
        public string GetUsuarioByPassword(string password)
        {
            return _usuarioRepository.GetNameByPassword(password);
        }
        public string GetUsuarioByNickName(string nick)
        {
            return _usuarioRepository.GetFullNameByNickName(nick);
        }
        public int GetUserIdByPassword(string contrasena)
        {
            return _usuarioRepository.GetUserIdByPassword(contrasena);
        }
        public Usuario GetByData(string contrasena)
        {
            return _usuarioRepository.GetByData(contrasena);
        }
        public Usuario GetUsuarioByCredenciales(string nick, string contrasena)
        {
            return _usuarioRepository.GetByCredenciales(nick, contrasena);
        }
        public Usuario GetByName(string name)
        {
            return _usuarioRepository.GetByName(name);
        }
        public void AddUsuario(Usuario usuario)
        {
            _usuarioRepository.Add(usuario);
        }
        public void UpdateUsuario(Usuario usuario)
        {
            _usuarioRepository.Update(usuario);
        }

        public void UpdateStatus(int id, string status)
        {
            _usuarioRepository.UpdateStatus(id, status);
        }
        public void DeleteUsuario(int id)
        {
            _usuarioRepository.Delete(id);
        }

        // USUARIO X SERVICIO
        public IEnumerable<UsuarioXServicio> GetByUsuarioXActividad(int idUsuario)
        {
            return _usuarioXActividadRepository.GetByUsuario(idUsuario);
        }
        public int AddUsuarioXActividad(UsuarioXServicio registro)
        {
            return _usuarioXActividadRepository.Add(registro);
        }
        public void UpdateEstadoUsuarioXActividad(int idUsuario, int idActividad, string nuevoEstado)
        {
            _usuarioXActividadRepository.UpdateEstado(idUsuario, idActividad, nuevoEstado);
        }
    }
}
