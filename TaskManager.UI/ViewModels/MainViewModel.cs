using System.Collections.ObjectModel;
using TaskManager.AppLayer.Interfaces;
using TaskManager.AppLayer.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.UI.Commands;

namespace TaskManager.UI.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly ITaskService _taskService;
    public Array Priorities => Enum.GetValues(typeof(Priority));

    public MainViewModel(ITaskService taskService)
    {
        _taskService = taskService;

        Tasks = new ObservableCollection<TodoTask>();

        AddTaskCommand = new AsyncRelayCommand(AddTaskAsync, CanAddTask);

        LoadTasksCommand = new AsyncRelayCommand(LoadTasksAsync);
    }

    #region Properties (UI Inputs)

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set
        {
            if (SetProperty(ref _title, value))
                AddTaskCommand.RaiseCanExecuteChanged();
        }
    }

    private string? _description;
    public string? Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private Priority _priority = Priority.Low;
    public Priority Priority
    {
        get => _priority;
        set => SetProperty(ref _priority, value);
    }

    private DateTime _dueDate = DateTime.Today.AddDays(1);
    public DateTime DueDate
    {
        get => _dueDate;
        set => SetProperty(ref _dueDate, value);
    }

    #endregion

    #region Task Collection

    public ObservableCollection<TodoTask> Tasks { get; }

    #endregion

    #region Commands

    public AsyncRelayCommand AddTaskCommand { get; }
    public AsyncRelayCommand LoadTasksCommand { get; }

    #endregion

    #region Methods

    private bool CanAddTask()
    {
        return !string.IsNullOrWhiteSpace(Title);
    }

    private async Task AddTaskAsync()
    {
        var request = new CreateTaskDTO
        {
            Title = Title,
            Description = Description,
            Priority = Priority,
            DueDate = DueDate,
            AssignedToUserId = null,
            AssignedByUserId = null
        };

        var task = await _taskService.CreateTaskAsync(request);

        Tasks.Add(task);

        ClearInputs();
    }

    private async Task LoadTasksAsync()
    {
        var tasks = await _taskService.GetAllTasksAsync();

        Tasks.Clear();

        foreach (var task in tasks)
        {
            Tasks.Add(task);
        }
    }

    private void ClearInputs()
    {
        Title = string.Empty;
        Description = string.Empty;
        Priority = Priority.Medium;
        DueDate = DateTime.Today.AddDays(1);
    }

    #endregion
}