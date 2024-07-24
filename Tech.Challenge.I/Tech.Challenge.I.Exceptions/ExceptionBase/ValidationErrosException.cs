using System.Runtime.Serialization;

namespace Tech.Challenge.I.Exceptions.ExceptionBase;

[Serializable]
public class ValidationErrosException : TechChallengeException
{
    public List<string> MensagensDeErro { get; set; } = [];
    public ValidationErrosException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }

    protected ValidationErrosException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
