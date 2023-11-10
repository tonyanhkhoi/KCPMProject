﻿using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainProject.StatisticWorkSpace
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : UserControl
    {
        public ContextMenu ContextMenuRowDataGrid { get; set; }
        public StatisticView()
        {
            Initialized += StatisticView_Initialized;
            InitializeComponent();
            txtDatePicker.CalendarOpened += TxtDatePicker_CalendarOpened;
            btnSubmit.Click += BtnSubmit_Click;
            this.ContextMenuRowDataGrid = new ContextMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Xem chi tiết";
            menuItem.Click += ShowDetail_Click;
            this.ContextMenuRowDataGrid.Items.Add(menuItem);
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfo("vi").IetfLanguageTag);
        }

        private void DatagridView_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var target = datagridView;
            if (target != null)
            {
                ContextMenu contextMenu = new ContextMenu();
                MenuItem menuItem = new MenuItem();
                menuItem.Header = "Xem chi tiết";
                menuItem.Click += ShowDetail_Click;
                contextMenu.Items.Add(menuItem);
                contextMenu.PlacementTarget = target;
                contextMenu.IsOpen = true;
            }
        }

        private void ShowDetail_Click(object sender, RoutedEventArgs e)
        {
            if (txtDatePicker.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)txtDatePicker.SelectedDate;
                int selectedMode = cbxStatisticMode.SelectedIndex;
                DateTime minDate;
                DateTime maxDate;

                if (selectedMode == 0)
                {
                    selectWeek(selectedDate, out minDate, out maxDate);
                }
                else if (selectedMode == 1 || selectedMode == 2)
                {
                    selectMonth(selectedDate, out minDate, out maxDate);
                }
                else // Last element
                {
                    selectYear(selectedDate, out minDate, out maxDate);
                }

                DetailStatisticViewModel vmodel = new DetailStatisticViewModel();
                DetailStatisticWindow detailStatisticWindow = new DetailStatisticWindow();
                detailStatisticWindow.min = minDate;
                detailStatisticWindow.max = maxDate;
                detailStatisticWindow.DataContext = vmodel;
                detailStatisticWindow.Show();
            }
        }

        private void TxtDatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            if (txtDatePicker.SelectedDate == null) return;
            var datePicker = (DatePicker)sender;
            var popup = (Popup)datePicker.Template.FindName("PART_Popup", datePicker);
            Calendar calendar = ((Calendar)popup.Child);
            calendar.SetValue(MaterialDesignThemes.Wpf.CalendarAssist.IsHeaderVisibleProperty, false);
            if (calendar.DisplayMode == CalendarMode.Month)
            {
                if (datePicker.SelectedDate != null)
                {
                    DateTime date = (DateTime)datePicker.SelectedDate;
                    calendar.SelectionMode = CalendarSelectionMode.SingleRange;
                    datePicker.SelectedDate = date;
                    DateTime minDate, maxDate;
                    selectMonth(date, out minDate, out maxDate);
                    Console.WriteLine(minDate.ToShortDateString() + " - " + maxDate.ToShortDateString());
                    calendar.SelectedDates.AddRange(minDate, maxDate);
                }
            }
            Console.WriteLine("Show mine calendar");
        }

        private void StatisticView_Initialized(object sender, EventArgs e)
        {
            cbxStatisticMode.SelectedIndex = 1;
            cbxStatisticMode_SelectionChanged(cbxStatisticMode, null);
            setDefaultDate();
        }

        public void setDefaultDate()
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);

            txtDatePicker.DisplayDateEnd = new DateTime(today.Year, today.Month, 1).AddDays(-1);
            txtDatePicker.SelectedDate = today.AddMonths(-1);
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Tháng có định dạng không chính xác. Vui lòng chọn lại", "Tháng không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                setDefaultDate();
                return;
            }
            DateTime selectedDate = (DateTime)txtDatePicker.SelectedDate;
            if (selectedDate.Year == DateTime.Now.Year && selectedDate.Month >= DateTime.Now.Month)
            {
                MessageBox.Show("Chỉ có thể thống kê sau khi đã kết thúc tháng. Vui lòng chọn lại", "Tháng không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                setDefaultDate();
                return;
            }

            StatisticViewModel viewModel = DataContext as StatisticViewModel;
            if (viewModel != null)
            {
                // Task in main thread 
                var button = sender as Button;
                button.Content = "Đổi thời gian";
                foreach (Control child in (button.Parent as Panel).Children)
                {
                    if (!(child == button)) { child.IsEnabled = false; }
                }
                button.Click -= BtnSubmit_Click;
                button.Click += BtnEdit_Click1;

                // Calculate from another thread
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input,
                    new Action<StatisticViewModel, DateTime, int>(CalculateFromDB), viewModel, selectedDate, cbxStatisticMode.SelectedIndex);
            }

        }

        private void CalculateFromDB(StatisticViewModel viewModel, DateTime selectedDate, int selectedMode)
        {
            DateTime minDate = DateTime.Now;
            DateTime maxDate = DateTime.Now;

            if (selectedMode == 0)
            {
                selectWeek(selectedDate, out minDate, out maxDate);
            }
            else if (selectedMode == 1 || selectedMode == 2)
            {
                selectMonth(selectedDate, out minDate, out maxDate);
            }
            else // Last element
            {
                selectYear(selectedDate, out minDate, out maxDate);
            }

            Console.WriteLine("Statistic range {0} - {1}", minDate.ToShortDateString(), maxDate.ToShortDateString());
            viewModel.SetTimeRange(minDate, maxDate);
        }

        private void BtnEdit_Click1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.Content = "Thống kê";
            foreach (Control child in (button.Parent as Panel).Children)
            {
                if (!(child == button)) { child.IsEnabled = true; }
            }
            button.Click -= BtnEdit_Click1;
            button.Click += BtnSubmit_Click;
            var viewModel = DataContext as StatisticViewModel;
            if (viewModel != null)
            {
                viewModel.ClearData();
            }
        }

        void selectWeek(DateTime date, out DateTime minDate, out DateTime maxDate)
        {
            minDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            maxDate = minDate.AddDays(7).AddSeconds(-1);

            //Console.WriteLine(String.Format("Selected Week : {0} - {1}", minDate.ToShortDateString(), maxDate.ToShortDateString()));
        }

        void selectMonth(DateTime date, out DateTime minDate, out DateTime maxDate)
        {
            minDate = new DateTime(date.Year, date.Month, 1, 0, 0, 1);
            maxDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59);

            //Console.WriteLine(String.Format("Selected Month : {0} - {1}", minDate.ToShortDateString(), maxDate.ToShortDateString()));
        }

        void selectYear(DateTime date, out DateTime minDate, out DateTime maxDate)
        {
            minDate = maxDate = new DateTime(date.Year, 1, 1, 0, 0, 1);
            maxDate = new DateTime(date.Year, 12, 31, 23, 59, 59);

            //Console.WriteLine(String.Format("Selected Month : {0} - {1}", minDate.ToShortDateString(), maxDate.ToShortDateString()));
        }

        string[] mDateFormat_Week = new string[] { "Một tuần từ ", "dd/MM/yyyy", null };
        public string[] MyDateFormat_Week => mDateFormat_Week;
        string[] mDateFormat_Month = new string[] { "Cả tháng ", "MM/yyyy", null };
        string[] mDateFormat_Year = new string[] { "Cả năm ", "yyyy", null };

        private void cbxStatisticMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (txtDatePicker != null)
            {
                if (comboBox.SelectedIndex == 0)
                {
                    txtDatePicker.SetValue(DatePickerYearOfDecade.IsYearDecadeProperty, false);
                    txtDatePicker.SetValue(DatePickerMonthOfYear.IsMonthYearProperty, false);
                    txtDatePicker.SetValue(DatePickerDateFormat.DateFormatProperty, mDateFormat_Week);
                }
                else if (comboBox.SelectedIndex == 1 || comboBox.SelectedIndex == 2)
                {
                    txtDatePicker.SetValue(DatePickerYearOfDecade.IsYearDecadeProperty, false);
                    txtDatePicker.SetValue(DatePickerMonthOfYear.IsMonthYearProperty, true);
                    txtDatePicker.SetValue(DatePickerDateFormat.DateFormatProperty, mDateFormat_Month);
                }
                else // Last element
                {
                    txtDatePicker.SetValue(DatePickerYearOfDecade.IsYearDecadeProperty, true);
                    txtDatePicker.SetValue(DatePickerMonthOfYear.IsMonthYearProperty, false);
                    txtDatePicker.SetValue(DatePickerDateFormat.DateFormatProperty, mDateFormat_Year);
                }
                
            }
            var viewModel = DataContext as StatisticViewModel;
            if (viewModel != null)
            {
                viewModel.SetCurrentMode(comboBox.SelectedIndex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
