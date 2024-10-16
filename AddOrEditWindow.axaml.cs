using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1;

public partial class AddOrEditWindow : Window
{
    public MainWindow.Agent _Agent;
    public AddOrEditWindow()
    {
        InitializeComponent();
        SaveOrEditButton.Content = "Добавить";
    }
    
    public AddOrEditWindow(MainWindow.Agent? agent, bool isEditing)
    {
        InitializeComponent();
        RemoveButton.IsVisible = true;
        _Agent = agent;
        SaveOrEditButton.Content = "Сохранить";
        AgentName.Text = agent.Name;
        AgentType.Text = agent.AgentType;
        AgentPriority.Text = agent.PriorityAsInt.ToString();
        AgentAdress.Text = agent.Adress;
        AgentINN.Text = agent.AgentINN;
        AgentKPP.Text = agent.AgentKPP;
        AgentsDirectorName.Text = agent.AgentsDirectorName;
        AgentPhone.Text = agent.Phone;
        AgentEmail.Text = agent.Email;
        AgentImage.Source = agent.AgentPhoto;
    }

    private void SaveOrEditButtonClick(object? sender, RoutedEventArgs e)
    {
        _Agent.Name = AgentName.Text;
        _Agent.AgentType = AgentType.Text;
        _Agent.Adress = AgentAdress.Text;
        _Agent.PriorityAsInt = int.Parse(AgentPriority.Text);
        _Agent.AgentINN = AgentINN.Text;
        _Agent.AgentKPP = AgentKPP.Text;
        _Agent.AgentsDirectorName = AgentsDirectorName.Text;
        _Agent.Phone = AgentPhone.Text;
        _Agent.Email = AgentEmail.Text;
        Close(false);
    }

    private void RemoveButtonClick(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }
}