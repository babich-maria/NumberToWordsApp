using System;
using System.Web.Mvc;
using NumberToWordApp.Models;
using ProcessService;
using ProcessService.Interfaces;

namespace NumberToWordApp.Controllers
{
    public class HomeController : Controller
    {
        readonly  private ITranslator _englishTranslator;
        readonly private  IConverterService _conserterService;
        public HomeController(ITranslator translator, IConverterService conserterService)
        {
            _englishTranslator = translator ?? throw new  ArgumentNullException("translator == null");
            _conserterService = conserterService ?? throw new  ArgumentNullException("conserterService == null");

            _conserterService.Translator = _englishTranslator;
        }
      
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Convertion(NumberViewModel number)
        {
            var result = new WordResultViewModel();
            if (ModelState.IsValid)
            {
                result.ValueWord = _conserterService.Convert(number.ValueDouble);
            }

            return PartialView(result);
        }
    }
}