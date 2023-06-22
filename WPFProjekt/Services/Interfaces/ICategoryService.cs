using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFProjekt.Models;

namespace WPFProjekt.Services.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        public void DeleteCategoryWithNotesById(Category cat);
    }
}
