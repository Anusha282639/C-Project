using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EmployeeTables
{
    public static void Generate(Dictionary<string, double> employeeHours)
    {
        var sorted = employeeHours.OrderByDescending(e => e.Value).ToList();

        using StreamWriter sw = new StreamWriter("employees.html");
        sw.WriteLine("<html><body><h2>Employee Time Report</h2><table border='1'>");
        sw.WriteLine("<tr><th>Name</th><th>Total Hours Worked</th></tr>");

        foreach (var emp in sorted)
        {
            string color = emp.Value < 100 ? " style='background-color:lightcoral'" : "";
            sw.WriteLine($"<tr{color}><td>{emp.Key}</td><td>{Math.Round(emp.Value, 2)}</td></tr>");
        }

        sw.WriteLine("</table></body></html>");
        Console.WriteLine("✅ HTML Report generated.");
    }

}
