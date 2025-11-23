

namespace Aplicacion.Interface
{
    public interface ICorreos
    {
        void EnviarCorreo(string destinatario, string asunto, string cuerpo, bool esHtml = false);
    }
}
