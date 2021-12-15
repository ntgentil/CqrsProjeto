using BaseCore.Commands;
using Core.Application.Importacao.Commands.Inputs;
using Core.Application.Importacao.Commands.Results;
using Core.Constants;
using CQRSProjeto.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRSProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ICommandDispatcher CommandDispatcher;

        public ImportController(ICommandDispatcher dispatcher)
        {
            CommandDispatcher = dispatcher;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<ErrosValidacaoResult>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ValidarCalculo([FromBody] ImportacaoCommand command)
        {
            var result = await CommandDispatcher.ExecuteAsync(command);

            if (result.Success)
                return Ok(ApiResult.Ok(ApplicationMessages.ValidacaoCalculoAprovada));
            else
                return UnprocessableEntity(ApiResult.Fail(result.ErrorMessages));
        }
    }
}
