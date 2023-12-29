using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly IDataProvider _dataProvider;

        public CountryGwpController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpPost("avg")]
        public async Task<IActionResult> GetAverageGwp([FromBody] GwpRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Country) || request.Lob == null || !request.Lob.Any())
            {
                return BadRequest("Invalid input. Please provide a valid request.");
            }

            var data = await _dataProvider.GetDataAsync();
            if (data == null || !data.Any())
            {
                return StatusCode(500, "Internal server error. Unable to fetch data.");
            }

            var result = new Dictionary<string, double>();
            foreach (var lob in request.Lob)
            {
                var averageGwp = data
                    .Where(entry => entry.Country.ToLower() == request.Country.ToLower() && entry.Lob.ToLower() == lob.ToLower())
                    .Average(entry => entry.Gwp);

                result.Add(lob.ToLower(), averageGwp);
            }

            return Ok(result);
        }
    }

    public class GwpRequest
    {
        public string Country { get; set; }
        public List<string> Lob { get; set; }
    }

    public interface IDataProvider
    {
        Task<List<GwpData>> GetDataAsync();
    }

    public class GwpData
    {
        public string Country { get; set; }
        public string Lob { get; set; }
        public double Gwp { get; set; }
    }

    public class CsvDataProvider : IDataProvider
    {
        private const string FilePath = "Data/gwpByCountry.csv";

        public async Task<List<GwpData>> GetDataAsync()
        {
            // Read CSV file and return data as a list of GwpData
            try
            {
                var csvLines = await File.ReadAllLinesAsync(FilePath);
                return csvLines.Skip(1).Select(line =>
                {
                    var values = line.Split(',');
                    return new GwpData
                    {
                        Country = values[0],
                        Lob = values[1],
                        Gwp = double.Parse(values[2])
                    };
                }).ToList();
            }
            catch (Exception)
            {
                // Handle file reading error
                return null;
            }
        }
    }
}