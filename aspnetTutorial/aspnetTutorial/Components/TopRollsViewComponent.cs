using aspnetTutorial.Models;
using aspnetTutorial.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Components
{
    public class TopRollsViewComponent : ViewComponent
    {
        private readonly INikkeiRepository _nikkeiRepository = null;
        public TopRollsViewComponent(INikkeiRepository nikkeiRepository)
        {
            _nikkeiRepository = nikkeiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var dishes = await _nikkeiRepository.GetTopRolls(count);
            return View(dishes);
        }

     
    }
}
