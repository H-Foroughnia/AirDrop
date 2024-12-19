using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirDrop.Domain.Models.Task;
using AirDropTask = AirDrop.Domain.Models.Task.AirDropTask;

namespace AirDrop.Domain.MVC.ViewModels
{
    public class TaskWithCategoriesViewModel
    {
        public AirDropTask Task { get; set; }
        public IEnumerable<AirDropTaskCategory> TaskCategories { get; set; }
    }
}
