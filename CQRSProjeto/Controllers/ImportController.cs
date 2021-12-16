﻿using BaseCore.Commands;
using BaseCore.Queries;
using Core.Application.Importacao.Commands.Results;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using CQRSProjeto.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private IQueryProcessor Processor { get; }
        private readonly ICommandDispatcher CommandDispatcher;

        public ImportController(IQueryProcessor processor, ICommandDispatcher dispatcher)
        {
            Processor = processor;
            CommandDispatcher = dispatcher;
        }

        /// <summary>
        /// Deverá receber como entrada um arquivo Excel,  caso o lote seja válido o mesmo deve ser salvo no banco de dados.
        /// </summary>
        /// <returns></returns>
        [HttpPost("Insert")]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<ErrosValidacaoResult>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Insert(IFormFile file)
        {
            //var result = await CommandDispatcher.ExecuteAsync(command);

            //if (result.Success)
            //    return Ok(ApiResult.Ok(ApplicationMessages.ValidacaoCalculoAprovada));
            //else
            //    return UnprocessableEntity(ApiResult.Fail(result.ErrorMessages));

            return Ok();
        }


        /// <summary>
        /// Deverá listar todas as importações mostrando o seu identificador criado no método de insert
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllImports")]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<ErrosValidacaoResult>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAllImports()
        {
            //var result = await CommandDispatcher.ExecuteAsync(id);

            //if (result.Success)
            //    return Ok(ApiResult.Ok(ApplicationMessages.ValidacaoCalculoAprovada));
            //else
            //    return UnprocessableEntity(ApiResult.Fail(result.ErrorMessages));

            return Ok();

        }

        /// <summary>
        /// Deverá retornar o dado de acordo com o identificador criado no método de insert
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetImportById/{id}")]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<ErrosValidacaoResult>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetImportById([FromRoute] int id)
        {
            var param = new ProdutoIdInput() { Id = id };

            var result = await Processor
                .ExecuteQueryAsync<ProdutoIdInput, ProdutoResult>(param);

            return Ok(ApiResult.Ok(result));
        }
    }
}
