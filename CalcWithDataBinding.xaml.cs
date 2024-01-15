using Android.App;
using Android.Views;
using System.ComponentModel;
using System.Data;

namespace CalculatorEx;

public class CalculatorBrain:INotifyPropertyChanged
{
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    private bool justEvaluated;
    
    //Connect witht the calculator label
    private string result;
    public string Result
    {
        get
        {
            return this.result;
        }
        set
        {
            this.result = value;
            OnPropertyChanged();
        }
    }

    //THe buttons command
    public Command<string> CalculatorButtonClicks { get; private set; }

    public CalculatorBrain()
    {
        justEvaluated = false;
        Result = "0";
        CalculatorButtonClicks = new Command<string>(Button_Clicked);
    }

    private void Button_Clicked(string str)
    {
        string buttonText = str;

        switch (buttonText)
        {
            case "AC":
                Result = "0";
                break;
            case "+/-":
                Result = Evaluate("-1*(" + Result + ")").ToString();
                justEvaluated = false;
                break;
            case "=":
                Result = Evaluate(Result).ToString();
                break;
            default:
                if (justEvaluated)
                {
                    justEvaluated = false;
                    Result = buttonText;
                }
                else
                    if (Result != "0")
                {
                    Result = Result + buttonText;
                }
                else
                    Result = buttonText;

                break;
        }
    }

    private double Evaluate(string expression)
    {
        expression = expression.Replace('x', '*');
        expression = expression.Replace("%", "/100");

        justEvaluated = true;
        var table = new DataTable();
        var column = new DataColumn("expression", typeof(string), expression);
        table.Columns.Add(column);
        table.Rows.Add(table.NewRow());
        return Convert.ToDouble(table.Rows[0]["expression"]);
    }
}
public partial class CalcWithDataBinding : ContentPage
{
	public CalcWithDataBinding()
	{
		InitializeComponent();
        BindingContext = new CalculatorBrain();
	}
}