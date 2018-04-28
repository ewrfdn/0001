using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.RegularExpressions;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 计算器
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class 标准计算器 : Page
    {
        public 标准计算器()
        {
            this.InitializeComponent();
        }
        string expression = null, data = null;
        double result = 0;
        Stack<double> ansStack = new Stack<double>();
        private void export_click(object sender, RoutedEventArgs e)
        {
            string date=DateTime.Now.ToString();
          
        }

        private void cleanUP_click(object sender, RoutedEventArgs e)
        {
            history.Text = " ";
        }

        private void DEL_click(object sender, RoutedEventArgs e)
        {
            
            if (expression != null)
            {
                expression = expression.Substring(0, expression.Length - 1);

                resultText.Text = expression;
            }

        }

        private void CE_CLick(object sender, RoutedEventArgs e)
        {
            expression = null;
            resultText.Text = " ";

        }


        private void Click_number(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            expression += btn.Content.ToString();
            data += btn.Content.ToString();
            resultText.Text = expression;


        }

        private void Click_char(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            expression += btn.Content.ToString();
            resultText.Text = expression;



        }

        private void Click_result(object sender, RoutedEventArgs e)
        {

            try
            {
                result = Calculate(expression);
                ansStack.Push(result);
                resultText.Text = expression+"\r" + result.ToString();
                history.Text += resultText.Text + "\r" + "_____________ \r";

            }
            catch
            {
                resultText.Text = "表达式错误";
            }
           
        }

        private void Click_ans(object sender, RoutedEventArgs e)
        {
            try
            {
                expression += ansStack.Peek().ToString();
                resultText.Text = expression;
            }
            catch
            {
                resultText.Text = "没有历史数据";
            }
        }

        private double Calculate(string expression)
        {
            Stack<string> operater = new Stack<string>(), operand = new Stack<string>();//创建一个存放用算符的stack 和一个存放操作数的stack
            Stack<double> result = new Stack<double>();
            string tmp=null;
            //double resultl=0;
            for (int i = 0; i < expression.Length; i++)//中置表达式转换为逆波兰表达式
            {
                if (expression[i] <= '9' && expression[i] >= '0'||expression[i]=='.')
                {
                    tmp += expression[i].ToString();
                    if (i+1==expression.Length||!(expression[i+1] <= '9' && expression[i+1] >= '0'||expression[i+1]=='.'))
                    {

                        operand.Push(tmp);
                        tmp = null;
                    }
                }
                else if(expression[i]== 'π')
                {
                    if (Regex.IsMatch(expression[i - 1].ToString(), @"^\d"))
                        operater.Push("×");
                    operand.Push(expression[i].ToString());
                }
                else if (expression[i] == '(')
                {
                    if(Regex.IsMatch(expression[i-1].ToString(),@"^\d"))
                    { operater.Push("×"); }
                    tmp = expression[i].ToString();
                    operater.Push(tmp);
                    tmp = null;

                }
                else if (expression[i] == ')')
                {
                    for (int j=operater.Count;j!=0 ; j--)
                    {
                        if (operater.Peek() != "(")
                        {
                            operand.Push(operater.Pop());

                        }
                        else if (operater.Peek() == "(")
                        {
                            operater.Pop();
                            j = 1;
                        }
                    }
                   
                }
                else if (expression[i] == '+' || expression[i] == '×' || expression[i] == '-' || expression[i] == '÷')
                {
                    if (operater.Count==0||operater.Peek()=="("|| operater.Peek() == ")")
                    {
                       operater.Push( expression[i].ToString());
                    }
                    else if ((expression[i]=='×'||expression[i]=='÷')&&(operater.Peek()=="+"||operater.Peek()=="-"))
                    {
                        operater.Push(expression[i].ToString());

                    }
                    else
                    {
                        operand.Push(operater.Pop());
                        operater.Push(expression[i].ToString());

                    }
                }
                else if(expression[i]== '²'||expression[i]== '√')
                {
                    operater.Push(expression[i].ToString());
                }
            }
            if (operater.Count!=0)
            {
                for (int i = operater.Count; i != 0; i--)
                {
                    operand.Push(operater.Pop());
                }

            }
           
            foreach (string item in operand)
            {
               

                operater.Push(item);
            }
            foreach (string item in operater)
            {
                if (Regex.IsMatch(item, @"^\d*[.]?\d*$"))
                {

                    result.Push(Convert.ToDouble(item));

                }
                else
                {
                    switch (item)
                    {
                        case "+":
                            {
                                double resultLeft, resultRight ;
                                if (result.Count==0)
                                {
                                    resultLeft = 0;
                                }
                                else
                                {
                                    resultLeft = result.Pop();
                                }

                                if (result.Count == 0)
                                {
                                    resultRight = 0;
                                }
                                else
                                {
                                    resultRight = result.Pop();

                                }
                                result.Push(resultLeft + resultRight);

                            }
                            break;
                        case "-":
                            {
                                double resultLeft, resultRight;
                                if (result.Count == 0)
                                {
                                    resultLeft = 0;
                                }
                                else
                                {
                                    resultLeft = result.Pop();
                                }

                                if (result.Count == 0)
                                {
                                    resultRight = 0;
                                }
                                else
                                {
                                    resultRight = result.Pop();

                                }
                                result.Push(resultRight - resultLeft);

                            }
                            break;
                        case "×":
                            {
                                double resultLeft = result.Pop(), resultRight = result.Pop();
                                result.Push(resultLeft * resultRight);

                            }
                            break;
                        case "÷":
                            {
                                double resultLeft = result.Pop(), resultRight = result.Pop();
                                result.Push(resultRight / resultLeft);

                            }
                            break;
                        case "π":
                            {
                                result.Push(Math.PI);
                            }break;
                        case "√":
                            {
                                result.Push(Math.Sqrt(result.Pop()));
                            }break;
                        case "²":
                            {
                                double resultLeft = result.Pop();
                                result.Push(resultLeft*resultLeft);
                            }
                            break;                    }
                }

            }
            return result.Pop();
        }
       
    }
   

    
}
