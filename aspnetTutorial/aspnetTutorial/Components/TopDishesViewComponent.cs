using aspnetTutorial.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial
{
    public class TopDishesViewComponent : ViewComponent
    {
        private readonly INikkeiRepository _nikkeiRepository = null;
        public TopDishesViewComponent(INikkeiRepository nikkeiRepository)
        {
            _nikkeiRepository = nikkeiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var dishes = await _nikkeiRepository.GetTopDishes(count);
            return View(dishes);
        }
    }
}
