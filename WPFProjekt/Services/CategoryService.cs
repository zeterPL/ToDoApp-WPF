using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFProjekt.Models;
using WPFProjekt.Services.Interfaces;

namespace WPFProjekt.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public void DeleteCategoryWithNotesById(Category cat)
        {
            var notes = _context.Notes.Where(n => n.CategoryId == cat.Id);
            _context.Notes.RemoveRange(notes);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
        }
    }
}
