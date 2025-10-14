using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;

namespace API.Domain.Interfaces.Gestion.Nomencladores
{
    public interface ICarritoService : IBaseService<Carrito, CarritoValidator>
    {
    }
}