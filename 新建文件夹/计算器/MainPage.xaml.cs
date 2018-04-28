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
using Windows.UI.Xaml.Hosting;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI;
using Windows.UI.Composition;


// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace 计算器
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            bodyFrame.Navigate(typeof(标准计算器));
            fromGlass(rootForm);
        }
        private void fromGlass(UIElement glassHost) //窗口主体模糊函数
        {

            Visual hostVisual = ElementCompositionPreview.GetElementVisual(glassHost);
            Compositor compositor = hostVisual.Compositor;
            var glassEffect = new GaussianBlurEffect
            {
                BlurAmount = 20.0f,
                BorderMode = EffectBorderMode.Soft,
                Source=new ArithmeticCompositeEffect
                {
                    MultiplyAmount=5,
                    Source1Amount=0.3f,
                    Source2Amount=0.9f,
                    Source1=new CompositionEffectSourceParameter("backdropBursh"),
                    Source2=new ColorSourceEffect
                    {
                        Color=Color.FromArgb(255,20,20,20)
                    }
                }

            };
            var effectFactor = compositor.CreateEffectFactory(glassEffect);
            var backdropBursh = compositor.CreateHostBackdropBrush();
            var effectBursh = effectFactor.CreateBrush();
            effectBursh.SetSourceParameter("backdropBursh", backdropBursh);
            var glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Brush = effectBursh;
            ElementCompositionPreview.SetElementChildVisual(glassHost, glassVisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);
            glassVisual.StartAnimation("Size", bindSizeAnimation);
        }

        private void hamberButton_Click(object sender, RoutedEventArgs e)
        {
            nav.IsPaneOpen = true;
        }

        private void navButton_click(object sender, RoutedEventArgs e)
        {
            nav.IsPaneOpen = false;
        }

        private void text2Selection(object sender, RoutedEventArgs e)
        {
            bodyFrame.Navigate(typeof(科学计算器));
        }

       

        private void isSelection(object sender, SelectionChangedEventArgs e)
        {
            if (list2.IsSelected)
            {
                bodyFrame.Navigate(typeof(标准计算器));

            }
            else if (list3.IsSelected)
            {
                bodyFrame.Navigate(typeof(科学计算器));

            }
            else if (list4.IsSelected)
            {
                bodyFrame.Navigate(typeof(单位转换));

            }
            else if (list5.IsSelected)
            {
                bodyFrame.Navigate(typeof(矩阵计算));

            }
        }
    }
    
}
