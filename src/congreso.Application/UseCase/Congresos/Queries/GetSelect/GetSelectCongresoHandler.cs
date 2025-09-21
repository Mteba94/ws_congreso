using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Commons;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;
using System.Globalization;

namespace congreso.Application.UseCase.Congresos.Queries.GetSelect;

internal sealed class GetSelectCongresoHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSelectCongresoQuery, IEnumerable<SelectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetSelectCongresoQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var congreso = await _unitOfWork.Congreso
                .GetAllAsync();

            if (congreso is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var selectResponses = congreso.Select(c =>
            {
                var fechaInicio = c.FechaInicio.Date;
                var fechaFin = c.FechaFin.Date;
                string descripcion;

                // Compara si la fecha de inicio y fin son el mismo día
                if (fechaInicio == fechaFin)
                {
                    // Caso: Mismo día (ej. "17 marzo 2025 de 3 a 5 pm")
                    var culturaEspanol = new CultureInfo("es-ES");

                    var horaInicio = c.FechaInicio.ToString("h tt", CultureInfo.InvariantCulture).ToLower();
                    var horaFin = c.FechaFin.ToString("h tt", CultureInfo.InvariantCulture).ToLower();
                    var fecha = fechaInicio.ToString("dd MMMM yyyy", culturaEspanol);

                    // Resultado esperado: "01 septiembre 2025 de 3 pm a 6 pm"
                    descripcion = $"{fecha} de {horaInicio} a {horaFin}";

                    // El resultado sería "03 PM" o "06 PM". 
                    // Para que se vea como en tu ejemplo, lo ajustaremos.

                    // Elimina el cero inicial si la hora es de un solo dígito
                    if (horaInicio.StartsWith("0"))
                    {
                        horaInicio = horaInicio.Substring(1);
                    }
                    if (horaFin.StartsWith("0"))
                    {
                        horaFin = horaFin.Substring(1);
                    }

                    descripcion = $"{fecha} de {horaInicio.ToLower()} a {horaFin.ToLower()}";
                }
                else
                {
                    // Caso: Varios días
                    var diaInicio = fechaInicio.Day;
                    var diaFin = fechaFin.Day;
                    var mes = fechaFin.ToString("MMMM", new CultureInfo("es-ES"));
                    var anio = fechaFin.Year;

                    descripcion = $"{diaInicio}-{diaFin} {mes} {anio}";
                }

                // Mapea los valores manualmente al SelectResponseDto
                return new SelectResponseDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Descripcion = descripcion
                };
            });

            response.IsSuccess = true;
            response.Data = selectResponses;
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
