using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows;
using MainProject.StatisticWorkSpace.Converter;

namespace MainProject.StatisticWorkSpace.Tests
{
    [TestClass]
    public class DatePickerCustomizationTests
    {
        [TestMethod]
        public void DatePickerDateFormat_DateFormatSet_FormatAppliedToTextBox()
        {
            // Arrange
            var datePicker = new DatePicker();
            DatePickerDateFormat.SetDateFormat(datePicker, new string[] { "(", "yyyy", ")" });
            datePicker.SelectedDate = new DateTime(2022, 1, 1);

            // Act
            datePicker.ApplyTemplate();
            var textBox = GetTemplateTextBox(datePicker);

            // Assert
            Assert.AreEqual("(2022)", textBox.Text);
        }

        private static Calendar GetCalendar(DatePicker datePicker)
        {
            var popup = (Popup)datePicker.Template.FindName("PART_Popup", datePicker);
            return ((Calendar)popup.Child);
        }

        private static TextBox GetTemplateTextBox(DatePicker datePicker)
        {
            datePicker.ApplyTemplate();
            return (TextBox)datePicker.Template.FindName("PART_TextBox", datePicker);
        }
    }
}
