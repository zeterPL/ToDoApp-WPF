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
        public Task<IList<Note>> GetNotesByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Note>> GetNotesByPriorityAsync(Priority priority)
        {
            throw new NotImplementedException();
        }
    }
}
