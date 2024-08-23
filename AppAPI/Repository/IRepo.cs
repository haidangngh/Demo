using AppData.Models;

namespace AppAPI.Repository
{
	public interface IRepo
	{
		Task <List<Department>> DepartmentGetAll();
		Task <Department> DepartmentGetById(Guid id);

		Task<List<DepartmentFacility>> DepartmentFacilityGetAll();
		Task<List<Facility>> FacilityGetAll();
		Task<List<Major>> MajorGetAll();
		Task<List<MajorFacility>> MajorFacilityGetAll();
		Task<List<staff>> StaffGetAll();
		Task<List<StaffMajorFacility>> StaffMajorFacilityGetAll();

		Task<DepartmentFacility> DepartmentFacilityGetById(Guid id);
		Task<Facility> FacilityGetById(Guid id);
		Task<Major> MajorGetById(Guid id);
		Task<MajorFacility> MajorFacilityGetById(Guid id);
		Task<staff> StaffGetById(Guid id);
		Task<StaffMajorFacility> StaffMajorFacilityGetById(Guid id);
		Task DeleteDepartmentFacility(Guid id);
		Task DeleteFacility(Guid id);
		Task DeleteMajor(Guid id);
		Task DeleteMajorFacility(Guid id);
		Task DeleteStaff(Guid id);
		Task DeleteStaffMajorFacility(Guid id);
		Task DeleteDepartment(Guid id);

		Task Create(Object obj);
		Task Update(Object obj);
		
	}
}
