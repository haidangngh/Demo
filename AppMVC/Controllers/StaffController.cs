using OfficeOpenXml;
using System.IO;
using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    public class StaffController : Controller
    {
        exam_distribution_testContext context;
        public StaffController(exam_distribution_testContext ct)
        {
            context = ct;  
        }

        HttpClient client = new HttpClient();

        // GET: NhanvienController
        public ActionResult Index()
        {
            // Thử chuyển sang HTTP để kiểm tra nhanh
            string requestURL = "https://localhost:7193/api/Staff/Get-All";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<List<staff>>(response);
            return View(data);
        }


        // GET: NhanvienController/Details/5
        public ActionResult Details(Guid id)
        {
            string requestURL = $"https://localhost:7193/api/Staff/Get-By-Id?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<staff>(response);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: NhanvienController/Create
        [HttpPost]
        public ActionResult Create(staff nv)
        {
            string requestURL = $"https://localhost:7193/api/Staff/Create";
            var response = client.PostAsJsonAsync(requestURL, nv).Result;
            return RedirectToAction("Index");
        }

        // GET: NhanvienController/Edit/5
        public ActionResult Edit(Guid id)
        {
            string requestURL = $"https://localhost:7193/api/Staff/Get-By-Id?id={id}";
            var response = client.GetStringAsync(requestURL).Result;
            var data = JsonConvert.DeserializeObject<staff>(response);
            return View(data);
        }

        // POST: NhanvienController/Edit/5
        [HttpPost]
        public ActionResult Edit(staff nv)
        {
            string requestURL = $"https://localhost:7193/api/Staff/Update";
            var response = client.PutAsJsonAsync(requestURL, nv).Result;
            return RedirectToAction("Index");
        }

        // GET: NhanvienController/Delete/5
        public ActionResult Delete(Guid id)
        {
            string requestURL = $"https://localhost:7193/api/Staff/Delete-By-Id?id={id}";
            var response = client.DeleteAsync(requestURL).Result;
            return RedirectToAction("Index");
        }
        //public ActionResult Khoa(Guid id)
        //{
        //    string requestURL = $"https://localhost:7193/api/Staff/Get-By-Id?id={id}";
        //    var response = client.GetStringAsync(requestURL).Result;
        //    var data = JsonConvert.DeserializeObject<staff>(response);
        //    return View(data);
        //}
        [HttpPut]
        public ActionResult Khoa(staff nv)
        {
            string requestURL = $"https://localhost:7193/api/Staff/Khoa";
            var response = client.PutAsJsonAsync(requestURL, nv).Result;
            return RedirectToAction("Index");
        }
		public IActionResult DownloadExcel()
		{
			var staffList = context.staff.ToList(); // Lấy dữ liệu từ database
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Staff Data");

				// Add header
				worksheet.Cells[1, 1].Value = "ID";
				worksheet.Cells[1, 2].Value = "Name";
				worksheet.Cells[1, 3].Value = "StaffCode";
				worksheet.Cells[1, 4].Value = "AccountFe";
				worksheet.Cells[1, 5].Value = "AccountFpt";
				worksheet.Cells[1, 6].Value = "Status";
				worksheet.Cells[1, 7].Value = "CreatedDate";
				worksheet.Cells[1, 8].Value = "LastModifiedDate";

				// Add data
				for (int i = 0; i < staffList.Count; i++)
				{
					var staff = staffList[i];
					worksheet.Cells[i + 2, 1].Value = staff.Id;               // Cột 1: Id
					worksheet.Cells[i + 2, 2].Value = staff.Name;             // Cột 2: Name
					worksheet.Cells[i + 2, 3].Value = staff.StaffCode;        // Cột 3: StaffCode
					worksheet.Cells[i + 2, 4].Value = staff.AccountFe;        // Cột 4: AccountFe
					worksheet.Cells[i + 2, 5].Value = staff.AccountFpt;       // Cột 5: AccountFpt
					worksheet.Cells[i + 2, 6].Value = staff.Status;           // Cột 6: Status
					worksheet.Cells[i + 2, 7].Value = staff.CreatedDate;      // Cột 7: CreatedDate
					worksheet.Cells[i + 2, 8].Value = staff.LastModifiedDate; // Cột 8: LastModifiedDate
				}


				var stream = new MemoryStream();
				package.SaveAs(stream);

				// Return the Excel file
				stream.Position = 0;
				string excelName = $"StaffData_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
				return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
			}
		}
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var idString = worksheet.Cells[row, 1].Value?.ToString().Trim();
                            if (Guid.TryParse(idString, out Guid staffId))
                            {
                                var staff = new staff
                                {
                                    Id = staffId,
                                    Name = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                                    StaffCode = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                                    AccountFe = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                                    AccountFpt = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                                    Status = Convert.ToByte(worksheet.Cells[row, 6].Value),
                                    CreatedDate = long.Parse(worksheet.Cells[row, 7].Value?.ToString().Trim()),
                                    LastModifiedDate = long.Parse(worksheet.Cells[row, 8].Value?.ToString().Trim())
                                };

                                context.staff.Add(staff);
                            }
                            else
                            {
                                // Xử lý khi giá trị trong cột Id không thể chuyển đổi thành Guid
                                ModelState.AddModelError("", $"Row {row}: Invalid Guid value.");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Xử lý ngoại lệ khác nếu có
                            ModelState.AddModelError("", $"Row {row}: {ex.Message}");
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }


    }
}
