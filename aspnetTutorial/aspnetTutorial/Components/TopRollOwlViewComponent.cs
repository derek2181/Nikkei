using aspnetTutorial.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Components
{
    public class TopRollOwlViewComponent : ViewComponent
    {
        private readonly INikkeiRepository _nikkeiRepository = null;
        public TopRollOwlViewComponent(INikkeiRepository nikkeiRepository)
        {
            _nikkeiRepository = nikkeiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dishes = (await _nikkeiRepository.GetTopRolls(1)).FirstOrDefault();
            return View(dishes);
        }
    }
}
