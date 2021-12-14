using BaseCore.Enumerations;
using System;
using System.Runtime.Serialization;

namespace BaseCore.Exceptions
{
    [Serializable]
    public class EnumerationIvalidOperatorException : Exception
    {
        public EnumerationIvalidOperatorException(Enumeration primeiro, Enumeration segundo, string operador) :
            base("Não utilize este operador para comparar enumerações, esta operação não é válida.",
                new Exception($"Este erro foi disparada na comparação entre os elementos: {primeiro?.ToString() ?? "null"} {operador} {segundo?.ToString() ?? "null"}."))
        {
        }

        protected EnumerationIvalidOperatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
