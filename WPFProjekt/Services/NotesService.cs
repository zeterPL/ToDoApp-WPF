using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFProjekt.Models;
using WPFProjekt.Models.Enums;
using WPFProjekt.Services.Interfaces;

namespace WPFProjekt.Services
{
    public class NotesService : BaseService<Note>, INoteService
    {
        public async Task<IList<Note>> GetNotesByCategoryIdAsync(int categoryId)
        {
           return await _context.Notes.Where(n => n.CategoryId == categoryId).ToListAsync();
        }

        public Task<ICollection<Note>> GetNotesByPriorityAsync(Priority priority)
        {
            throw new NotImplementedException();
        }
    }
}
