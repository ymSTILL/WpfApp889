using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp8 // 請確保與 XAML 的 x:Class 命名空間一致
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, int> beverageMenu = new Dictionary<string, int>()
        {
            { "紅茶大杯", 60 },
            { "紅茶小杯", 40 },
            { "綠茶大杯", 60 },
            { "綠茶小杯", 40 },
            { "可樂大杯", 50 },
            { "可樂小杯", 30 }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            string orderType = rbIn.IsChecked == true ? "內用" : "外帶";

            StringBuilder summary = new StringBuilder();
            summary.AppendLine($"【訂購單 - {orderType}】");
            summary.AppendLine("------------------------------------");

            int rawTotal = 0;
            int totalItems = 0;

            // 將畫面上的 UI 控制項與品項名稱打包進行迴圈處理
            var items = new (CheckBox Chk, Slider Sld, string Name)[]
            {
                (chk1, sld1, "紅茶大杯"),
                (chk2, sld2, "紅茶小杯"),
                (chk3, sld3, "綠茶大杯"),
                (chk4, sld4, "綠茶小杯"),
                (chk5, sld5, "可樂大杯"),
                (chk6, sld6, "可樂小杯")
            };

            foreach (var item in items)
            {
                int qty = (int)item.Sld.Value;
                if (item.Chk.IsChecked == true && qty > 0)
                {
                    int price = beverageMenu[item.Name];
                    int itemTotal = price * qty;
                    rawTotal += itemTotal;
                    totalItems += qty;
                    summary.AppendLine($"{item.Name} x {qty}杯 = {itemTotal} 元");
                }
            }

            if (totalItems == 0)
            {
                txtOrderSummary.Text = "提示：您尚未勾選任何飲料或選擇數量，請重新確認！";
                return;
            }

            summary.AppendLine("------------------------------------");
            summary.AppendLine($"原始小計：{rawTotal} 元");

            int finalTotal = rawTotal;
            if (rawTotal >= 200)
            {
                // 使用 AwayFromZero 確保標準四捨五入，並轉為整數
                finalTotal = (int)Math.Round(rawTotal * 0.9, MidpointRounding.AwayFromZero);
                summary.AppendLine("優惠活動：消費滿 200 元享 9 折優惠！");
            }

            summary.AppendLine($"實付總金額：{finalTotal} 元");
            txtOrderSummary.Text = summary.ToString();
        }
    }
}