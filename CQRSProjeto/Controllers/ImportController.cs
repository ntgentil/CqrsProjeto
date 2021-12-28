using BaseCore.Commands;
using BaseCore.Queries;
using Core.Application.Importacao.Commands.Inputs;
using Core.Application.Importacao.Queries.Inputs;
using Core.Application.Importacao.Queries.Results;
using Core.Helps;
using CQRSProjeto.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
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
        [ProducesResponseType(typeof(ApiResult<>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Insert(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return NotFound(ApiResult.Fail(@"Arquivo não encontrado."));

            var fileLoaded = new FileInfo(file.FileName);
            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            if (!fileLoaded.Extension.Contains(".xlsx"))
                return NotFound(ApiResult.Fail(@"Extenção do arquivo não é permitido."));

            ImportacaoCommand command = new ReadFile().ReadAllLines(memoryStream);

            var result = await CommandDispatcher.ExecuteAsync(command);

            if (result.Success)
                return Ok(ApiResult.Ok());
            else
                return UnprocessableEntity(ApiResult.Fail(result.ErrorMessages));

            return Ok();
        }


        /// <summary>
        /// Deverá listar todas as importações mostrando o seu identificador criado no método de insert
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllImports")]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAllImports()
        {
            var result = await Processor
                .ExecuteQueryAsync<ImportacaoAllInput, ImportacoesResult>(new ImportacaoAllInput());

            if (result.Importacoes.Count == 0)
                return NotFound(ApiResult.Fail(@"Não existem produtos cadastrados."));

            return Ok(ApiResult.Ok(result));

        }

        /// <summary>
        /// Deverá retornar o dado de acordo com o identificador criado no método de insert
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetImportById/{id}")]
        [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetImportById([FromRoute] int id)
        {
            var param = new ImportacaoIdInput() { Id = id };

            var result = await Processor
                .ExecuteQueryAsync<ImportacaoIdInput, ImportacaoProsdutosResult>(param);

            if (result.Produtos.Count == 0)
                return NotFound(ApiResult.Fail(@"Importação não encontrada."));


            return Ok(ApiResult.Ok(result));
        }


        
    }
}
