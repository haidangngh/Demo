using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentFacilityController : ControllerBase
	{
		exam_distribution_testContext _context = new exam_distribution_testContext();
		[HttpGet("Get-All")]

		public ActionResult Index()
		{
			return Ok(_context.DepartmentFacilities.ToList());
		}
		[HttpGet("Get-By-Id")]

		public ActionResult GetById(Guid id)
		{
			return Ok(_context.DepartmentFacilities.Find(id));
		}
		[HttpPost("Create")]

		public ActionResult Create(DepartmentFacility depart)
		{
			try
			{
				_context.Add(depart); _context.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		[HttpPut("Update")]

		public ActionResult Put(DepartmentFacility depart)
		{
			try
			{
				var updateItem = _context.DepartmentFacilities.Find(depart.Id);
				_context.Update(updateItem);
				_context.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		[HttpDelete("Delete-By-Id")]

		public ActionResult Delete(Guid id)
		{
			try
			{
				var updateItem = _context.DepartmentFacilities.Find(id);
				_context.Remove(updateItem);
				_context.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
	}
}
