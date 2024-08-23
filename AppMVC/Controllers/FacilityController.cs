using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppAPI.Controllers
{
	public class FacilityController : Controller
	{
		HttpClient client = new HttpClient();

		// GET: NhanvienController
		public ActionResult Index()
		{
			// Thử chuyển sang HTTP để kiểm tra nhanh
			string requestURL = "https://localhost:7193/api/Facility/Get-All";
			var response = client.GetStringAsync(requestURL).Result;
			var data = JsonConvert.DeserializeObject<List<Facility>>(response);
			return View(data);
		}


		// GET: NhanvienController/Details/5
		public ActionResult Details(Guid id)
		{
			string requestURL = $"https://localhost:7193/api/Facility/Get-By-Id?id={id}";
			var response = client.GetStringAsync(requestURL).Result;
			var data = JsonConvert.DeserializeObject<Facility>(response);
			return View(data);
		}
        public ActionResult Create()
        {

            return View();
        }
        // POST: NhanvienController/Create
        [HttpPost]
		public ActionResult Create(Facility nv)
		{
			string requestURL = $"https://localhost:7193/api/Facility/Create";
			var response = client.PostAsJsonAsync(requestURL, nv).Result;
			return RedirectToAction("Index");
		}

		// GET: NhanvienController/Edit/5
		public ActionResult Edit(Guid id)
		{
			string requestURL = $"https://localhost:7193/api/Facility/Get-By-Id?id={id}";
			var response = client.GetStringAsync(requestURL).Result;
			var data = JsonConvert.DeserializeObject<Facility>(response);
			return View(data);
		}

		// POST: NhanvienController/Edit/5
		[HttpPost]
		public ActionResult Edit(Facility nv)
		{
			string requestURL = $"https://localhost:7193/api/Facility/Update";
			var response = client.PutAsJsonAsync(requestURL, nv).Result;
			return RedirectToAction("Index");
		}

		// GET: NhanvienController/Delete/5
		public ActionResult Delete(Guid id)
		{
			string requestURL = $"https://localhost:7193/api/Facility/Delete-By-Id?id={id}";
			var response = client.DeleteAsync(requestURL).Result;
			return RedirectToAction("Index");
		}
	}
}
