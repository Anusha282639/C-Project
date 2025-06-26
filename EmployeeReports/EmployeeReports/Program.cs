using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

class Program
{
    static async Task Main()
    {
        string url = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        using var client = new HttpClient();
        var json = await client.GetStringAsync(url);

        var entries = JsonConvert.DeserializeObject<List<EmployeeEntry>>(json);

        var employeeGroups = entries
            .Where(e => e.EndTimeUtc > e.StarTimeUtc && !string.IsNullOrWhiteSpace(e.EmployeeName))
            .GroupBy(e => e.EmployeeName)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(e => (e.EndTimeUtc - e.StarTimeUtc).TotalHours)
            );


        EmployeeTables.Generate(employeeGroups);
        PieChartReport.Generate(employeeGroups);
    }
}
