namespace CalculatorEx;

public partial class MainPage : ContentPage
{

	double current;
	
	public MainPage()
	{
		InitializeComponent();
		display.Text = "0";
		current = 0;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Button button = (Button)sender;
		string buttonText = button.Text;

		switch(buttonText)
		{
			case "AC":
				display.Text = "0";
				current = 0;
				break;
			default:
                if (display.Text != "0")
                {
                    display.Text = display.Text + buttonText;
                }
				else
                    display.Text = buttonText;

                break;
        }
    }
}

