﻿namespace CalculatorEx;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new CalcWithDataBinding();
	}
}
