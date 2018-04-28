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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 计算器
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class det : Page
    {
        public det()
        {
            this.InitializeComponent();

        }

        
        private void inputMatrix2(object sender, TextChangedEventArgs e)
        {
           
            //matrixString = matrix1.Text;

            //int length = matrixString.Length;
            //if (matrixString[length - 1] == ' ' || matrixString[length - 1] == ',')
            //{
            //    tmp = null;
            //    matrixString += ',';
            //}


        }

        private void inputMatrix1(object sender, TextChangedEventArgs e)
        {

        }

        private void star(object sender, RoutedEventArgs e)
        {
            string tmp=null, matrixString;
            matrixString = textInPut.Text;
            List<List<double>> mar1 = new List<List<double>>();
            List<double> item = new List<double>();
            int number=0;
            for (int i = 0; i < matrixString.Length; i++)
            {
                if (matrixString[i]<'9'&&matrixString[i]>'0')
                {
                    tmp += matrixString[i].ToString();
                }
                else if (matrixString[i]==','||matrixString[i]==' ')

                {
                    item.Add(Convert.ToDouble(tmp));
                    tmp = null;
                    
                }
                else if (matrixString[i]=='\r')
                {
                    number++;
                    mar1.Add(item);
                    item.Clear();
                }
            }
            //double[,] temp = new double[5, 5];
            double frist;
            //double[,] mar1 = new double[5, 5] { { 1,1,1 , 1, 1 }, { 1,2, 1, 1, 1 }, { 1, 1, 3, 1, 1}, { 1, 1, 1, 4, 1 }, { 1, 1, 1, 1, 5 } };
            //temp = mar1;
            //for (int i = 0; i < 2; i++)
            //{

            //    if (mar1[i][i] == 0)
            //    {
            //        continue; 
            //    }
            //    for (int k = i; k < 2; k++)
            //    {
            //        frist = mar1[k + 1][i];
            //        for (int j = i; j < 5; j++)
            //        {

            //            mar1[k + 1][ j] -=  frist*mar1[i][j] / mar1[i][i];
            //        }
            //    }
            //}
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0;  j< 3; j++)
            //    {
            //        resulrt.Text += mar1[i][j].ToString() + ",";
            //    }
            //    resulrt.Text += "\r";

            //}
            resulrt.Text += mar1[0][0].ToString() + ",";
        }

    }
}
