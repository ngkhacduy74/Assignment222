using Assignment.Model;
using System.Diagnostics;

namespace Assignment.Services
{
    public class accountService
    {
        private readonly PrivateGymDbContext _context;

        public accountService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public bool checkEmail(string email)
        {
            var check = _context.Accounts.FirstOrDefault(n => n.Email == email);
            if (check != null)
            {
                return true;// nếu đúng thì đã ó tài khoản rồi
            }
            return false;// nếu sai là chưa có tài khoản , cho phép tạo tài khoán
        }

        public async Task<bool> createAccount(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                int affectedRows = await _context.SaveChangesAsync();

                Debug.WriteLine($"✔ SaveChangesAsync() đã thực thi, affectedRows: {affectedRows}");

                return affectedRows > 0; // Kiểm tra nếu có ít nhất 1 dòng bị ảnh hưởng
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Lỗi khi lưu tài khoản: {ex.Message}");
                return false;
            }
        }
    }
}