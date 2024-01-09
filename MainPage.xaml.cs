using System.Data;
using System.Linq.Expressions;

namespace CalculatorEx;

public partial class MainPage : ContentPage
{

	bool justEvaluated;
	public MainPage()
	{
		InitializeComponent();
		display.Text = "0";
		justEvaluated = false;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Button button = (Button)sender;
		string buttonText = button.Text;

		switch(buttonText)
		{
			case "AC":
				display.Text = "0";
				break;
            case "+/-":
                display.Text = Evaluate("-1*("+display.Text+")").ToString(); 
                justEvaluated = false;
                break;
            case "=":
                display.Text = Evaluate(display.Text).ToString();
                break;
			default:
                if(justEvaluated)
                {
                    justEvaluated = false;
                    display.Text = buttonText;
                }
                else
                    if (display.Text != "0")
                    {
                        display.Text = display.Text + buttonText;
                    }
				    else
                        display.Text = buttonText;

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


