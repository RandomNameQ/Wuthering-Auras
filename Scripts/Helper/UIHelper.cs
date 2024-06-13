using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using Button = System.Windows.Controls.Button;
using DataObject = System.Windows.DataObject;
using TextBox = System.Windows.Controls.TextBox;

public static class UIHelper
{
    public static bool ChangeButtonColorOnClick(object sender)
    {

        var button = sender as Button;
        if (button == null) return true;


        // Сохранение оригинального цвета кнопки
        var originalColor = button.Background;

        // Изменение цвета кнопки на зелёный
        button.Background = new SolidColorBrush(Colors.DarkSeaGreen);

        // Создание таймера на 2 секунды
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        // Обработчик события по истечении таймера
        timer.Tick += (s, e) =>
        {
            // Возврат кнопки к оригинальному цвету
            button.Background = originalColor;
            timer.Stop();
        };

        timer.Start();
        return false;
    }

    public static string GetPath()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "PNG Files (*.png)|*.png";
        DialogResult result = openFileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            string selectedFilePath = openFileDialog.FileName;
            return selectedFilePath;
        }
        return null;
    }


    public static void AllowOnlyLetters(TextBox textBox)
    {
        textBox.PreviewTextInput += (sender, e) =>
        {
            e.Handled = !IsTextAllowed(e.Text, "^[a-zA-Z]+$");
        };

        DataObject.AddPastingHandler(textBox, (sender, e) =>
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text, "^[a-zA-Z]+$"))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        });
    }

    public static void AllowOnlyNumbers(TextBox textBox)
    {
        textBox.PreviewTextInput += (sender, e) =>
        {
            e.Handled = !IsTextAllowed(e.Text, "^[0-9]+$");
        };

        DataObject.AddPastingHandler(textBox, (sender, e) =>
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text, "^[0-9]+$"))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        });
    }

    public static string AllowOnlyNumbers(string input)
    {
        Regex regex = new Regex("[^0-9]+"); // Регулярное выражение, разрешающее только цифры
        return regex.Replace(input, "");
    }

    private static bool IsTextAllowed(string text, string pattern)
    {
        return Regex.IsMatch(text, pattern);
    }


    public static string GetString(object sender)
    {
        var textBox = sender as TextBox;
        if (textBox != null) return textBox.Text;

        return "";
    }
}

