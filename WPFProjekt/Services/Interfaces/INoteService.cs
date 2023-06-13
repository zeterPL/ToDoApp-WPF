using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFProjekt.Models;
using WPFProjekt.Models.Enums;

namespace WPFProjekt.Services.Interfaces
{
    public interface INoteService : IBaseService<Note>
    {
        Task<IList<Note>> GetNotesByCategoryIdAsync(int categoryId);
        Task<IList<Note>> GetNotesByPriorityAsync(Priority priority);
    }
}
