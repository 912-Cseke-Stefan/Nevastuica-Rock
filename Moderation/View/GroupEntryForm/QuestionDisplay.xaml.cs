using Microsoft.Maui.Controls;
using Moderation.Model;

namespace Moderation.GroupEntryForm;

public abstract partial class QuestionDisplay : ContentView
{
    protected Question question;
    public QuestionDisplay(Question question)
    {
        this.question = question;
        Content = new StackLayout
        {
            Margin = new Thickness(20),
            Spacing = 6
        };
        ((StackLayout)Content).Children.Add(new Label { Text = question.Text });
        CreateContent();
    }
    public string GetQuestion()
    {
        return question.Text;
    }
    public abstract string GetResponse();
    protected abstract void CreateContent();
}
public class TextQuestionDisplay(TextQuestion question) : QuestionDisplay(question)
{
    protected override void CreateContent()
    {
        var inputArea = new Editor();
        ((StackLayout)Content).Children.Add(inputArea);
    }

    public override string GetResponse()
    {
        var boxWithTextInputIndex = 1;
        var mainComponentLayout = (StackLayout)Content;
        var boxWithTextInput = (Editor)mainComponentLayout.Children[boxWithTextInputIndex];
        return boxWithTextInput.Text;
    }
}

public class SliderQuestionDisplay(SliderQuestion question) : QuestionDisplay(question)
{
    protected override void CreateContent()
    {
#pragma warning disable CS9179 // Primary constructor parameter is shadowed by a member from base
        SliderQuestion? sliderQuestion = question as SliderQuestion;
#pragma warning restore CS9179 // Primary constructor parameter is shadowed by a member from base
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var slider = new Slider { Minimum = sliderQuestion.Min, Maximum = sliderQuestion.Max };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        var minValueLabel = new Label { Text = sliderQuestion.Min.ToString(), HorizontalOptions = LayoutOptions.Start };
        var maxValueLabel = new Label { Text = sliderQuestion.Max.ToString(), HorizontalOptions = LayoutOptions.End };

        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        grid.Children.Add(minValueLabel);
        grid.Children.Add(slider);
        grid.Children.Add(maxValueLabel);

        Grid.SetColumn(minValueLabel, 0);
        Grid.SetColumn(slider, 1);
        Grid.SetColumn(maxValueLabel, 2);

        ((StackLayout)Content).Children.Add(grid);
    }

    public override string GetResponse()
    {
        var gridIndexInMainLayout = 1;
        var sliderIndex = 1;
        var mainComponentLayout = (StackLayout)Content;
        var grid = (Grid)mainComponentLayout.Children[gridIndexInMainLayout];
        var slider = (Slider)grid.Children[sliderIndex];

        return slider.Value.ToString();
    }
}
public class RadioQuestionDisplay(RadioQuestion question) : QuestionDisplay(question)
{
    protected override void CreateContent()
    {
#pragma warning disable CS9179 // Primary constructor parameter is shadowed by a member from base
        RadioQuestion? radioQuestion = question as RadioQuestion;
#pragma warning restore CS9179 // Primary constructor parameter is shadowed by a member from base
        // var OptionView = new RadioButtonGroup { }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var option in radioQuestion?.Options)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        {
            ((StackLayout)Content).Children.Add(new RadioButton() { Content = option });
        }
    }
    public override string GetResponse()
    {
        return ((StackLayout)Content)
                .Children
                .Where(child => child is RadioButton)
                .Where(radioButton => ((RadioButton)radioButton).IsChecked)
                .Select(checkedButton => ((RadioButton)checkedButton).Content.ToString())
                .FirstOrDefault()
                ?? "none";
    }
}
public class QuestionDisplayFactory
{
    public static QuestionDisplay GetQuestionDisplay(Question question)
    {
        return question switch
        {
            TextQuestion textQuestion => new TextQuestionDisplay(textQuestion),
            SliderQuestion sliderQuestion => new SliderQuestionDisplay(sliderQuestion),
            RadioQuestion radioQuestion => new RadioQuestionDisplay(radioQuestion),
            _ => throw new NotSupportedException("JoinRequestAnswerToOneQuestion type not supported."),
        };
    }
}
