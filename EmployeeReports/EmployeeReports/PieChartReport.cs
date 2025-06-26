using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PieChartReport
{
    public static void Generate(Dictionary<string, double> employeeHours)
    {
        double total = employeeHours.Values.Sum();
        Bitmap bmp = new Bitmap(600, 600);
        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);

        float startAngle = 0;
        Random rand = new Random();
        Font font = new Font("Arial", 10);
        var sorted = employeeHours.OrderByDescending(e => e.Value).ToList();

        for (int i = 0; i < sorted.Count; i++)
        {
            var emp = sorted[i];
            float sweep = (float)(emp.Value / total * 360);
            Brush brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
            g.FillPie(brush, 100, 100, 400, 400, startAngle, sweep);

            g.DrawString($"{emp.Key} ({Math.Round(emp.Value / total * 100)}%)",
                         font, brush, 10, 10 + i * 15);

            startAngle += sweep;
        }

        bmp.Save("employee_pie_chart.png", ImageFormat.Png);
        Console.WriteLine("✅ Pie chart image saved.");
    }
}
