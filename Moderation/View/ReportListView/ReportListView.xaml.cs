using Moderation.Model;
using Backend.Service;

namespace Moderation.ReportListView;

public partial class ReportListView : ContentPage
{
    private IService service;
	private readonly IEnumerable<PostReport> postReports;
	public ReportListView(IService service, Group group)
	{
        this.service = service;
		// InitializeComponent();
		this.postReports = service.GetReportsWhichBelongToGivenGroup(group);
        CreateList();
	}
	private void CreateList()
	{
        var stackLayout = new StackLayout();

        var titleLabel = new Label
        {
            Text = "List of reports",
        };

        stackLayout.Children.Add(titleLabel);
        foreach (var report in postReports)
        {
            var reportControl = new ReportDisplay(service, report);
            stackLayout.Children.Add(reportControl);
        }
        var backButton = BackButton();
        stackLayout.Children.Add(backButton);

        Content = new ScrollView { Content = stackLayout };
    }
    private Button BackButton()
    {
        var button = new Button { Text = "Back", Margin = 4 };
        button.Clicked += (sender, e) => { Navigation.PopAsync(); };
        return button;
    }
}