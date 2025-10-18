using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Actividades;
using congreso.Application.UseCase.Actividades.Commands.Create;
using congreso.Application.UseCase.Actividades.Commands.Delete;
using congreso.Application.UseCase.Actividades.Commands.Update;
using congreso.Application.UseCase.Actividades.Commands.UpdateEstadoActividad;
using congreso.Application.UseCase.Actividades.Queries.GetAll;
using congreso.Application.UseCase.Actividades.Queries.GetById;
using congreso.Application.UseCase.Actividades.Queries.GetParticipantsByActivityId;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
﻿{
﻿    [Route("api/[controller]")]
﻿    [ApiController]
﻿    public class ActividadController(IDispatcher dispatcher) : ControllerBase
﻿    {
﻿        private readonly IDispatcher _dispatcher = dispatcher;
﻿
﻿        [HttpGet]
﻿        public async Task<IActionResult> ActividadList([FromQuery] GetAllActividadesQuery query)
﻿        {
﻿            var response = await _dispatcher
﻿                .Dispatch<GetAllActividadesQuery, IEnumerable<ActividadResponseDto>>(query, CancellationToken.None);
﻿
﻿            return response.IsSuccess ? Ok(response) : BadRequest(response);
﻿        }
﻿
﻿        [HttpGet("{actividadId:int}")]
﻿        public async Task<IActionResult> ActividadById(int actividadId)
﻿        {
﻿            var response = await _dispatcher
﻿                .Dispatch<GetByIdActividadQuery, ActividadByIdResponseDto>(new GetByIdActividadQuery { ActividadId = actividadId }, CancellationToken.None);
﻿
﻿            return response.IsSuccess ? Ok(response) : BadRequest(response);
﻿        }
﻿
﻿        [HttpPost("Create")]
﻿        public async Task<IActionResult> CreateActividad([FromForm] CreateActividadCommand command)
﻿        {
﻿            var ponente = command.GetPonente();
﻿            var objetivos = command.GetObjetivos();
﻿            var materiales = command.GetMateriales();
﻿
﻿            var response = await _dispatcher
﻿                .Dispatch<CreateActividadCommand, bool>(command, CancellationToken.None);
﻿
﻿            return response.IsSuccess ? Ok(response) : BadRequest(response);
﻿        }
﻿
﻿        [HttpPut("Update")]
﻿        public async Task<IActionResult> UpdateActividad([FromBody] UpdateActividadCommand command)
﻿        {
﻿            var response = await _dispatcher
﻿                .Dispatch<UpdateActividadCommand, bool>(command, CancellationToken.None);
﻿
﻿            return response.IsSuccess ? Ok(response) : BadRequest(response);
﻿        }
﻿
﻿        [HttpDelete("Delete/{actividadId:int}")]
﻿        public async Task<IActionResult> DeleteActividad(int actividadId)
﻿        {
﻿            var response = await _dispatcher
﻿                .Dispatch<DeleteActividadCommand, bool>(new DeleteActividadCommand { ActividadId = actividadId }, CancellationToken.None);
﻿
﻿            return response.IsSuccess ? Ok(response) : BadRequest(response);
﻿        }

        [HttpPut("UpdateEstado")]
        public async Task<IActionResult> UpdateEstadoActividad([FromBody] UpdateEstadoActividadCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateEstadoActividadCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{actividadId:int}/Participants")]
        public async Task<IActionResult> GetParticipantsByActivityId(int actividadId)
        {
            var response = await _dispatcher
                .Dispatch<GetParticipantsByActivityIdQuery, IEnumerable<ParticipantByActivityDto>>(new GetParticipantsByActivityIdQuery(actividadId), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
﻿    }
﻿}
