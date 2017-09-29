using CodeValue.ScrumBoard.Client.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValue.ScrumBoard.Client.Apis
{
    [Headers("Authorization: Bearer")]
    public interface ITaskApi
    {
        [Get("/task")]
        Task<TaskModel> CreateTaskAsync();

        [Delete("/task/{id}")]
        Task DeleteTaskAsync(string id);
    }
}
