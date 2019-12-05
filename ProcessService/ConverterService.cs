using ProcessService.Interfaces;

namespace ProcessService
{
    public class ConverterService : IConverterService
    {
        public ITranslator Translator { get; set; }

        public string Convert(double number)
        {
            if (Translator != null)
                return Translator.Translate(number);

            return string.Empty;
        }
    }
}
