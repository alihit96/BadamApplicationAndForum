using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface ISlider
    {
        IEnumerable<Slider> GetSliders();
        int SliserCount();
        Slider GetSlider(int Id);
        Task AddSlider(Slider slider);
        Task UpdateSlider(Slider slider);
        Task DeleteSlider(int Id);
    }
}
