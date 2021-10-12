using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Service
{
    public class SliderService : ISlider
    {
        private readonly ApplicationDatabaseContext _context;
        public SliderService(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddSlider(Slider slider)
        {
            _context.Sliders.Add(slider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSlider(int Id)
        {
            var slider = _context.Sliders.Where(s => s.Id == Id).FirstOrDefault();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public Slider GetSlider(int Id)
        {
            return _context.Sliders.Where(s => s.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Slider> GetSliders()
        {
            return _context.Sliders;
        }

        public int SliserCount()
        {
            return _context.Sliders.ToList().Count;
        }

        public async Task UpdateSlider(Slider slider)
        {
            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }
    }
}
