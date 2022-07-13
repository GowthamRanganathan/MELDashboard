using BusinessLayer.Interfaces.Manage;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Models.Manage;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MEL_Dashboard.Controllers
{
    public class ManageController : Controller
    {
        private readonly IGetGrantService _grantService;

        private readonly ILoadDownloadDataService _loaddownloadDataService;

        public ManageController(IGetGrantService grantService, ILoadDownloadDataService loaddownloadDataService)
        {
            _grantService = grantService;
            _loaddownloadDataService = loaddownloadDataService;
        }

        public IActionResult Manage()
        {
            var grants = _grantService.GetGrants();
            return View(grants);
        }

        [HttpPost]
        public IActionResult DownloadGrant(string grantName)
        {
            var downloadData = _loaddownloadDataService.LoadDownloadData(grantName);
            int dataCount = downloadData.Count;

            if (dataCount != 0)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(downloadData.FirstOrDefault().tab_name);
                    int currentRow = 1;

                    for (int i = 1; i <= dataCount; i++)
                    {
                        #region header

                        worksheet.Cell(currentRow, i).Value = downloadData[i-1].column_name;
                        worksheet.Cell(currentRow, i).Style.Fill.BackgroundColor = XLColor.AshGrey;

                        #endregion
                    }

                    var distinctValues = downloadData.Select(o => o.op_ind_name).Distinct().ToList();

                    int distCount = 0;

                    for (int i = dataCount + 1; i <= dataCount + distinctValues.Count; i++)
                    {
                        #region header

                        worksheet.Cell(currentRow, i).Value = distinctValues[distCount];
                        worksheet.Cell(currentRow, i).Style.Fill.BackgroundColor = XLColor.Orange;

                        #endregion

                        distCount++;
                    }

                    var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{downloadData.FirstOrDefault().template_name}.xlsx");
                }
            }
            return RedirectToAction("Manage");
        }
    }
}
