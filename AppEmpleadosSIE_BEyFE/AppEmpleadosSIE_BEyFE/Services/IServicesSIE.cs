using AppEmpleadosSIE_BEyFE.Models;

namespace AppEmpleadosSIE_BEyFE.Services
{
    public interface IServicesSIE
    {
        // USUARIO
        IEnumerable<Usuario> GetAllUsuarios();
        string GetUsuarioByPassword(string password);
        string GetUsuarioByNickName(string nick);
        int GetUserIdByPassword(string contrasena);
        Usuario GetUsuarioByCredenciales(string nick, string contrasena);
        Usuario GetByData(string contrasena);
        Usuario GetByName(string name);
        void AddUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void UpdateStatus(int id, string status);
        void DeleteUsuario(int id);

        // USUARIO X Servicio
        IEnumerable<UsuarioXServicio> GetByUsuarioXActividad(int idUsuario);
        int AddUsuarioXActividad(UsuarioXServicio registro);
        void UpdateEstadoUsuarioXActividad(int idUsuario, int idActividad, string nuevoEstado);
    }
}
