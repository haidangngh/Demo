using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffController : ControllerBase
	{
		exam_distribution_testContext _context = new exam_distribution_testContext();
		[HttpGet("Get-All")]

		public ActionResult Index()
		{
			return Ok(_context.staff.ToList());
		}
		[HttpGet("Get-By-Id")]

		public ActionResult GetById(Guid id)
		{
			return Ok(_context.staff.Find(id));
		}
        [HttpPost("Create")]
        public ActionResult Create(staff depart)
        {
            try
            {
                // Kiểm tra trùng lặp email FPT
                bool isFptEmailExists = _context.staff.Any(s => s.AccountFpt == depart.AccountFpt);
                if (isFptEmailExists)
                {
                    return BadRequest("Email FPT đã tồn tại.");
                }

                // Kiểm tra trùng lặp email FE
                bool isFeEmailExists = _context.staff.Any(s => s.AccountFe == depart.AccountFe);
                if (isFeEmailExists)
                {
                    return BadRequest("Email FE đã tồn tại.");
                }

                // Kiểm tra trùng lặp StaffCode
                bool isStaffCodeExists = _context.staff.Any(s => s.StaffCode == depart.StaffCode);
                if (isStaffCodeExists)
                {
                    return BadRequest("Mã nhân viên đã tồn tại.");
                }

                depart.Id = Guid.NewGuid();
                depart.CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                depart.LastModifiedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                _context.staff.Add(depart);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Đã xảy ra lỗi trong quá trình tạo nhân viên.");
            }
        }

        [HttpPut("Update")]

		public ActionResult Put(staff depart)
		{
			try
			{
				var updateItem = _context.staff.Find(depart.Id);
				_context.Update(updateItem);
				_context.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		[HttpPut("Khoa")]
        public ActionResult Khoa(staff depart)
        {
            try
            {
                var updateItem = _context.staff.Find(depart.Id);
                // Cập nhật thuộc tính Status của đối tượng dựa trên giá trị hiện tại
                depart.Status = depart.Status == 1 ? (byte)0 : (byte)1;


                // Đánh dấu đối tượng là đã thay đổi
                _context.Update(updateItem);

                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                // Trả về kết quả thành công
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
				var updateItem = _context.staff.Find(id);
				_context.Remove(updateItem);
				_context.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		[HttpPut("updateStatus")]
		public IActionResult UpdateStatus([FromBody] StatusUpdateRequest request)
		{
			// Giả sử bạn có cách để truy xuất và cập nhật đối tượng staff
			// Ví dụ: Cập nhật trạng thái trong cơ sở dữ liệu hoặc bộ nhớ
			var updatedStatus = request.Status; // Cập nhật trạng thái thành công

			// Trả về trạng thái đã cập nhật
			return Ok(new { updatedStatus });
		}
		public class StatusUpdateRequest
		{
			public byte Status { get; set; }
		}
	}
}
