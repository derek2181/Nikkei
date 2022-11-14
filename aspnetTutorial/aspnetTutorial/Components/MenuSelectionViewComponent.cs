using aspnetTutorial.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Components
{
    public class MenuSelectionViewComponent : ViewComponent
    {

        private readonly INikkeiRepository _nikkeiRepository = null;
        public MenuSelectionViewComponent(INikkeiRepository nikkeiRepository)
        {
            _nikkeiRepository = nikkeiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selection="Rolls")
        {
            //Desarrollar codigo para que te traiga solo la selecion
            var dishes = await _nikkeiRepository.GetMenuSelection(selection);
            ViewBag.Selection = selection;
            return View(dishes);
        }
    }
}
