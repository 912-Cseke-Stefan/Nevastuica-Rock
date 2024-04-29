
using Moderation.CurrentSessionNamespace;
using Moderation.Model;
using Moderation.Serivce;
using ApplicationState = Moderation.Serivce.ApplicationState;

namespace Moderation;

public partial class GroupsView : ContentPage
{
    public GroupsView()
    {
        Content = new StackLayout { HorizontalOptions = LayoutOptions.Fill };
        MakeKids();
    }

    private void MakeKids()
    {
        foreach (Group group in ApplicationState.Get().Groups.GetAll())
        {
            ((StackLayout)Content).Children.Add(new View.SingleGroupView(group, CurrentSession.GetInstance().User));
        }
        Button backButton = new ()
        {
            Text = "Back",
            HorizontalOptions = LayoutOptions.Fill,
        };
        backButton.Clicked += (s, e) => { CurrentSession.GetInstance().LogOut(); Navigation.PopAsync(); };
        ((StackLayout)Content).Children.Add(backButton);
    }
}