using BaseCore.Enumerations;

namespace Core.Application.Importacao.Enumerations
{
    public class ValidacaoImportacao : Enumeration
    {

        public static readonly ValidacaoImportacao CPFNaoNulo = new ValidacaoImportacao("CPF", "Não pode ser nulo.");
        public static readonly ValidacaoImportacao CPFInvalido = new ValidacaoImportacao("CPF", "CPF Inválido.");
        public static readonly ValidacaoImportacao MenorIdade = new ValidacaoImportacao("Idade", "O cálculo somente poderá ser realizado para clientes maiores de idade ou emancipados. Gentileza verificar no sistema de Cadastro.");
        public static readonly ValidacaoImportacao ProblemaPlanoRural = new ValidacaoImportacao("DRE", "O cliente não possui Plano da Atividade Rural ou esse encontra-se desatualizado (vigência de 12 meses). Gentileza providenciar a atualização no sistema de cadastro.");
        public static readonly ValidacaoImportacao FalhaSCRBacen = new ValidacaoImportacao("SCR-BACEN", "Falha ao realizar a consulta ao SCR-BACEN via S255. Gentileza executar a solicitação novamente, após alguns minutos.");
        public static readonly ValidacaoImportacao NaoPossuiAvaliacaoRisco = new ValidacaoImportacao("Risco", "O cliente não possui Avaliação de Risco Cliente ou está se encontra vencida. Gentileza proceder com a atualização da avaliação no sistema S253.");
        public static readonly ValidacaoImportacao CadastroDesatualizado = new ValidacaoImportacao("Atualização Cadastro", "O cadastro do cliente encontra-se desatualizado ou sua última renovação foi do tipo automática. Gentileza proceder com a atualização no sistema de cadastro.");
        public static readonly ValidacaoImportacao AtividadeNaoEncontrada = new ValidacaoImportacao("Atividade", "O cliente não possui atividade econômica cadastrada no sistema S400. Gentileza providenciar a regularização no sistema de cadastro.");
        public static readonly ValidacaoImportacao ValidadeLCC = new ValidacaoImportacao("LCC", "Cliente possui LCC na validade, novo cálculo não será realizado.");
        public static readonly ValidacaoImportacao RatingAceito = new ValidacaoImportacao("AVRC", @$"A Avaliação de Risco Cliente registrou conceito igual ou pior que ""D"" para o cliente, portanto, não será possível a realização do cálculo do LCC. Gentileza consultar a Avaliação de Risco Cliente no S253.");
        public static readonly ValidacaoImportacao LimiteLCC = new ValidacaoImportacao("LCC", "Já foram realizados 05 (cinco) LCCs para esse cliente, não sendo permitida a realização de novo LCC nesse mês.");
        public static readonly ValidacaoImportacao CadastroInativo = new ValidacaoImportacao("Cadastro", "O cadastro do cliente encontra-se inativo ou desativado. Gentileza verificar o sistema de cadastro");
        public static readonly ValidacaoImportacao RendaDesatualizada = new ValidacaoImportacao("Rendas", "LCC não calculado, pois a renda encontra-se desatualizada. Gentileza providenciar a atualização no sistema de Cadastro.");
        public static readonly ValidacaoImportacao RendasPassiveisCalculo = new ValidacaoImportacao("Rendas", "O cliente não dispõe de renda / proventos passíveis para fins de cálculo do LCC");

        public ValidacaoImportacao(string id, string name) : base(id, name) { }
    }
}
