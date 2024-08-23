using AppAPI.Repository;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GenericController<T> : ControllerBase where T : class
	{
		private readonly IRepo _repo;

		public GenericController(IRepo repo)
		{
			_repo = repo;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = typeof(T) switch
			{
				Type t when t == typeof(DepartmentFacility) => await _repo.DepartmentFacilityGetAll() as Task<List<T>>,
				Type t when t == typeof(Facility) => await _repo.FacilityGetAll() as Task<List<T>>,
				Type t when t == typeof(Major) => await _repo.MajorGetAll() as Task<List<T>>,
				Type t when t == typeof(MajorFacility) => await _repo.MajorFacilityGetAll() as Task<List<T>>,
				Type t when t == typeof(staff) => await _repo.StaffGetAll() as Task<List<T>>,
				Type t when t == typeof(StaffMajorFacility) => await _repo.StaffMajorFacilityGetAll() as Task<List<T>>,
				_ => throw new NotImplementedException()
			};
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = typeof(T) switch
			{
				Type t when t == typeof(DepartmentFacility) => await _repo.DepartmentFacilityGetById(id) as T,
				Type t when t == typeof(Facility) => await _repo.FacilityGetById(id) as T,
				Type t when t == typeof(Major) => await _repo.MajorGetById(id) as T,
				Type t when t == typeof(MajorFacility) => await _repo.MajorFacilityGetById(id) as T,
				Type t when t == typeof(staff) => await _repo.StaffGetById(id) as T,
				Type t when t == typeof(StaffMajorFacility) => await _repo.StaffMajorFacilityGetById(id) as T,
				_ => throw new NotImplementedException()
			};
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] T entity)
		{
			await _repo.Create(entity);
			return CreatedAtAction(nameof(GetById), new { id = (entity as dynamic).Id }, entity);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] T entity)
		{
			var existingEntity = await _repo.DepartmentFacilityGetById(id) as T;
			if (existingEntity == null) return NotFound();
			await _repo.Update(entity);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var existingEntity = await _repo.DepartmentFacilityGetById(id) as T;
			if (existingEntity == null) return NotFound();
			await _repo.DeleteDepartment(id); // Adjust this line based on entity type
			return NoContent();
		}
	}
}
