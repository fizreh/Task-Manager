using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.AppLayer.Interfaces;

namespace TaskManager.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private readonly ITaskService _taskService;
    
        MainViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }


    }
}
