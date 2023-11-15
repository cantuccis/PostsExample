using BusinessLogic.Domain;
using BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IPostsRepository : IRepository<int, PostDTO>
    {
        bool ExistsByTitle(string title);
        List<PostDTO> GetAll(string? title = null, PostOrderBy? orderBy = null);
    }

    public enum PostOrderBy
    {
        CreatedAtASC,
        CreatedAtDESC
    }
}
