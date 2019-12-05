using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessService.Interfaces
{
    public interface IConverterService
    {
        ITranslator Translator { get; set; }

        string Convert(double number);
    }
}
