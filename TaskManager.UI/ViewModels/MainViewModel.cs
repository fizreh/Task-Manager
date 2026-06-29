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
        CompleteTaskCommand = new AsyncRelayCommand(CompleteTaskAsync, CanModifyTask);
        DeleteTaskCommand = new AsyncRelayCommand(DeleteTaskAsync, CanModifyTask);
        ReopenTaskCommand = new AsyncRelayCommand(ReopenTaskAsync, CanModifyTask);
        UpdateTaskCommand = new AsyncRelayCommand(UpdateTaskAsync, CanModifyTask);
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

    private TodoTask? _selectedTask;

    public TodoTask? SelectedTask
    {
        get => _selectedTask;
        set
        {
            if (SetProperty(ref _selectedTask, value))
            {
                CompleteTaskCommand.RaiseCanExecuteChanged();
                DeleteTaskCommand.RaiseCanExecuteChanged();
                UpdateTaskCommand.RaiseCanExecuteChanged();

                LoadSelectedTaskToInputs();
            }
        }
    }

    #endregion

    #region Task Collection

    public ObservableCollection<TodoTask> Tasks { get; }

    #endregion

    #region Commands

    public AsyncRelayCommand AddTaskCommand { get; }
    public AsyncRelayCommand LoadTasksCommand { get; }
    public AsyncRelayCommand CompleteTaskCommand { get; }
    public AsyncRelayCommand DeleteTaskCommand { get; }
    public AsyncRelayCommand ReopenTaskCommand { get; }
    public AsyncRelayCommand UpdateTaskCommand { get; }

    #endregion

    #region Methods

    private bool CanAddTask()
    {
        return !string.IsNullOrWhiteSpace(Title);
    }

    private bool CanModifyTask()
    {
        return SelectedTask != null;
    }

    public async Task InitializeAsync()
    {
        await LoadTasksAsync();
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

    private async Task UpdateTaskAsync()
    {
        if (SelectedTask == null) return;
        var request = new UpdateTaskDTO
        {
            Id = SelectedTask.Id,
            Title = Title,
            Description = Description,
            Priority = Priority,
            DueDate = DueDate

        };

        var task = await _taskService.UpdateTaskAsync(request);

        Tasks.Add(task);

        ClearInputs();
    }

    private void LoadSelectedTaskToInputs()
    {
        if (SelectedTask == null) return;

        Title = SelectedTask.Title;
        Description = SelectedTask.Description;
        Priority = SelectedTask.Priority;
        DueDate = SelectedTask.DueDate;
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

    private async Task CompleteTaskAsync()
    {
        if (SelectedTask == null) return;

        await _taskService.CompleteTaskAsync(SelectedTask.Id);

        SelectedTask.MarkCompleted();

        await LoadTasksAsync();

       
    }

    private async Task ReopenTaskAsync()
    {
        if (SelectedTask == null) return;

        await _taskService.ReopenTaskAsync(SelectedTask.Id);

        SelectedTask.Reopen();
    }

    private async Task DeleteTaskAsync()
    {
        if (SelectedTask == null) return;

        await _taskService.DeleteTaskAsync(SelectedTask.Id);

        Tasks.Remove(SelectedTask);
        SelectedTask = null;
    }

    private void ClearInputs()
    {
        Title = string.Empty;
        Description = string.Empty;
        Priority = Priority.Low;
        DueDate = DateTime.Today.AddDays(1);
    }

    #endregion
}