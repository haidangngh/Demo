using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Repository
{
	public class Repo : IRepo
	{
		private readonly exam_distribution_testContext _context;

		public Repo(exam_distribution_testContext context)
		{
			_context = context;
		}

		// Create
		public async Task Create(object obj)
		{
			_context.Add(obj);
			await _context.SaveChangesAsync();
		}

		// Delete
		public async Task DeleteDepartment(Guid id)
		{
			var entity = await _context.Departments.FindAsync(id);
			if (entity != null)
			{
				_context.Departments.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteDepartmentFacility(Guid id)
		{
			var entity = await _context.DepartmentFacilities.FindAsync(id);
			if (entity != null)
			{
				_context.DepartmentFacilities.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteFacility(Guid id)
		{
			var entity = await _context.Facilities.FindAsync(id);
			if (entity != null)
			{
				_context.Facilities.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteMajor(Guid id)
		{
			var entity = await _context.Majors.FindAsync(id);
			if (entity != null)
			{
				_context.Majors.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteMajorFacility(Guid id)
		{
			var entity = await _context.MajorFacilities.FindAsync(id);
			if (entity != null)
			{
				_context.MajorFacilities.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteStaff(Guid id)
		{
			var entity = await _context.staff.FindAsync(id);
			if (entity != null)
			{
				_context.staff.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteStaffMajorFacility(Guid id)
		{
			var entity = await _context.StaffMajorFacilities.FindAsync(id);
			if (entity != null)
			{
				_context.StaffMajorFacilities.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		// GetAll
		public async Task<List<DepartmentFacility>> DepartmentFacilityGetAll()
		{
			return await _context.DepartmentFacilities.ToListAsync();
		}

		public async Task<List<Department>> DepartmentGetAll()
		{
			return await _context.Departments.ToListAsync();
		}

		public async Task<List<Facility>> FacilityGetAll()
		{
			return await _context.Facilities.ToListAsync();
		}

		public async Task<List<MajorFacility>> MajorFacilityGetAll()
		{
			return await _context.MajorFacilities.ToListAsync();
		}

		public async Task<List<Major>> MajorGetAll()
		{
			return await _context.Majors.ToListAsync();
		}

		public async Task<List<staff>> StaffGetAll()
		{
			return await _context.staff.ToListAsync();
		}

		public async Task<List<StaffMajorFacility>> StaffMajorFacilityGetAll()
		{
			return await _context.StaffMajorFacilities.ToListAsync();
		}

		// GetById
		public async Task<DepartmentFacility> DepartmentFacilityGetById(Guid id)
		{
			return await _context.DepartmentFacilities.FindAsync(id);
		}

		public async Task<Department> DepartmentGetById(Guid id)
		{
			return await _context.Departments.FindAsync(id);
		}

		public async Task<Facility> FacilityGetById(Guid id)
		{
			return await _context.Facilities.FindAsync(id);
		}

		public async Task<MajorFacility> MajorFacilityGetById(Guid id)
		{
			return await _context.MajorFacilities.FindAsync(id);
		}

		public async Task<Major> MajorGetById(Guid id)
		{
			return await _context.Majors.FindAsync(id);
		}

		public async Task<staff> StaffGetById(Guid id)
		{
			return await _context.staff.FindAsync(id);
		}

		public async Task<StaffMajorFacility> StaffMajorFacilityGetById(Guid id)
		{
			return await _context.StaffMajorFacilities.FindAsync(id);
		}

		// Update
		public async Task Update(object obj)
		{
			_context.Update(obj);
			await _context.SaveChangesAsync();
		}
	}
}
