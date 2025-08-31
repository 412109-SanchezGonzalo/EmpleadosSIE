using AppEmpleadosSIE_BEyFE.Models;

namespace AppEmpleadosSIE_BEyFE.Data.Interfaces
{
    public interface IUsuarioXServicioRepository
    {
        IEnumerable<UsuarioXServicio> GetByUsuario(int idUsuario);
        int Add(UsuarioXServicio registro);
        void UpdateEstado(int idUsuario, int idActividad, string nuevoEstado);
    }
}
