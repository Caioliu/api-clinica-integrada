using Application.DTOs;
using MediatR;

namespace Application.Handlers.ListaEspera.Commands.Create
{
    public class CreateListaEsperaEntryCommand : ListaEsperaEntryCommand, IRequest<ListaEsperaEntryDto>
    {

    }

}
