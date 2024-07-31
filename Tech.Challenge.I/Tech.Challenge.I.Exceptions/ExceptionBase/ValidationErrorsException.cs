using System.Runtime.Serialization;

namespace Tech.Challenge.I.Exceptions.ExceptionBase;

[Serializable]
public class ValidationErrorsException : TechChallengeException
{
    public List<string> MensagensDeErro { get; set; } = [];
    public ValidationErrorsException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }

    protected ValidationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
