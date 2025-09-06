using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.ExternalWS;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System.Security.Cryptography;
using System.Text.Json;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.CodigosVerificacion.Commands.CreateCodigo
{
    internal class CreateCodigoHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger, ISendEmailAPI sendEmailAPI) : ICommandHandler<CreateCodigoCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly HandlerExecutor _executor = executor;
        private readonly IFileLogger _fileLogger = fileLogger;
        private readonly ISendEmailAPI _sendEmailAPI = sendEmailAPI;

        public async Task<BaseResponse<string>> Handle(CreateCodigoCommand command, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync(command, () => CreateCodigoAsync(command, cancellationToken), cancellationToken);
        }

        private async Task<BaseResponse<string>> CreateCodigoAsync(CreateCodigoCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<string>();

            try
            {
                _fileLogger.Log("ws_congreso", "CreateCodigo", "0", command);

                var codigo = command.Adapt<CodigoVerificacion>();

                var user = await _unitOfWork.User.UserByEmailAsync(command.Email);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                    _fileLogger.Log("ws_congreso", "CreateCodigo", "1", response);

                    return response;
                }

                var randomNumber = new byte[4];
                RandomNumberGenerator.Create().GetBytes(randomNumber);
                int result = BitConverter.ToInt32(randomNumber, 0);

                var code = (Math.Abs(result) % 900000 + 100000).ToString();

                var codeSTR = code.ToString();

                var codeHash = BC.HashPassword(codeSTR);

                codigo.UserId = user.Id;
                codigo.Codigo = codeHash;
                codigo.FechaCreacion = DateTime.UtcNow;
                codigo.FechaExpiracion = DateTime.UtcNow.AddMinutes(1);
                codigo.Estado = (int)TipoEstado.Activo;

                await _unitOfWork.CodigoVerificacion.CreateAsync(codigo);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var endpoint = "SendEmail";

                object peticion = new
                {
                    plantilla = "verificacionemail.html",
                    to = "tebalandonis@gmail.com",
                    subject = "Verificación de Correo",
                    body = new Dictionary<string, string>
                    {
                        { "0", $"{codeSTR[0]}"},
                        { "1", $"{codeSTR[1]}"},
                        { "2", $"{codeSTR[2]}"},
                        { "3", $"{codeSTR[3]}"},
                        { "4", $"{codeSTR[4]}"},
                        { "5", $"{codeSTR[5]}"}
                    }
                };

                var envioEmail = await _sendEmailAPI.PostDataAsync<dynamic>(endpoint, peticion);

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
                response.Data = codeSTR;

                _fileLogger.Log("ws_congreso", "CreateCodigo", "1", response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                _fileLogger.Log("ws_congreso", "CreateCodigo", "1", response, ex.Message);
            }

            return response;
        }
    }
}
