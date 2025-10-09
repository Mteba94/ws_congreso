using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using System.Text.Json;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.CodigosVerificacion.Queries.ValidacionCodigo;

internal sealed class ValidacionCodigoHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : IQueryHandler<ValidacionCodigoQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<bool>> Handle(ValidacionCodigoQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            _fileLogger.Log("ws_congreso", "ValidacionCodigo", "0", query);

            var user = await _unitOfWork.User.UserByEmailAsync(query.Email);

            if(user == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "ValidacionCodigo", "1", response);

                return response;
            }

            var codigoVal = await _unitOfWork.CodigoVerificacion.ValidarCodigoAsync(user.Id, query.Purpose);

            if (codigoVal == null)
            {
                response.IsSuccess = false;
                response.Message = "El código de verificación no existe o ha expirado.";

                _fileLogger.Log("ws_congreso", "ValidacionCodigo", "1", response);

                return response;
            }

            if (!BC.Verify(query.Codigo, codigoVal!.Codigo))
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_CODE_ERROR;

                _fileLogger.Log("ws_congreso", "ValidacionCodigo", "1", response);

                return response;
            }

            if (codigoVal.FechaExpiracion < DateTime.UtcNow)
            {
                response.IsSuccess = false;
                response.Message = "El código de verificación ha expirado.";

                _fileLogger.Log("ws_congreso", "ValidacionCodigo", "1", response);

                return response;
            }

            int estadoF = (int)TipoEstado.Inactivo;

            if (query.Purpose.Equals("recovery"))
            {
                estadoF = (int)TipoEstado.Pendiente;
            }

            codigoVal.Estado = estadoF;

            _unitOfWork.CodigoVerificacion.Update(codigoVal);
            await _unitOfWork.SaveChangesAsync();

            user.EmailConfirmed = true;

            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangesAsync();

            response.Data = true;
            response.IsSuccess = true;
            response.Message = "Código de verificación válido.";

            _fileLogger.Log("ws_congreso", "ValidacionCodigo", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error al validar el código: {ex.Message}";

            _fileLogger.Log("ws_congreso", "ValidacionCodigo", "1", response, ex.Message);
        }

        return response;
    }
}
